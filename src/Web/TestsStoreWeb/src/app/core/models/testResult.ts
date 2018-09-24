import { IStatus } from "./status";
import { ITest } from "./test";

export interface ITestResult {
    id: string;
    test: ITest;
    buildId: string;
    status: IStatus;
    duration: number;
    message: string;
    stackTrace: string;
    errorMessage: string;
}