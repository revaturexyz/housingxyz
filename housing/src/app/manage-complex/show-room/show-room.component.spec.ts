import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MatChipsModule } from '@angular/material/chips';
import { ShowRoomComponent } from './show-room.component';

describe('ShowRoomComponent', () => {
  let component: ShowRoomComponent;
  let fixture: ComponentFixture<ShowRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MatChipsModule],
      declarations: [ShowRoomComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
