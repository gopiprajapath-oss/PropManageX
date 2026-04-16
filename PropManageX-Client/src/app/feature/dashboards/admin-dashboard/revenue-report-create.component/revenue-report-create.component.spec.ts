import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueReportCreateComponent } from './revenue-report-create.component';

describe('RevenueReportCreateComponent', () => {
  let component: RevenueReportCreateComponent;
  let fixture: ComponentFixture<RevenueReportCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RevenueReportCreateComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RevenueReportCreateComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
