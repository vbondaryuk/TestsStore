import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CommonAppModule } from 'src/app/modules/common/common.module';
import { ProjectsComponent } from 'src/app/modules/project/components/projects/projects.component';
import { BuildModule } from 'src/app/modules/build/build.module';

@NgModule({
  imports: [
    CommonModule,
    CommonAppModule,
    FormsModule,
    BuildModule
  ],
  exports: [
    ProjectsComponent
  ],
  declarations: [
    ProjectsComponent
  ]
})
export class ProjectModule { }
