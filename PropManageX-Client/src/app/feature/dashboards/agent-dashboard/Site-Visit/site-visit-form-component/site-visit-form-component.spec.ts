import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteVisitFormComponent } from './site-visit-form-component';

describe('SiteVisitFormComponent', () => {
  let component: SiteVisitFormComponent;
  let fixture: ComponentFixture<SiteVisitFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SiteVisitFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SiteVisitFormComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
