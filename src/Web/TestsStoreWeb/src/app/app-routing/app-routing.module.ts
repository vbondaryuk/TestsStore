import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProjectsComponent } from '../components/projects/projects.component';
import { BuildsComponent } from '../components/builds/builds.component';

const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', data: {breadcrumb: 'projects'}, component: ProjectsComponent },
  { path: 'builds/:id', data: {breadcrumb: 'builds'}, component: BuildsComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
