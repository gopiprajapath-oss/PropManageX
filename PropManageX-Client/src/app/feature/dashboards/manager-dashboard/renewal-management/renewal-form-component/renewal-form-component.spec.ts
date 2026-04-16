import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenewalFormComponent } from './renewal-form-component';

describe('RenewalFormComponent', () => {
  let component: RenewalFormComponent;
  let fixture: ComponentFixture<RenewalFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenewalFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RenewalFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
