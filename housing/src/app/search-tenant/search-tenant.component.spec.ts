import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTenantComponent } from './search-tenant.component';

describe('SearchTenantComponent', () => {
  let component: SearchTenantComponent;
  let fixture: ComponentFixture<SearchTenantComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchTenantComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchTenantComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
