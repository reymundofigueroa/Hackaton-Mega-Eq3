import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignPromotionsComponent } from './assign-promotions.component';

describe('AssignPromotionsComponent', () => {
  let component: AssignPromotionsComponent;
  let fixture: ComponentFixture<AssignPromotionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignPromotionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignPromotionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
