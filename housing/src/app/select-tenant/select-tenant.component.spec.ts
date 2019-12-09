import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectTenantComponent } from './select-tenant.component';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('SelectTenantComponent', () => {
  let component: SelectTenantComponent;
  let fixture: ComponentFixture<SelectTenantComponent>;
  const selectTenSpy = jasmine.createSpyObj('SelectTenantComponent', ['deleteGo']);
  const activeRoute = jasmine.createSpyObj('ActivatedRoute', ['id']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SelectTenantComponent],
      imports: [RouterTestingModule, HttpClientTestingModule]
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
