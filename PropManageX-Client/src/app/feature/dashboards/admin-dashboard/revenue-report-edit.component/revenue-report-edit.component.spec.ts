import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueReportEditComponent } from './revenue-report-edit.component';

describe('RevenueReportEditComponent', () => {
  let component: RevenueReportEditComponent;
  let fixture: ComponentFixture<RevenueReportEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RevenueReportEditComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RevenueReportEditComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
