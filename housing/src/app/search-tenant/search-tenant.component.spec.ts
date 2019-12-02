import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchTenantComponent } from './search-tenant.component';
import { FormsModule } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { TenantSearcherService } from '../services/tenant-searcher.service';


describe('SearchTenantComponent', () => {
  let component: SearchTenantComponent;
  let fixture: ComponentFixture<SearchTenantComponent>;
  const searchTenSpy = jasmine.createSpyObj('TenantSearcherService', ['GetTenants']);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SearchTenantComponent ],
      imports: [FormsModule, HttpClientTestingModule, RouterTestingModule],
      providers: [
        {provide: TenantSearcherService, useValue: searchTenSpy}
      ]
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

  it('should get tenants', () => {
    expect(component.searchAllTenants).toBeTruthy();
  });
});
