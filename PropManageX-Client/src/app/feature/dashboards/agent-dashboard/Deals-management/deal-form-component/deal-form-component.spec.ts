import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DealFormComponent } from './deal-form-component';

describe('DealFormComponent', () => {
  let component: DealFormComponent;
  let fixture: ComponentFixture<DealFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DealFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DealFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
