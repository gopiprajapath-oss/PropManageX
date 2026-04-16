import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueFormComponent } from './revenue-form-component';

describe('RevenueFormComponent', () => {
  let component: RevenueFormComponent;
  let fixture: ComponentFixture<RevenueFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RevenueFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RevenueFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
