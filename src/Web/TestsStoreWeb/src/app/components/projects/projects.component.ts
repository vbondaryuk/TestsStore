import { Component, OnInit } from '@angular/core';
import { TestsStoreService } from "../../core/services/testsstore.service";
import { IProject } from '../../core/models/project';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})

export class ProjectsComponent implements OnInit {
  
  projects: IProject[] = [];
  selectedProject: IProject;

  displayedColumns: string[] = ['name'];
  dataSource = new MatTableDataSource(this.projects);

  constructor(private testsStoreService: TestsStoreService) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects(): void {
    this.testsStoreService.getProjects()
      .subscribe(projects => {
        this.projects = projects;
        this.dataSource = new MatTableDataSource(this.projects);
      });
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  onRowClicked(item: IProject) {
    this.selectedProject = item;
  }
}
