import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { BuildsComponent } from './components/builds/builds.component';
import { BuildDetailsComponent } from './components/build-details/build-details.component';
import { BuildStatusChartComponent } from './components/build-status-chart/build-status-chart.component';
import { CommonAppModule } from 'src/app/modules/common/common.module';
import { BuildShortDetailsComponent } from 'src/app/modules/build/components/build-short-details/build-short-details.component';
import { BuildListComponent } from 'src/app/modules/build/components/build-list/build-list.component';
import { TestModule } from '../test/test.module';

@NgModule({
  imports: [
    CommonModule,
    CommonAppModule,
    FormsModule,
    RouterModule,
    TestModule
  ],
  exports: [
    BuildsComponent,
    BuildDetailsComponent,
    BuildStatusChartComponent,
    BuildShortDetailsComponent,
    BuildListComponent
  ],
  declarations: [
    BuildsComponent,
    BuildDetailsComponent,
    BuildStatusChartComponent,
    BuildShortDetailsComponent,
    BuildListComponent
  ]
})
export class BuildModule { }
