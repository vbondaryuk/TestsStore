import { Component, OnInit, Input, OnChanges, SimpleChanges, EventEmitter } from '@angular/core';
import { TestsStoreService } from 'src/app/core/services/testsstore.service';
import { IProject } from '../../core/models/project';
import { IBuild } from '../../core/models/build';
import { StatusService } from '../../core/services/status.service';
import { MatSelectChange } from '@angular/material/select/index';
import { Subject } from 'rxjs';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-builds',
  templateUrl: './builds.component.html',
  styleUrls: ['./builds.component.css']
})

export class BuildsComponent implements OnChanges, OnInit {

  @Input()
  project: IProject;

  builds: IBuild[] = [];
  selectedBuild: IBuild;
  
  displayedColumns: string[] = ['name', 'status'];
  dataSource = new MatTableDataSource(this.builds);

  chartBuildsCountSubject = new Subject<number>();
  chartBuildsCount: number;

  ////
  chartData: any[];
  view: any[] = [700, 400];
  xAxisLabel = 'Builds';
  yAxisLabel = 'Duration in seconds';
  colorScheme = { domain: [] };
  tickFormatting;
  ////

  constructor(
    private testsStoreService: TestsStoreService,
    private statusService: StatusService
  ) {
    this.tickFormatting = (series) => {
      var index = series.indexOf("*");
      if (index > 0) {
        return series.substring(0, index);
      }

      return series;
    }
  }

  ngOnInit() {
    this.chartBuildsCountSubject.subscribe((count: number) => {
      this.chartBuildsCount = count;
      this.showChart();
    });
    this.getBuilds();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['project']) {
      this.getBuilds();
    } if (changes['number']) {
      this.showChart();
    }
  }


  getBuilds(): void {
    this.testsStoreService.getBuilds(this.project.id)
      .subscribe(builds => {
        this.builds = builds;
        this.dataSource = new MatTableDataSource(this.builds);
        this.showChart();
      });
  }

  showChart(): void {
    this.chartData = [];
    this.colorScheme.domain = [];

    if (this.builds.length == 0) {
      return;
    }
    
    for (let i = 0; i < this.builds.length; i++) {
      if (i > this.chartBuildsCount) {
        break;
      }

      let build = this.builds[i];
      const keyVal = new KeyValue();
      keyVal.name = build.name + "*" + build.id;
      keyVal.value = build.duration;
      this.chartData.push(keyVal);

      this.colorScheme.domain.push(this.statusService.getColor(build.status));
    }
    
    this.chartData = this.chartData.reverse();
    this.colorScheme.domain = this.colorScheme.domain.reverse();
  }

  onRowClicked(item: IBuild) {
    this.selectedBuild = item;
  }

  onSelect(event) {
    console.log(event);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}

export class KeyValue {
  id: string;
  name: string;
  value: number;
}
