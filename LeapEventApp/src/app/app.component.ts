import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AppModules } from './app-modules';
import { EventService } from './services/event.service';

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

  constructor(
    private eventService: EventService,
    private router: Router
  ) {}

  async clearCache() {
    await this.eventService.clearCache();
  }

  getEvents(days: number) {
    this.router.navigate(['/'], { skipLocationChange: true }).then(() => {
      this.router.navigate(['event-list', days])
    });
    
  }
}
