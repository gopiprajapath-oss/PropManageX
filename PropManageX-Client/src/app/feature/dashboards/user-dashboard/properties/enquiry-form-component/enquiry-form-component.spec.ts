import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnquiryFormComponent } from './enquiry-form-component';

describe('EnquiryFormComponent', () => {
  let component: EnquiryFormComponent;
  let fixture: ComponentFixture<EnquiryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnquiryFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EnquiryFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
