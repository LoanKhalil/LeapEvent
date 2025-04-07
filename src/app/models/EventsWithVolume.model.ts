import { Events } from "./Events.model";

export interface EventsWithVolume extends Events {
    totalVolume: number
}