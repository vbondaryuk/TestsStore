import { IStatus } from "./status";
import { IEntity } from "./entity";

export interface IBuild extends IEntity {
    status: IStatus;
    startTime: Date;
    endTime: Date;
    duration: number;
}