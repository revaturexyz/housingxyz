import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexDetailsComponent } from './complex-details.component';

describe('ComplexDetailsComponent', () => {
  let component: ComplexDetailsComponent;
  let fixture: ComponentFixture<ComplexDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ComplexDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComplexDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
