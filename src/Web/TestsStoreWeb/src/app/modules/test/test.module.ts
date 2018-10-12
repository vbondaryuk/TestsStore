import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TestsComponent } from './components/tests/tests.component';
import { TestDetailsComponent } from './components/test-details/test-details.component';
import { CommonAppModule } from '../common/common.module';


@NgModule({
  imports: [
    CommonModule,
    CommonAppModule,
    RouterModule
  ],
  exports:[
    TestsComponent,
    TestDetailsComponent
  ],
  declarations: [
    TestsComponent,
    TestDetailsComponent
  ]
})
export class TestModule { }
