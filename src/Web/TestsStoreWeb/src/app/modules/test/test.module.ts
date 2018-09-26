import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestsComponent } from './components/tests/tests.component';
import { TestDetailsComponent } from './components/test-details/test-details.component';
import { CommonAppModule } from '../common/common.module';


@NgModule({
  imports: [
    CommonModule,
    CommonAppModule
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
