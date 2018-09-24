import { Component, OnInit, Input, HostListener } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';
import { IBuild } from 'src/app/core/models/build';

@Component({
  selector: 'app-builds',
  templateUrl: './builds.component.html',
  styleUrls: ['./builds.component.css']
})

export class BuildsComponent implements OnInit {

  @Input()
  projectIdSubject: Subject<string>;

  buildsSubject = new BehaviorSubject<IBuild[]>([]);
  buildIdSubject = new BehaviorSubject<string>(null);

  halfHeight: number;

  constructor() { }

  ngOnInit() {
    //this.onResize();
  }

  @HostListener('window:resize')
  onResize() {
    let navbarElement = document.getElementsByClassName('navbar')[0];
    
    this.halfHeight = (window.innerHeight - navbarElement.clientHeight)  / 2;
  }
  
}