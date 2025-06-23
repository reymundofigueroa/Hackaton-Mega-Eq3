import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private router = inject(Router);

  goToCreatePromo() {
    this.router.navigate(['/crear-promo']);
  }

  goToPromosList() {
    this.router.navigate(['/lista-de-promos']);
  }

  goToAssignPromo(){
    this.router.navigate(['/asignar-promoción'])
  }

  goToDebtCalculator(){
    this.router.navigate(['/cálculo-de-deuda'])
  }
}
