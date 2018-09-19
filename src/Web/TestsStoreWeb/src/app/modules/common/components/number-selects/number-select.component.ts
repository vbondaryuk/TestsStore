import { Component, OnInit, Input } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
    selector: 'app-number-select',
    templateUrl: './number-select.component.html',
    styleUrls: ['./number-select.component.css']
})

export class NumberSelectComponent implements OnInit {

    ngOnInit(): void {
        this.number = 5;
        this.numberSubject.next(this.number);
    }

    @Input()
    placeholder: string;

    @Input() numberSubject: Subject<number>;
    
    number: number;

    numbers: number[] = [ 5, 10, 15 ];

    onSelectionChange(change: any) {//EventEmitter<MatSelectChange>){
        this.numberSubject.next(change.value);
      }
}