import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from 'src/app/modules/project/components/projects/projects.component';
import { BuildDetailsComponent } from 'src/app/modules/build/components/build-details/build-details.component';


const routes: Routes = [
  { path: '', redirectTo: '/projects', pathMatch: 'full' },
  { path: 'projects', data: {breadcrumb: 'projects'}, component: ProjectsComponent },
  { path: 'build/:id', data: {breadcrumb: 'build'}, component: BuildDetailsComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
