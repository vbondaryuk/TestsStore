import { Component, OnInit, Input } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { TestsStoreService } from 'src/app/core/services/testsstore.service';
import { IBuildDetails } from 'src/app/core/models/buildDetails';
import { StatusService } from 'src/app/core/services/status.service';

@Component({
  selector: 'app-build-short-details',
  templateUrl: './build-short-details.component.html',
  styleUrls: ['./build-short-details.component.css']
})

export class BuildShortDetailsComponent implements OnInit {

  @Input()
  buildIdSubject: BehaviorSubject<string>;
  buildId: string;
  buildDetails: IBuildDetails;

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

      if (this.buildId == buildId) {
        return;
      }

      this.buildId = buildId;
      this.clearChart(); 
      
      this.testsStoreService.getBuildDetails(this.buildId)
        .subscribe(buildDetals => {
          this.buildDetails = buildDetals;
          this.showChart();
        });
    })
  }

  onSelect(event) {
    console.log(event);
  }

  clearChart() {
    this.chartData = [];
    this.colorScheme.domain = [];
  }

  showChart() {
    this.clearChart();

    if (this.buildDetails.testsSummary.length == 0) {
      return;
    }

    this.buildDetails.testsSummary.forEach(testSummary => {
      const keyVal = {
        name: testSummary.status,
        value: testSummary.count
      };
      this.chartData.push(keyVal);
      this.colorScheme.domain.push(this.statusService.getColor(testSummary.status));
    });
  }
}
