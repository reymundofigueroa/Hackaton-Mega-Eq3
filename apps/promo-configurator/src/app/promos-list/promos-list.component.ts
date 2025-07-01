import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { GetPromosListService } from '../services/promosList/get-promos-list.service';
import { promoModel } from '../models/data-models';
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-promos-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule, NavBarComponent],
  templateUrl: './promos-list.component.html',
  styleUrl: './promos-list.component.css'
})
export class PromosListComponent implements OnInit {
  selectedCard: promoModel | null = null;

  httpService = inject(GetPromosListService);

  cards: promoModel[] = [];
  internetPromos: promoModel[] = [];
  telefoniaPromos: promoModel[] = [];
  tvPromos: promoModel[] = [];

  selectedFilter = 'todos';


  ngOnInit(): void {
  this.httpService.getPromosList().subscribe((response: promoModel[]) => {
    console.log('Promociones recibidas:', response);
    this.cards = response;

    // Debug: Verificar servicios de cada promoción
    response.forEach(promo => {
      console.log(`Promoción ${promo.idPromocion}: ${promo.nombre} - Servicios:`, promo.servicios);
    });

    this.internetPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 1)
    );
    this.telefoniaPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 2)
    );
    this.tvPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 3)
    );

    console.log('Internet promos:', this.internetPromos.length);
    console.log('Telefonía promos:', this.telefoniaPromos.length);
    console.log('TV promos:', this.tvPromos.length);
  });
}

  onFilterChange(event: Event) {
  const selectElement = event.target as HTMLSelectElement;
  this.selectedFilter = selectElement.value;
}


  openModal(card: promoModel) {
    this.selectedCard = card;
  }

  closeModal() {
    this.selectedCard = null;

  }
}
