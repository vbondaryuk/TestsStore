import {Component, HostListener, Input, OnInit} from '@angular/core';
import {TestsStoreService} from 'src/app/core/services/testsstore.service';
import {MatTableDataSource} from '@angular/material';
import {IBuild} from 'src/app/core/models/build';
import {BehaviorSubject, Subject} from 'rxjs';
import {StatusService} from 'src/app/core/services/status.service';

@Component({
  selector: 'app-build-list',
  templateUrl: './build-list.component.html',
  styleUrls: ['./build-list.component.css']
})
export class BuildListComponent implements OnInit {

  searchString: string;

  @Input()
  projectIdSubject: Subject<string>;
  @Input()
  buildsSubject: BehaviorSubject<IBuild[]>;
  @Input()
  buildIdSubject: BehaviorSubject<string>;

  projectId: string;
  tableContainerHeight: number;

  displayedColumns: string[] = ['name', 'status'];
  builds: IBuild[] = [];

  dataSource = new MatTableDataSource(this.builds);

  constructor(
    private testsStoreService: TestsStoreService,
    private statusService: StatusService) {
    this.dataSource.filterPredicate = (data: IBuild, filter: string) =>
      data.name.toLowerCase().indexOf(filter) !== -1 || data.status.name.toLowerCase().indexOf(filter) !== -1;
  }

  ngOnInit() {
    this.projectIdSubject.subscribe((projectId: string) => {
      this.projectId = projectId;
      this.buildIdSubject.next(null);
      this.getBuilds();
    });
    this.onResize();
  }

  @HostListener('window:resize')
  onResize() {
    const navbarElement = document.getElementsByClassName('navbar')[0];
    const buildListHeaderElement = document.getElementsByClassName('build-list__header')[0];
    const buildListFilterElement = document.getElementsByClassName('build-list__filter')[0];

    this.tableContainerHeight = window.innerHeight - navbarElement.clientHeight - buildListHeaderElement.clientHeight - buildListFilterElement.clientHeight - 55;
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
    this.buildIdSubject.next(id);
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getBackgroundColor(status: string): string {
    return this.statusService.getColor(status);
  }
}
