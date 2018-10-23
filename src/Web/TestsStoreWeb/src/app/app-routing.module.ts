import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ProjectsComponent} from 'src/app/modules/project/components/projects/projects.component';
import {BuildDetailsComponent} from 'src/app/modules/build/components/build-details/build-details.component';
import {UploadComponent} from './modules/upload/upload/upload.component';


const routes: Routes = [
  {path: '', redirectTo: 'projects', pathMatch: 'full'},
  {path: '**', redirectTo: 'welcome', pathMatch: 'full'},
  {path: 'projects', component: ProjectsComponent},
  {path: 'upload', component: UploadComponent},
  {path: 'build/:id', component: BuildDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
