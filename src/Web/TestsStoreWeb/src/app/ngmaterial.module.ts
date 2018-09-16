import { NgModule } from '@angular/core';

import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        BrowserAnimationsModule,
        MatListModule,
        MatSelectModule
    ],
    exports: [
        BrowserAnimationsModule,
        MatListModule,
        MatSelectModule
    ],
})
export class NgMaterialModule { }