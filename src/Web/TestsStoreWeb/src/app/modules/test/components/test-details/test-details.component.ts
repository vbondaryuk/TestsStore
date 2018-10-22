import {Component, OnInit, Input, ViewEncapsulation} from '@angular/core';
import {NgbModalConfig, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ITestResult} from '../../../../core/models/testResult';
import {TestsStoreService} from 'src/app/core/services/testsstore.service';
import {StatusService} from 'src/app/core/services/status.service';

@Component({
  selector: 'app-test-details',
  templateUrl: './test-details.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./test-details.component.css']
})

export class TestDetailsComponent implements OnInit {

  @Input()
  testResult: ITestResult;
  testResults: ITestResult[];

  chartData: any[];
  view: any[] = [];
  xAxisLabel = 'Builds';
  yAxisLabel = 'Duration in seconds';
  colorScheme = {domain: []};
  tickFormatting;

  constructor(
    config: NgbModalConfig,
    private modalService: NgbModal,
    private testStoreService: TestsStoreService,
    private statusService: StatusService) {
    // customize default values of modals used by this component tree
    config.backdrop = 'static';
    config.keyboard = false;
    this.tickFormatting = (series) => {
      const index = series.indexOf('*');
      if (index > 0) {
        return series.substring(0, index);
      }

      return series;
    };
  }

  ngOnInit() {
  }

  open(content) {
    this.modalService.open(content, {size: 'lg', windowClass: 'modal-adaptive'});
    this.showChart();
  }

  showChart(): void {
    this.testStoreService.getTestStatistic(this.testResult.test.id)
      .subscribe(testResults => {
        this.chartData = [];
        this.testResults = testResults;
        this.testResults.forEach(testResult => {
          const keyVal = {
            name: testResult.test.name + '*' + testResult.id,
            value: testResult.duration / 1000
          };
          this.chartData.push(keyVal);
          this.colorScheme.domain.push(this.statusService.getColor(testResult.status));
        });
        this.testResult = testResults.find(x => x.id === this.testResult.id);
      });
  }

  onSelect(item: any) {
    if (item.name.indexOf('*') > 0) {
      const id = item.name.substring(item.name.indexOf('*') + 1);
      this.testResult = this.testResults.find(x => x.id === id);
    }
  }
}
