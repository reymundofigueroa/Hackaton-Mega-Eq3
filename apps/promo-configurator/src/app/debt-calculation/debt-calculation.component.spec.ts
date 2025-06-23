import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DebtCalculationComponent } from './debt-calculation.component';

describe('DebtCalculationComponent', () => {
  let component: DebtCalculationComponent;
  let fixture: ComponentFixture<DebtCalculationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DebtCalculationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DebtCalculationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
