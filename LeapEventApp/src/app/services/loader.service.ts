import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {

  private showLoaderSubject = new Subject<string>();
  private hideLoaderSubject = new Subject<void>();

  showLoader$ = this.showLoaderSubject.asObservable();
  hideLoader$ = this.hideLoaderSubject.asObservable();  
  
  constructor() { }

  
  showLoader(message: string) {
    this.showLoaderSubject.next(message);
  }

  hideLoader() {
    this.hideLoaderSubject.next();
  }
}
