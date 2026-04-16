import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RenewalListComponent } from './renewal-list-component';

describe('RenewalListComponent', () => {
  let component: RenewalListComponent;
  let fixture: ComponentFixture<RenewalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RenewalListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RenewalListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
