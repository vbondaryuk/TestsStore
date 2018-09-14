import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { TestsComponent } from './components/tests/tests.component';
import { TestDetailsComponent } from './components/test-details/test-details.component';
import { BuildsComponent } from './components/builds/builds.component';
import { ProjectsComponent } from './components/projects/projects.component';

@NgModule({
  declarations: [
    AppComponent,
    TestsComponent,
    TestDetailsComponent,
    BuildsComponent,
    ProjectsComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
