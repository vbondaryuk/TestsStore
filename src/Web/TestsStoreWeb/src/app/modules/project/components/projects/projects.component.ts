import { Component, OnInit } from '@angular/core';
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
  
  projects: IProject[] = [];
  projectIdSubject = new Subject<string>();

  displayedColumns: string[] = ['name'];
  dataSource = new MatTableDataSource(this.projects);

  constructor(private testsStoreService: TestsStoreService) { }

  ngOnInit() {
    this.dataSource.filterPredicate = (data: IProject, filter: string) => data.name.toLowerCase().indexOf(filter) != -1;
    this.getProjects();
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
