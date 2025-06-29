import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GetPromosListService } from '../services/promosList/get-promos-list.service';
import { GetClientsListService } from '../services/clientsList/get-clients-list.service';
import { HttpClientModule } from '@angular/common/http';
import { promoModel } from '../models/data-models';
import { clientModel } from '../models/data-models';
@Component({
  selector: 'app-assign-promotions',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './assign-promotions.component.html',
  styleUrl: './assign-promotions.component.css'
})
export class AssignPromotionsComponent implements OnInit {
  getPromosService = inject(GetPromosListService)
  getClientsService = inject(GetClientsListService)

  clienteSearch = '';
  promoSearch = '';
  clientSelected: clientModel | null = null;
  promoSelected: promoModel | null = null;


  clientsToShow = []
  clients: clientModel[] = [];
  promotions: promoModel[] = [];

  ngOnInit() {
    this.getPromosService.getPromosList().subscribe((response: promoModel[]) => {
      this.promotions = response
      console.log(this.promotions)
    })

    this.getClientsService.getClientsList().subscribe((response: clientModel[]) => {
      this.clients = response
      console.log(this.clients)
    })

  }

  clientesFiltrados() {
    return this.clients.filter(cliente =>
      cliente.nombre.toLowerCase().includes(this.clienteSearch.toLowerCase()) ||
      cliente.idSuscriptor.toString().includes(this.clienteSearch)
    );
  }

  promocionesFiltradas(queryByClient: clientModel | null) {
    if (queryByClient === null) {
      return this.promotions.filter(promo =>
        promo.nombre.toLowerCase().includes(this.promoSearch.toLowerCase()) ||
        promo.idPromocion.toString().includes(this.promoSearch)
      );
    } else {
     return this.promotions
    }
  }

  selectClient(client: clientModel) {
    if (this.clientSelected === client) {
      this.clientSelected = null;
    } else {
      this.clientSelected = this.clientSelected === client ? null : client;
    }
  }
  selectPromo(promo: promoModel) {
    if (this.promoSelected === promo) {
      this.promoSelected = null;
    } else {
      this.promoSelected = this.promoSelected === promo ? null : promo;
    }
  }
}
