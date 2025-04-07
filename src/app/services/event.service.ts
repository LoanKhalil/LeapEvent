import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of, shareReplay, tap, throwError } from 'rxjs';
import { IApiResponse } from '../models/IApiResponse.model';
import { Events } from '../models/Events.model';
import { openDB } from 'idb';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private readonly dbName = 'LeapEventDb';
  private readonly eventsTable = 'Events';

  private events: {days: number, events: Events[]}  | null = null;
  private events$: Observable<Events[]> | null = null;

  

  constructor(private http: HttpClient,
    private snackBar: MatSnackBar
  ) {}

  
  async initDB() {
    return openDB(this.dbName, 1, {
      upgrade(db) {
        if (!db.objectStoreNames.contains('Events')) {
          db.createObjectStore('Events', { keyPath: 'days'});
        }
        
      }
    });
  }

  async getCachedData(days: number): Promise<Events[] | null> {
    const db = await this.initDB();
    return (await db.get(this.eventsTable, days))?.data || null;
  }

  async saveToCache(days: number, data: Events[]) {
    const db = await this.initDB();
    await db.put(this.eventsTable, { days, data });
  }

  async clearCache() {
    this.events = null;
    this.events$ = null;
    const db = await this.initDB();
    await db.clear(this.eventsTable);
  }


  getEvents(days: number): Observable<Events[]> {
    if (this.events && this.events.days == days) 
      return of(this.events.events);

    
    this.events$ = new Observable<Events[]>(observer => {
      this.getCachedData(days).then(cachedData => {
        if (cachedData) {
          observer.next(cachedData)
          observer.complete() 
        } else {
          this.http.get<IApiResponse>(`${environment.apiServer}/events/${days}`).pipe(
            map(response => response.data),
            tap(async data => {
              this.events = {days: days, events: data}
              await this.saveToCache(days, data)
            }) 
          ).subscribe({
            next: data => {
              observer.next(data);
              observer.complete()
            },
            error: err => observer.error(err)
          })
        }
      })
    }).pipe(shareReplay(1));
    
    
    return this.events$;
  }

  getTop5Volume(): Observable<IApiResponse> {
    return this.http.get<IApiResponse>(`${environment.apiServer}/events/TopSellingEventsByVolume`).pipe(
      catchError(this.handleError)
    );
  }

  getTop5Revenue(): Observable<IApiResponse> {
    return this.http.get<IApiResponse>(`${environment.apiServer}/events/TopSellingEventsByRevenue`).pipe(
      catchError(this.handleError)
    );
  }


  private handleError(error: HttpErrorResponse): Observable<IApiResponse> {
    console.error('API Error:', error);
    this.snackBar.open("We apologize, an error occurred while processing your request. Please try again.", undefined, {duration: 2000, verticalPosition: 'top'});
    return throwError(() => new Error('Something went wrong while fetching data.'));
  }
}
