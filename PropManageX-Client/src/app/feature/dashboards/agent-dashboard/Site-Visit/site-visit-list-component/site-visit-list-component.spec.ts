import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteVisitListComponent } from './site-visit-list-component';

describe('SiteVisitListComponent', () => {
  let component: SiteVisitListComponent;
  let fixture: ComponentFixture<SiteVisitListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SiteVisitListComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(SiteVisitListComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
