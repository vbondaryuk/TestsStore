import {IStatus} from './status';
import {ITest} from './test';
import {IBuild} from './build';

export interface ITestResult {
  id: string;
  test: ITest;
  build: IBuild;
  status: IStatus;
  duration: number;
  message: string;
  stackTrace: string;
  errorMessage: string;
}
