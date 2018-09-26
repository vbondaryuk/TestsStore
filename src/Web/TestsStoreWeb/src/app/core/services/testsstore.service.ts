import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { IProject } from "../models/project";
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { IBuild } from "../models/build";
import { IBuildDetails } from "src/app/core/models/buildDetails";
import { ITestResult } from "../models/testResult";
import { IPaginatedItems } from "../models/paginatedItems";

@Injectable({ providedIn: 'root' })
export class TestsStoreService {

    baseUrl = environment.baseUrl + "api/";

    constructor(private http: HttpClient) { }

    getProjects(): Observable<IProject[]> {
        return this.http.get<IProject[]>(this.baseUrl + "project/items")
            .pipe(catchError(this.handleError('getProjects', [])));
    }

    getBuilds(projectId: string): Observable<IBuild[]> {
        return this.http.get<IBuild[]>(this.baseUrl + "build/project/" + projectId)
            .pipe(catchError(this.handleError('getBuilds', [])));
    }

    getBuildDetails(buildId: string): Observable<IBuildDetails> {
        return this.http.get<IBuildDetails>(this.baseUrl + "build/id/" + buildId + "/details")
            .pipe(catchError(this.handleError('getBuilds', null)));
    }

    getTestStatistic(testId: string): Observable<ITestResult[]> {
        return this.http.get<ITestResult[]>(
            `${this.baseUrl}testresult/items/test/${testId}/statistic`)
            .pipe(catchError(this.handleError('getBuilds', [])));
    }
    
    getTestResults(
        buildId: string,
        filter:string,
        sortDirection:string, 
        pageSize: number, 
        pageIndex: number
    ): Observable<IPaginatedItems<ITestResult>> {
        return this.http.get<IPaginatedItems<ITestResult>>(
            `${this.baseUrl}testresult/items/build/${buildId}?filter=${filter}&pageSize=${pageSize}&pageIndex=${pageIndex}`)
            .pipe(catchError(this.handleError('getBuilds', null)));
    }



    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            console.error(error);
            return of(result as T);
        };
    }
}