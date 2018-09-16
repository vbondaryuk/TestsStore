import { Component, OnInit } from '@angular/core';
import { TestsStoreService } from "../../core/services/testsstore.service";
import { IProject } from '../../core/models/project';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})

export class ProjectsComponent implements OnInit {

  projects: IProject[];
  selectedProject: IProject;

  constructor(private testsStoreService: TestsStoreService) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects(): void {
    this.testsStoreService.getProjects()
      .subscribe(projects => this.projects = projects);
  }

  onRowClicked(item: IProject) {
    this.selectedProject = item;
    console.log(item);
  }
}
