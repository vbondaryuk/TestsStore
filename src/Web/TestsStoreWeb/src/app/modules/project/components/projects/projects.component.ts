import { Component, OnInit, HostListener } from '@angular/core';
import { MatTableDataSource } from '@angular/material';
import { Subject } from 'rxjs';
import { IProject } from 'src/app/core/models/project';
import { TestsStoreService } from 'src/app/core/services/testsstore.service';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})

export class ProjectsComponent implements OnInit {
  
  searchString: string;
  projects: IProject[] = [];
  projectIdSubject = new Subject<string>();

  tableContainerHeight: number;
  displayedColumns: string[] = ['name'];
  dataSource = new MatTableDataSource(this.projects);

  constructor(private testsStoreService: TestsStoreService) { 
    this.dataSource.filterPredicate = (data: IProject, filter: string) => data.name.toLowerCase().indexOf(filter) != -1;
  }

  ngOnInit() {
    this.onResize();    
    this.getProjects();
  }

  @HostListener('window:resize')
  onResize() {
    let navbarElement = document.getElementsByClassName('navbar')[0];
    let projectListHeaderElement = document.getElementsByClassName('projects__list-header')[0];
    let projectListFilterElement = document.getElementsByClassName('projects__list-filter')[0];
    
    this.tableContainerHeight = window.innerHeight - navbarElement.clientHeight - projectListHeaderElement.clientHeight - projectListFilterElement.clientHeight - 55;
  }

  getProjects(): void {
    this.testsStoreService.getProjects()
      .subscribe(projects => {
        this.projects = projects;
        this.dataSource.data = this.projects;
      });
      
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(item: IProject) {
    this.projectIdSubject.next(item.id);
  }
}
