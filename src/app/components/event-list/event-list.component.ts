import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { Events } from '../../models/Events.model';
import { AppModules } from '../../app-modules';
import { MatSelectChange } from '@angular/material/select';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [
    AppModules
  ],
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss'
})
export class EventListComponent implements OnInit {
    
  _days: number = 30;
  events: Events[] = [];
  sortBy: 'name' | 'startDate' = 'startDate';
  error: string | null = null;

  constructor(
    private eventService: EventService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.route.params.subscribe(params => {
      const days = params["days"]
      if (days)
        this._days = days;
      
      this.loadEvents();
    })
    
  }

  loadEvents() {
    this.eventService.getEvents(this._days).subscribe({
      next: data => {
        this.events = data;
        this.sortEvents();
        
      },
      error: err => this.error = err.message
    });
  }

  sortEvents() {
    if (this.sortBy === 'name') {
      this.events.sort((a, b) => a.name.localeCompare(b.name));
    } else {
      this.events.sort((a, b) => new Date(a.startsOn).getTime() - new Date(b.startsOn).getTime());
    }
  }

  onSortChange(ev: MatSelectChange) {
    const sortField = ev.value
    this.sortBy = sortField;
    this.sortEvents();
  }
}
