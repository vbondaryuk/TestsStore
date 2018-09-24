import { IEntity } from "./entity";
import { ITestsSummary } from "./testSummary";

export interface IBuildDetails extends IEntity {
    status: string;
    startTime: Date;
    endTime: Date;
    duration: number;
    testsSummary: ITestsSummary[]
}