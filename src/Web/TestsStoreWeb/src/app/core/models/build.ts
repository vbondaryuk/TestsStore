import { IStatus } from "./status";

export interface IBuild{
    id: string;
    name: string;
    status: IStatus;
    startTime: Date;
    endTime: Date;
    duration: number;
}