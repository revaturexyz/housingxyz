import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AmenityDialogueComponent } from './amenity-dialogue.component';

describe('AmenityDialogueComponent', () => {
  let component: AmenityDialogueComponent;
  let fixture: ComponentFixture<AmenityDialogueComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AmenityDialogueComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AmenityDialogueComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
