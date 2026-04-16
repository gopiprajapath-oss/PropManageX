import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceListComponent } from './maintenance-list-component';

describe('MaintenanceListComponent', () => {
  let component: MaintenanceListComponent;
  let fixture: ComponentFixture<MaintenanceListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MaintenanceListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MaintenanceListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
