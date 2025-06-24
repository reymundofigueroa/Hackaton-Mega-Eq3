import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-promos-list',
  imports: [CommonModule],
  templateUrl: './promos-list.component.html',
  styleUrl: './promos-list.component.css'
})
export class PromosListComponent {
selectedCard: any = null;

cards = [
  { title: 'Promo 1', type: 'Telefonía', discount: '$100 MXN' },
  { title: 'Promo 2', type: 'Internet', discount: '$150 MXN' },
  { title: 'Promo 3', type: 'TV', discount: '$200 MXN' },
  { title: 'Promo 4', type: 'Combo', discount: '$300 MXN' },
  { title: 'Promo 5', type: 'Telefonía', discount: '$120 MXN' },
  { title: 'Promo 6', type: 'TV', discount: '$90 MXN' },
  { title: 'Promo 7', type: 'Internet', discount: '$130 MXN' },
  { title: 'Promo 8', type: 'Combo', discount: '$250 MXN' },
  { title: 'Promo 9', type: 'Telefonía', discount: '$80 MXN' },
  { title: 'Promo 10', type: 'TV', discount: '$110 MXN' },
];

openModal(card: any) {
  this.selectedCard = card;
}

closeModal() {
  this.selectedCard = null;

}
}
