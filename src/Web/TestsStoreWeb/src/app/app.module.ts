import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';

import { TestsComponent } from './components/tests/tests.component';
import { TestDetailsComponent } from './components/test-details/test-details.component';
import { BuildsComponent } from './components/builds/builds.component';
import { ProjectsComponent } from './components/projects/projects.component';
import { HttpClientModule } from '@angular/common/http';
import { NgMaterialModule } from './ngmaterial.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { NumberSelectComponent } from './components/number-selects/number-select.component';
import { BuildDetailsComponent } from './components/build-details/build-details.component';

@NgModule({
  declarations: [
    AppComponent,
    TestsComponent,
    TestDetailsComponent,
    BuildsComponent,
    BuildDetailsComponent,
    ProjectsComponent,
    NumberSelectComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,    
    HttpClientModule,
    FormsModule,
    NgMaterialModule,
    NgxChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
