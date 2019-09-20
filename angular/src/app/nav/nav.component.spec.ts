import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NavComponent } from './nav.component';
import { By } from '@angular/platform-browser';

describe('NavComponent', () => {
  let component: NavComponent;
  let fixture: ComponentFixture<NavComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        NavComponent
      ]
    }).compileComponents().then(() => {
      fixture = TestBed.createComponent(NavComponent);
      component = fixture.componentInstance;
      fixture.detectChanges();

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

    expect(about[3].nativeElement.href).toEqual('https://revature.com/our-story/');
  });
});
