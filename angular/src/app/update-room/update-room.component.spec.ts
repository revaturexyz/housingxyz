import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { UpdateRoomComponent } from './update-room.component';
import { RoomUpdateFormComponent } from '../room-update-form/room-update-form.component';
import { RoomDetailsComponent } from '../room-details/room-details.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Complex } from 'src/interfaces/complex';
import { TestServiceData } from '../services/static-test-data';
import { DebugElement } from '@angular/core';
import { Room } from 'src/interfaces/room';

describe('UpdateRoomComponent', () => {
  let component: UpdateRoomComponent;
  let fixture: ComponentFixture<UpdateRoomComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UpdateRoomComponent, RoomDetailsComponent, RoomUpdateFormComponent ],
      imports: [ HttpClientTestingModule ]
    })
    .overrideComponent(RoomUpdateFormComponent, {
        set: { template: '<div></div>'}}
    )
    .overrideComponent(RoomDetailsComponent, {
        set: { template: '<div></div>'}}
    );
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRoomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeDefined();
    expect(component.complexList).toBeTruthy();
    expect(component.roomList).toBeTruthy();
    expect(component.activeComplex).toBeFalsy();
    expect(component.showString).toEqual('Choose Complex');
    expect(component.complexRooms).toBeFalsy();
    expect(component.mouseOverRoom).toBeFalsy();
    expect(component.selectedRoom).toBeFalsy();
    expect(component.highlightRoom).toBeFalsy();
  });

  it('should initialize correctly', () => {
    expect(component.complexList).toBeTruthy();
    expect(component.roomList).toBeTruthy();
  });


  it('should choose Complex on chooseComplex()', () => {

    // given this complex
    const complex: Complex = TestServiceData.dummyComplex2;

  // execute test case
    component.complexChoose(complex);

  // assertion
    expect(component.showString).toEqual(complex.complexName);
    expect(component.activeComplex).toBe(complex);
    expect(component.complexRooms).toBeTruthy(); // assertion that the available rooms are filtered successfully

  });

  it('should have <div> with class page', () => {
    const pageElement: HTMLElement = fixture.nativeElement;
    const div = pageElement.getElementsByClassName('page');
    expect(div).toBeTruthy();
  });

  it('should have <div> with class "page" with two <div> children of class "leftBod" and "rightBod" ', () => {
    const pageDebug: DebugElement = fixture.debugElement;
    const pageElement: HTMLElement = pageDebug.nativeElement;
    const child1 = pageElement.getElementsByClassName('leftBod');
    const child2 = pageElement.getElementsByClassName('rightBod');
    expect(child1).toBeTruthy();
    expect(child2).toBeTruthy();
  });

  it('should have a button that displays current selected complex', () => {
    const buttonDebug: DebugElement = fixture.debugElement;
    const buttonElement: HTMLElement = buttonDebug.nativeElement.querySelector('#dropdownBasic1');
    component.showString = 'SELECTED COMPLEX';
    fixture.detectChanges();
    expect(buttonElement.textContent).toContain(component.showString);
  });

  it('should mouse over on mouseOn()', () => {
    const room: Room = TestServiceData.room;
    component.mouseOn(room);

    expect(component.mouseOverRoom).toBe(room);
  });

  it('should mouse off on mouseOff()', () => {
    const room: Room = TestServiceData.room;
    component.mouseOn(room);
    component.mouseOff();

    expect(component.mouseOverRoom).toBeNull();
  });

  it('should select on select()', () => {
    const room: Room = TestServiceData.room;
    component.select(room);

    expect(component.selectedRoom).toEqual(room);
    expect(component.highlightRoom).toEqual(room);
  });

  it('should clear select on clearSelect()', () => {
    const room: Room = TestServiceData.room;
    component.select(room);

    component.clearSelect();
    expect(component.selectedRoom).toBeNull();
    expect(component.highlightRoom).toBeNull();

  });

  it('should update room on roomChange()', () => {
    const room: Room = TestServiceData.room;

    component.roomChange(room);
    expect(component.selectedRoom).toBeNull();
    expect(component.highlightRoom).toBeNull();
  });

  it('should remove room on RemoveRoom()', () => {
    const room: Room = TestServiceData.room;
    component.complexChoose(TestServiceData.dummyComplex);
    component.removeRoom(room);

    expect(component.selectedRoom).toBeNull();
    expect(component.highlightRoom).toBeNull();
  });

});
