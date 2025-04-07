import { ChangeDetectorRef, Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AppModules } from './app-modules';
import { EventService } from './services/event.service';
import { LoaderService } from './services/loader.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    AppModules
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'LeapEventApp';

  _loading: boolean = false;
  _text: string = 'Processing';

  private _subs: Subscription[] = [];

  constructor(
    private eventService: EventService,
    private router: Router,
    private loaderService: LoaderService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this._subs.push(this.loaderService.showLoader$.subscribe((message) => {
      this._loading = true;
      this._text = message;
      this.cdr.detectChanges()
    }))

    this._subs.push(this.loaderService.hideLoader$.subscribe(() => {
      this._loading = false;
      this._text = '';
      this.cdr.detectChanges()
    }))
  }

  async clearCache() {
    await this.eventService.clearCache();
  }

  getEvents(days: number) {
    this.router.navigate(['/'], { skipLocationChange: true }).then(() => {
      this.router.navigate(['event-list', days])
    });
    
  }
}
