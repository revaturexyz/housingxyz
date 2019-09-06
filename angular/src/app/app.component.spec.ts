import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';


@Component({
  selector: 'app-nav',
  template: '<p ngClass="testing">Mock app-nav Component</p>'
})
class MockNavComponent {}

describe('AppComponent', () => {
  let fixture : ComponentFixture<AppComponent>;
  let app :any;
  let compiled :any;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent,
        MockNavComponent
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    app = fixture.componentInstance;
    fixture.detectChanges();
    compiled = fixture.debugElement.nativeElement;



  });
  it('should create the app', () => {
    expect(app).toBeTruthy();
  });

  it(`should have as title 'housingxyz'`, () => {
    expect(app.title).toEqual('housingxyz');
  });
});
