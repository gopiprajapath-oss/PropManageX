import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorEditFormComponent } from './vendor-edit-form-component';

describe('VendorEditFormComponent', () => {
  let component: VendorEditFormComponent;
  let fixture: ComponentFixture<VendorEditFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VendorEditFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VendorEditFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
