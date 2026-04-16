import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorFormComponent } from './vendor-form-component';

describe('VendorFormComponent', () => {
  let component: VendorFormComponent;
  let fixture: ComponentFixture<VendorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VendorFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(VendorFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
