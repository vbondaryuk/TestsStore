import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
import { TestsStoreService } from 'src/app/core/services/testsstore.service';
import { IProject } from '../../core/models/project';
import { IBuild } from '../../core/models/build';

@Component({
  selector: 'app-builds',
  templateUrl: './builds.component.html',
  styleUrls: ['./builds.component.css']
})

export class BuildsComponent implements OnChanges, OnInit {

  @Input()
  project: IProject;

  builds: IBuild[];
  selectedBuild: IBuild;

  ////
  single: any[];
  multi: any[];

  view: any[] = [700, 400];

  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Builds';
  showYAxisLabel = true;
  yAxisLabel = 'Duration in seconds';

  colorScheme = {
    domain: []
  };
  ////

  constructor(private testsStoreService: TestsStoreService) {
    this.tickFormatting = (series) => {
      var index = series.indexOf( "*" );
      console.log(index);
      if (index > 0) {
        return series.substring(0, index); 
      }
      
      return series;
    }
   }

  ngOnInit() {
    this.getBuilds();
  }

  ngOnChanges(changes: SimpleChanges) {
    this.getBuilds();
  }


  getBuilds(): void {
    this.testsStoreService.getBuilds(this.project.id)
      .subscribe(builds => {
        this.builds = builds;
        this.showChart();
      });

    
  }

  showChart(): void{
    this.single = [];
    this.colorScheme.domain = [];

    if (this.builds.length == 0) {
      return;
    }
    
    let counter = 1;
    for (let build of this.builds) {
      const keyVal = new KeyValue();
      keyVal.id = build.id;
      keyVal.name = build.name +"*"+ counter;
      keyVal.value = build.duration;
      this.single.push(keyVal);

      this.colorScheme.domain.push(build.status.name !== "Passed" ? "red" : "green");
      counter++;
    }
  }
  onRowClicked(item: IBuild) {
    this.selectedBuild = item;
    console.log(item);
  }
  tickFormatting;
  onSelect(event) {
    console.log(event);
  }

}

export class KeyValue {
  id: string;
  name: string;
  value: number;
}
