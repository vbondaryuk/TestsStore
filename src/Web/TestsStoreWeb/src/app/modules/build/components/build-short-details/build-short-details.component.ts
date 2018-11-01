import {Component, Input, OnInit} from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {TestsStoreService} from 'src/app/core/services/testsstore.service';
import {StatusService} from 'src/app/core/services/status.service';
import {ITestsSummary} from '../../../../core/models/testSummary';

@Component({
  selector: 'app-build-short-details',
  templateUrl: './build-short-details.component.html',
  styleUrls: ['./build-short-details.component.css']
})

export class BuildShortDetailsComponent implements OnInit {

  @Input()
  buildIdSubject: BehaviorSubject<string>;
  buildId: string;
  private testResultSummary: ITestsSummary[];

  chartData: any[];
  colorScheme = {
    domain: []
  };
  view: any[] = [500, 200];

  constructor(
    private testsStoreService: TestsStoreService,
    private statusService: StatusService) {
  }

  ngOnInit() {
    this.buildIdSubject.subscribe(buildId => {
      if (buildId == null) {
        this.buildId = buildId;
        this.clearChart();
        return;
      }

      if (this.buildId === buildId) {
        return;
      }

      this.buildId = buildId;
      this.clearChart();

      this.testsStoreService.getTestResultsSummary(this.buildId)
        .subscribe(testResultSummary => {
          this.testResultSummary = testResultSummary;
          this.showChart();
        });
    });
  }

  clearChart() {
    this.chartData = [];
    this.colorScheme.domain = [];
  }

  showChart() {
    this.clearChart();

    if (this.testResultSummary.length === 0) {
      return;
    }

    this.testResultSummary.forEach(testSummary => {
      const keyVal = {
        name: testSummary.status,
        value: testSummary.count
      };
      this.chartData.push(keyVal);
      this.colorScheme.domain.push(this.statusService.getColor(testSummary.status));
    });
  }
}
