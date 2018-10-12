import {CollectionViewer, DataSource} from '@angular/cdk/collections';
import {ITestResult} from 'src/app/core/models/testResult';
import {BehaviorSubject, of, Observable} from 'rxjs';
import {TestsStoreService} from '../../../core/services/testsstore.service';
import {catchError, finalize, map} from 'rxjs/operators';


export class TestResultDataSource implements DataSource<ITestResult> {

  private testResultsSubject = new BehaviorSubject<ITestResult[]>([]);
  private lengthSubject = new BehaviorSubject<number>(0);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public length$ = this.lengthSubject.asObservable();
  public loading$ = this.loadingSubject.asObservable();


  constructor(private testStoreService: TestsStoreService) {

  }

  loadTestResults(buildId: string,
                  filter: string,
                  sortDirection: string,
                  pageIndex: number,
                  pageSize: number) {

    this.loadingSubject.next(true);

    this.testStoreService.getTestResults(buildId, filter, sortDirection,
      pageSize, pageIndex).pipe(
      map(data => {
        this.lengthSubject.next(data.count);
        return data.data;
      }),
      catchError(() => of([])),
      finalize(() => this.loadingSubject.next(false))
    )
      .subscribe(items => {
          this.testResultsSubject.next(items);
        }
      );
  }

  connect(collectionViewer: CollectionViewer): Observable<ITestResult[]> {
    console.log('Connecting data source');
    return this.testResultsSubject.asObservable();
  }

  disconnect(collectionViewer: CollectionViewer): void {
    this.testResultsSubject.complete();
    this.loadingSubject.complete();
    this.lengthSubject.complete();
  }
}
