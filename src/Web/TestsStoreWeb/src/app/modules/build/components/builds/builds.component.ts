import { Component, OnInit, Input } from '@angular/core';
import { TestsStoreService } from 'src/app/core/services/testsstore.service';
import { Router } from "@angular/router";
import { Subject, BehaviorSubject } from 'rxjs';
import { MatTableDataSource } from '@angular/material';
import { IProject } from 'src/app/core/models/project';
import { IBuild } from 'src/app/core/models/build';

@Component({
  selector: 'app-builds',
  templateUrl: './builds.component.html',
  styleUrls: ['./builds.component.css']
})

export class BuildsComponent implements OnInit {

  @Input()
  projectIdSubject: Subject<string>;
  projectId: string;

  builds: IBuild[] = [];
  buildsSubject = new BehaviorSubject<IBuild[]>(this.builds);

  displayedColumns: string[] = ['name', 'status'];
  dataSource = new MatTableDataSource(this.builds);

  constructor(
    private router: Router,
    private testsStoreService: TestsStoreService
  ) {
    this.dataSource.filterPredicate = (data: IBuild, filter: string) => data.name.toLowerCase().indexOf(filter) != -1 || data.status.name.toLowerCase().indexOf(filter) != -1;
  }

  ngOnInit() {
    this.projectIdSubject.subscribe((projectId: string) => {
      this.projectId = projectId;
      this.getBuilds();
    });
  }

  getBuilds(): void {
    this.testsStoreService.getBuilds(this.projectId)
      .subscribe(builds => {
        this.builds = builds;
        this.dataSource.data = builds;
        this.buildsSubject.next(builds);
      });
  }

  onRowClicked(item: IBuild) {
    this.openBuildDetails(item.id);
  }

  openBuildDetails(id: string) {
    this.router.navigate(['build/' + id]);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}