import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProviderStatusComponent } from './provider-status.component';

describe('ProviderStatusComponent', () => {
  let component: ProviderStatusComponent;
  let fixture: ComponentFixture<ProviderStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProviderStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProviderStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
