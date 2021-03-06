import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { Component } from '@angular/core';
import { By } from '@angular/platform-browser';

@Component({
  selector: 'dev-nav',
  template: '<p ngClass="testing">Mock nav Component</p>'
})
class MockNavComponent { }

describe('AppComponent', () => {
  let fixture: ComponentFixture<AppComponent>;
  let app: any;
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        MockNavComponent
      ],
      imports: [
        RouterTestingModule
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    app = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the app', () => {
    expect(app).toBeTruthy();
  });

  it(`should have as title 'housing'`, () => {
    expect(app.title).toEqual('housing');
  });

  it('should render child nav component', () => {
    expect(fixture.debugElement.query(By.css('.testing')).nativeElement.textContent).toContain('Mock nav Component');
  });
});
