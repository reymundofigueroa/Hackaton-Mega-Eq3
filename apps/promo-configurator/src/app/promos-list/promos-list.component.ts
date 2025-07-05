import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { GetPromosListService } from '../services/promosList/get-promos-list.service';
import { ComunicationpromosListToCreateService } from '../services/comunicationPromoslist-create/comunicationpromos-list-to-create.service';
import { promoModel } from '../models/data-models';
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-promos-list',
  standalone: true,
  imports: [CommonModule, RouterModule, HttpClientModule, NavBarComponent],
  templateUrl: './promos-list.component.html',
  styleUrl: './promos-list.component.css'
})
export class PromosListComponent implements OnInit {
  selectedCard: promoModel | null = null;

  httpService = inject(GetPromosListService);
  communication = inject(ComunicationpromosListToCreateService)
  router = inject(Router)

  cards: promoModel[] = [];
  internetPromos: promoModel[] = [];
  telefoniaPromos: promoModel[] = [];
  tvPromos: promoModel[] = [];

  selectedFilter = 'todos';


  ngOnInit(): void {
  this.httpService.getPromosList().subscribe((response: promoModel[]) => {
    console.log('Promociones recibidas:', response);
    this.cards = response;

    this.internetPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 1)
    );
    this.telefoniaPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 2)
    );
    this.tvPromos = response.filter(promo =>
      promo.servicios && promo.servicios.some(s => s.idServicio === 3)
    );
  });
}

  onFilterChange(event: Event) {
  const selectElement = event.target as HTMLSelectElement;
  this.selectedFilter = selectElement.value;
}


  openModal(card: promoModel) {
    this.selectedCard = card;
    console.log('Card seleccionada',this.selectedCard)
  }

  closeModal() {
    this.selectedCard = null;
  }

  handlerToSendDataToEditPromos(promo: promoModel){
    this.communication.sendMessage(promo)
    this.router.navigate(['crear-promo'])
  }
}
