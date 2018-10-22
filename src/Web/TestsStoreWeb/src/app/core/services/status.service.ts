import {Injectable} from '@angular/core';
import {IStatus} from '../models/status';

@Injectable({providedIn: 'root'})
export class StatusService {

  static getColorByName(status: string): string {
    if (status === 'Passed') {
      return '#5AA454';
    } else if (status === 'Failed') {
      return '#A10A28';
    } else if (status === 'Skipped') {
      return '#C7B42C';
    } else if (status === 'Inconclusive') {
      return '#FF9933';
    }

    return '#1d1f1d';
  }

  getColor(status: string | IStatus) {
    if (typeof status === 'string') {
      return StatusService.getColorByName(status);
    }

    return this.getColorByStatus(status);
  }

  getColorByStatus(status: IStatus): string {
    return StatusService.getColorByName(status.name);
  }
}
