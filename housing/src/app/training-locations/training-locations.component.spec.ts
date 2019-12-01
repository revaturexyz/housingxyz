import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MatPaginatorModule, MatTableModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { TrainingLocationsComponent } from './training-locations.component';

describe('TrainingLocationsComponent', () => {
  let component: TrainingLocationsComponent;
  let fixture: ComponentFixture<TrainingLocationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        MatPaginatorModule, MatTableModule,
        BrowserAnimationsModule
      ],
      declarations: [ TrainingLocationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrainingLocationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
