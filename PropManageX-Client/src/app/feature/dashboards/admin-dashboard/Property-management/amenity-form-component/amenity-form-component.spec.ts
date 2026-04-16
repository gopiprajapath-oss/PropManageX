import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmenityFormComponent } from './amenity-form-component';

describe('AmenityFormComponent', () => {
  let component: AmenityFormComponent;
  let fixture: ComponentFixture<AmenityFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AmenityFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AmenityFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
