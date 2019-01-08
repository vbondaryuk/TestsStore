import {IStatus} from './status';
import {IEntity} from './entity';

export interface IBuild extends IEntity {
  status: string;
  startTime: Date;
  endTime: Date;
  duration: number;
}
