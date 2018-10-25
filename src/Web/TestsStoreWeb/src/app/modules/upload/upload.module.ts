import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {UploadComponent} from './upload/upload.component';
import {DialogComponent} from './dialog/dialog.component';
import {CommonAppModule} from '../common/common.module';

@NgModule({
  imports: [
    CommonModule,
    CommonAppModule
  ],
  declarations: [UploadComponent, DialogComponent],
  exports: [UploadComponent],
  entryComponents: [DialogComponent]
})
export class UploadModule {
}
