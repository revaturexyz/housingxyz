import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CoordinatorNotificationsComponent } from './coordinator-notifications.component';

describe('CoordinatorNotificationsComponent', () => {
  let component: CoordinatorNotificationsComponent;
  let fixture: ComponentFixture<CoordinatorNotificationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
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
