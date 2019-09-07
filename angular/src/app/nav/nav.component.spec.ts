import { async, ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';

import { NavComponent } from './nav.component';
import { RouterTestingModule } from '@angular/router/testing';
import { By } from '@angular/platform-browser';
import { Router } from '@angular/router';

describe('NavComponent', () => {
  let component: NavComponent;
  let fixture: ComponentFixture<NavComponent>;
  let compiled: any;
  let linkDes: any;
  let routerLinks: any;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        NavComponent
      ],
      providers: [
        { provide: Router }
      ]
    }).compileComponents().then(() => {
      fixture = TestBed.createComponent(NavComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();
      compiled = fixture.debugElement.nativeElement;

      // find DebugElements with an attached RouterLinkStubDirective

      // get attached link directive instances
      // using each DebugElement's injector
    });
  }));


  it('should create nav component', () => {
    expect(component).toBeTruthy();
  });

  it(' href of about us should be "https://revature.com/our-story/"', () => {
    const about = fixture.debugElement.queryAll(By.css('a'));
    
    expect(about[2].nativeElement.href).toEqual("https://revature.com/our-story/");
  });
});
