import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoordinatorNotificationsComponent } from './coordinator-notifications.component';

import { MatTableModule, MatPaginatorModule } from '@angular/material';

describe('CoordinatorNotificationsComponent', () => {
  let component: CoordinatorNotificationsComponent;
  let fixture: ComponentFixture<CoordinatorNotificationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ MatTableModule, MatPaginatorModule, RouterTestingModule, BrowserAnimationsModule ],
      declarations: [ CoordinatorNotificationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CoordinatorNotificationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
