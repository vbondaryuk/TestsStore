import {Component, OnInit, ViewChild, ElementRef} from '@angular/core';
import {ActivatedRoute, ParamMap} from '@angular/router';
import {TestsStoreService} from '../../../../core/services/testsstore.service';
import {ITestResult} from 'src/app/core/models/testResult';
import {StatusService} from '../../../../core/services/status.service';
import {trigger, state, transition, style, animate} from '@angular/animations';
import {MatPaginator} from '@angular/material';
import {tap, distinctUntilChanged, debounceTime} from 'rxjs/operators';
import {TestResultDataSource} from '../../services/testresult.datasource';
import {fromEvent} from 'rxjs';
import {IBuildDetails} from '../../../../core/models/buildDetails';

@Component({
  selector: 'app-build-details',
  templateUrl: './build-details.component.html',
  styleUrls: ['./build-details.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0', display: 'none'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class BuildDetailsComponent implements OnInit {

  buildId: string;
  buildDetails: IBuildDetails;
  expandedTestResult: ITestResult;

  dataSource: TestResultDataSource;
  displayedColumns = ['className', 'name', 'status'];
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild('input') input: ElementRef;

  chartData: any[];
  colorScheme = {
    domain: []
  };

  constructor(
    private route: ActivatedRoute,
    private testsStoreService: TestsStoreService,
    private statusService: StatusService) {
    this.buildId = this.route.snapshot.paramMap.get('id');
    this.route.paramMap.subscribe((params: ParamMap) => {
      const buildId = params.get('id');
      if (this.buildId !== buildId) {
        this.buildId = buildId;
        this.paginator.pageIndex = 0;
        this.loadTestResultsPage();
        this.populateChart();
      }
    });
  }

  ngOnInit() {
    this.setTestResultDataSource();
    this.populateChart();
  }

  setTestResultDataSource() {
    this.dataSource = new TestResultDataSource(this.testsStoreService);
    this.dataSource.loadTestResults(this.buildId, '', 'asc', 0, 10);
    this.paginator.page
      .pipe(tap(() => this.loadTestResultsPage()))
      .subscribe();

    fromEvent(this.input.nativeElement, 'keyup')
      .pipe(
        debounceTime(150),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.loadTestResultsPage();
        })
      )
      .subscribe();
  }

  populateChart() {
    this.testsStoreService.getBuildDetails(this.buildId)
      .subscribe(buildDetals => {
        this.buildDetails = buildDetals;

        this.chartData = [];
        this.colorScheme.domain = [];
        this.buildDetails.testsSummary.forEach(testSummary => {
          const keyVal = {
            name: testSummary.status,
            value: testSummary.count
          };
          this.chartData.push(keyVal);
          this.colorScheme.domain.push(this.statusService.getColor(testSummary.status));
        });
      });
  }

  loadTestResultsPage() {
    this.dataSource.loadTestResults(
      this.buildId,
      this.input.nativeElement.value,
      // this.sort.direction,
      '',
      this.paginator.pageIndex,
      this.paginator.pageSize);
  }

  getBackgroundColor(status: string): string {
    return this.statusService.getColor(status);
  }
}
