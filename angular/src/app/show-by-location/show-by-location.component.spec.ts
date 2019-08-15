import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowByLocationComponent } from './show-by-location.component';

describe('ShowByLocationComponent', () => {
  let component: ShowByLocationComponent;
  let fixture: ComponentFixture<ShowByLocationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowByLocationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowByLocationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
