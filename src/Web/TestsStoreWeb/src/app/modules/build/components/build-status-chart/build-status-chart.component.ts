import {Component, Input, OnInit} from '@angular/core';
import {BehaviorSubject, Subject} from 'rxjs';
import {IBuild} from 'src/app/core/models/build';
import {StatusService} from 'src/app/core/services/status.service';

@Component({
  selector: 'app-build-status-chart',
  templateUrl: './build-status-chart.component.html',
  styleUrls: ['./build-status-chart.component.css']
})
export class BuildStatusChartComponent implements OnInit {

  @Input()
  buildsSubject: BehaviorSubject<IBuild[]>;
  @Input()
  buildIdSubject: BehaviorSubject<string>;
  builds: IBuild[] = [];
  chartBuildsCountSubject = new Subject<number>();
  chartBuildsCount = 5;

  ////
  chartData: any[];
  view: any[] = [];
  xAxisLabel = 'Builds';
  yAxisLabel = 'Duration in seconds';
  colorScheme = {domain: []};
  tickFormatting;

  ////

  constructor(private statusService: StatusService) {
    this.tickFormatting = (series) => {
      const index = series.indexOf('*');
      if (index > 0) {
        return series.substring(0, index);
      }

      return series;
    };
  }

  ngOnInit() {
    this.chartBuildsCountSubject.subscribe((count: number) => {
      this.chartBuildsCount = count;
      this.showChart();
    });
    this.buildsSubject.subscribe((builds: IBuild[]) => {
      this.setBuilds(builds);
    });

    this.resizeChart();
    this.setBuilds(this.buildsSubject.value);
  }

  setBuilds(builds: IBuild[]) {
    this.builds = builds;
    this.showChart();
  }

  showChart(): void {
    this.chartData = [];
    this.colorScheme.domain = [];

    if (this.builds.length === 0) {
      return;
    }

    for (let i = 0; i < this.builds.length; i++) {
      if (i > this.chartBuildsCount) {
        break;
      }

      const build = this.builds[i];
      const keyVal = {
        name: build.name + '*' + build.id,
        value: build.duration
      };
      this.chartData.push(keyVal);

      this.colorScheme.domain.push(this.statusService.getColor(build.status));
    }

    this.chartData = this.chartData.reverse();
    this.colorScheme.domain = this.colorScheme.domain.reverse();
  }

  onResize(event) {
    this.resizeChart();
  }

  onSelect(item: any) {
    if (item.name.indexOf('*') > 0) {
      const id = item.name.substring(item.name.indexOf('*') + 1);
      this.buildIdSubject.next(id);
    }
  }

  resizeChart() {
    const element = document.getElementsByClassName('build__status__chart')[0];
    if (!element) {
      return;
    }

    const width = element.clientWidth - 30;
    const height = ((window.innerHeight - 60) / 2) - 50;


    this.view = [width, height];
  }
}
