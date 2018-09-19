import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { BuildsComponent } from './components/builds/builds.component';
import { BuildDetailsComponent } from './components/build-details/build-details.component';
import { BuildStatusChartComponent } from './components/build-status-chart/build-status-chart.component';
import { CommonAppModule } from 'src/app/modules/common/common.module';

@NgModule({
  imports: [
    CommonModule,
    CommonAppModule
  ],
  exports: [
    BuildsComponent,
    BuildDetailsComponent,
    BuildStatusChartComponent,
  ],
  declarations: [
    BuildsComponent,
    BuildDetailsComponent,
    BuildStatusChartComponent
  ]
})
export class BuildModule { }
