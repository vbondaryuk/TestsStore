import { Component, OnInit } from '@angular/core';
import { TestsStoreService } from "../../core/services/teststore.service";
import { Project } from '../../core/models/project';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})

export class ProjectsComponent implements OnInit {
  
  projects: Project[];

  constructor(private testsStoreService TestsStoreService) { }

  ngOnInit() {
    this.getProjects();
  }

  getProjects():void{
    this.testsStoreService.
  }
}
