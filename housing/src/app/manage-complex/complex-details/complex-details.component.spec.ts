import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplexDetailsComponent } from './complex-details.component';

import {
  MatRippleModule, MatOptionModule, MatSelectModule, MatSidenavModule,
  MatIconModule, MatButtonModule, MatDividerModule, MatListModule, MatChipsModule,
  MatPaginatorModule, MatTableModule, MatCardModule, MatFormFieldModule
} from '@angular/material';
import { MatExpansionModule } from '@angular/material/expansion';
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';

describe('ComplexDetailsComponent', () => {
  let component: ComplexDetailsComponent;
  let fixture: ComponentFixture<ComplexDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NoopAnimationsModule,
        BrowserAnimationsModule,
        MatChipsModule,
        MatTableModule,
        MatChipsModule,
        MatPaginatorModule,
        MatCardModule,
        MatFormFieldModule,
        MatOptionModule,
        MatSelectModule,
        MatRippleModule,
        MatSidenavModule,
        MatIconModule,
        MatButtonModule,
        MatDividerModule,
        MatListModule,
        MatExpansionModule
      ],
      declarations: [ComplexDetailsComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ComplexDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
