import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../../environments/environment';
import {IProject} from '../models/project';
import {Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';
import {IBuild} from '../models/build';
import {IBuildDetails} from 'src/app/core/models/buildDetails';
import {ITestResult} from '../models/testResult';
import {IPaginatedItems} from '../models/paginatedItems';
import {ITestsSummary} from '../models/testSummary';

@Injectable({providedIn: 'root'})
export class TestsStoreService {

  baseUrl = environment.baseUrl + 'api/';

  constructor(private http: HttpClient) {
  }

  getProjects(): Observable<IProject[]> {
    return this.http.get<IProject[]>(this.baseUrl + 'project/items')
      .pipe(catchError(this.handleError<IProject[]>('getProjects')));
  }

  getBuilds(projectId: string): Observable<IBuild[]> {
    return this.http.get<IBuild[]>(this.baseUrl + 'build/project/' + projectId)
      .pipe(catchError(this.handleError<IBuild[]>('getBuilds')));
  }

  getBuild(buildId: string): Observable<IBuild> {
    return this.http.get<IBuild>(this.baseUrl + 'build/id/' + buildId)
      .pipe(catchError(this.handleError<IBuild>('getBuild')));
  }

  getBuildDetails(buildId: string): Observable<IBuildDetails> {
    return this.http.get<IBuildDetails>(this.baseUrl + 'build/id/' + buildId + '/details')
      .pipe(catchError(this.handleError<IBuildDetails>('getBuildDetails')));
  }

  getTestResultsSummary(buildId: string): Observable<ITestsSummary[]> {
    return this.http.get<ITestsSummary[]>(`${this.baseUrl}testresult/summary/build/${buildId}`)
      .pipe(catchError(this.handleError<ITestsSummary[]>('getBuildDetails')));
  }

  getTestStatistic(testId: string): Observable<ITestResult[]> {
    return this.http.get<ITestResult[]>(
      `${this.baseUrl}testresult/items/test/${testId}/statistic`)
      .pipe(catchError(this.handleError<ITestResult[]>('getTestStatistic')));
  }

  getTestResults(
    buildId: string,
    filter: string,
    sortDirection: string,
    pageSize: number,
    pageIndex: number
  ): Observable<IPaginatedItems<ITestResult>> {
    return this.http.get<IPaginatedItems<ITestResult>>(
      `${this.baseUrl}testresult/items/build/${buildId}?filter=${filter}&pageSize=${pageSize}&pageIndex=${pageIndex}`)
      .pipe(catchError(this.handleError<IPaginatedItems<ITestResult>>('getBuilds')));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.log(`${operation} failed: ${error.message}`);
      // console.error(error.error);
      return of(result as T);
    };
  }
}
