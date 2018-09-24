import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TestsStoreService } from '../../../../core/services/testsstore.service';
import { ITestResult } from 'src/app/core/models/testResult';
import { StatusService } from '../../../../core/services/status.service';

@Component({
  selector: 'app-build-details',
  templateUrl: './build-details.component.html',
  styleUrls: ['./build-details.component.css']
})
export class BuildDetailsComponent implements OnInit {
  testResults: ITestResult[];

  constructor(
    private route: ActivatedRoute,
    private teststoreSerive: TestsStoreService,
    private statusService: StatusService) { }

  ngOnInit() {
    this.getTestResults();
  }

  getTestResults() {
    let buildId: string = this.route.snapshot.paramMap.get('id');
    this.teststoreSerive.getTestResults(buildId).subscribe(testResults => this.testResults = testResults);
  }

  getBackgroundColor(status: string): string {
    return this.statusService.getColor(status);
  }
}
