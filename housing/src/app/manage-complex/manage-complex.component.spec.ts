import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageComplexComponent } from './manage-complex.component';

describe('ManageComplexComponent', () => {
  let component: ManageComplexComponent;
  let fixture: ComponentFixture<ManageComplexComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageComplexComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageComplexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
