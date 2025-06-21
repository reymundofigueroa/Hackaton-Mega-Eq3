import { Component } from '@angular/core';
import { PromotionConfiguratorComponent } from './promotion-configurator/promotion-configurator.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PromotionConfiguratorComponent],
  template: `<app-configurador-promocion></app-configurador-promocion>`,
})
export class AppComponent {}
