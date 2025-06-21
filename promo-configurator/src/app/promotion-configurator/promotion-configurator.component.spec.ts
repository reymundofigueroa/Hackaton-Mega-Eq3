import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionConfiguratorComponent } from './promotion-configurator.component';

describe('PromotionConfiguratorComponent', () => {
  let component: PromotionConfiguratorComponent;
  let fixture: ComponentFixture<PromotionConfiguratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PromotionConfiguratorComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PromotionConfiguratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
