import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestsComponent } from './components/tests/tests.component';
import { TestDetailsComponent } from './components/test-details/test-details.component';


@NgModule({
  imports: [
    CommonModule
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
