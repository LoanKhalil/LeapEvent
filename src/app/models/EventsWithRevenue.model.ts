import { Events } from "./Events.model";

export interface EventsWithRevenue extends Events {
    totalRevenue: number
}