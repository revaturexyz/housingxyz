import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowTenantComponent } from './show-tenant.component';

describe('ShowTenantComponent', () => {
  let component: ShowTenantComponent;
  let fixture: ComponentFixture<ShowTenantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShowTenantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
