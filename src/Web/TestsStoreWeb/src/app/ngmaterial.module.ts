import { NgModule } from '@angular/core';

import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        // BrowserAnimationsModule,
        // MatListModule,
        // MatSelectModule,
        // MatTableModule
    ],
    exports: [
        BrowserAnimationsModule,
        MatListModule,
        MatSelectModule,
        MatTableModule,
        MatInputModule
    ],
})

export class NgMaterialModule { }