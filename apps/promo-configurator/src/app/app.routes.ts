import { Routes } from '@angular/router';

export const routes: Routes = [
  
  {
    path: 'home', loadComponent: () => import('./home/home.component')
      .then(m => m.HomeComponent)
  },
  {
    path: 'crear-promo', loadComponent: () => import('./creador-promociones/creador-promociones.component')
      .then(m => m.CreadorPromocionesComponent)
  },
  {
    path: 'lista-de-promos', loadComponent: () => import('./promos-list/promos-list.component')
      .then(m => m.PromosListComponent)
  },
  {
    path: 'asignar-promoción', loadComponent: () => import('./assign-promotions/assign-promotions.component')
      .then(m => m.AssignPromotionsComponent)
  },
  {
    path: 'calculo-de-deuda', loadComponent: () => import('./debt-calculation/debt-calculation.component')
      .then(m => m.DebtCalculationComponent)
  },
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full'
  }
];
