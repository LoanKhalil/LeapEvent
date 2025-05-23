import { Component } from '@angular/core';
import { EventService } from '../../services/event.service';
import { Events } from '../../models/Events.model';
import { AppModules } from '../../app-modules';
import { EventsWithVolume } from '../../models/EventsWithVolume.model';
import { EventsWithRevenue } from '../../models/EventsWithRevenue.model';
import { LoaderService } from '../../services/loader.service';

@Component({
  selector: 'app-sales-summary',
  standalone: true,
  imports: [
    AppModules
  ],
  templateUrl: './sales-summary.component.html',
  styleUrl: './sales-summary.component.scss'
})
export class SalesSummaryComponent {
  top5VolumeEvents: EventsWithVolume[] = [];
  top5RevenueEvents: EventsWithRevenue[] = [];

  error: string | null = null;

  constructor(
    private eventService: EventService, 
    private loaderService: LoaderService
  ) {}

  ngOnInit() {
    this.loadTopEventsByVolume();
    this.loadTopEventsByRevenue();
  }

  loadTopEventsByVolume() {
    this.loaderService.showLoader('Loading...')
    this.eventService.getTop5Volume().subscribe({
      next: result => {
        this.loaderService.hideLoader();
        this.top5VolumeEvents = result.data
      },
      error: err => this.error = err.message
    });

    
  }

  loadTopEventsByRevenue() {
    this.loaderService.showLoader('Loading...')
    this.eventService.getTop5Revenue().subscribe({
      next: result => {
        this.loaderService.hideLoader();
        this.top5RevenueEvents = result.data
      },
      error: err => this.error = err.message
    });

    
  }
}
