import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { NumberSelectComponent } from 'src/app/modules/common/components/number-selects/number-select.component';
import { FilterPipe } from './pipes/filter.pipe';

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
        MatToolbarModule
    ],
    exports: [
        BrowserAnimationsModule,
        NgbModule,
        // MatListModule,
        // MatSelectModule,
        // MatTableModule,
        // MatInputModule,
        // MatToolbarModule,
        NumberSelectComponent,
        NgxChartsModule,
        FilterPipe
    ],
    declarations: [
        NumberSelectComponent,
        FilterPipe
    ]
})

export class CommonAppModule { }