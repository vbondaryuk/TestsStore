import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatSelectModule} from '@angular/material/select';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {NgxChartsModule} from '@swimlane/ngx-charts';

import {NumberSelectComponent} from 'src/app/modules/common/components/number-selects/number-select.component';
import {FilterPipe} from './pipes/filter.pipe';
import {MatButtonModule} from '@angular/material';
import {MatDialogModule} from '@angular/material/Dialog';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {FlexLayoutModule} from '@angular/flex-layout';

@NgModule({
  imports: [
    NgbModule,
    CommonModule,
    FormsModule,
    BrowserAnimationsModule,
    MatListModule,
    MatSelectModule,
    MatTableModule,
    MatInputModule,
    MatToolbarModule,
    MatPaginatorModule,
    MatProgressSpinnerModule
  ],
  exports: [
    BrowserAnimationsModule,
    NgbModule,
    MatListModule,
    MatSelectModule,
    MatTableModule,
    MatInputModule,
    MatToolbarModule,
    NumberSelectComponent,
    NgxChartsModule,
    FilterPipe,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatDialogModule,
    MatListModule,
    MatProgressBarModule,
    FlexLayoutModule,
    BrowserAnimationsModule
  ],
  declarations: [
    NumberSelectComponent,
    FilterPipe
  ]
})

export class CommonAppModule {
}
