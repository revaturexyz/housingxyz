import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectTenantComponent } from './select-tenant.component';

describe('SelectTenantComponent', () => {
  let component: SelectTenantComponent;
  let fixture: ComponentFixture<SelectTenantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelectTenantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
