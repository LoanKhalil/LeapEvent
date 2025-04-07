import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { SalesSummaryComponent } from './components/sales-summary/sales-summary.component';


export const routes: Routes = [
    { path: '', component: EventListComponent},
    { path: 'sales-summary', component: SalesSummaryComponent },
    { path: 'event-list/:days', component: EventListComponent }
];
