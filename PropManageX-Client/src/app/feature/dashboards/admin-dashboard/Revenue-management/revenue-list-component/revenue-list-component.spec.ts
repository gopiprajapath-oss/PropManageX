import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevenueListComponent } from './revenue-list-component';

describe('RevenueListComponent', () => {
  let component: RevenueListComponent;
  let fixture: ComponentFixture<RevenueListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RevenueListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RevenueListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
