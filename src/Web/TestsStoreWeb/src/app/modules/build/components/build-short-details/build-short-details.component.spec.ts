import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildShortDetailsComponent } from './build-short-details.component';

describe('BuildShortDetailsComponent', () => {
  let component: BuildShortDetailsComponent;
  let fixture: ComponentFixture<BuildShortDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuildShortDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildShortDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
