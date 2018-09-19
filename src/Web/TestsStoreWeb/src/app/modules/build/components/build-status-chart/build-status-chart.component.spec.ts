import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BuildStatusChartComponent } from './build-status-chart.component';

describe('BuildStatusChartComponent', () => {
  let component: BuildStatusChartComponent;
  let fixture: ComponentFixture<BuildStatusChartComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BuildStatusChartComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BuildStatusChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
