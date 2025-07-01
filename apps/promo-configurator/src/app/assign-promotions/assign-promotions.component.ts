import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GetPromosListService } from '../services/promosList/get-promos-list.service';
import { GetClientsListService } from '../services/clientsList/get-clients-list.service';
import { HttpClientModule } from '@angular/common/http';
import { promoModel } from '../models/data-models';
import { clientModel } from '../models/data-models';
import { NavBarComponent } from "../nav-bar/nav-bar.component";
import { AssignPromoToClientService } from '../services/clientToPromo/assign-promo-to-client.service';
@Component({
  selector: 'app-assign-promotions',
  imports: [CommonModule, FormsModule, HttpClientModule, NavBarComponent],
  templateUrl: './assign-promotions.component.html',
  styleUrl: './assign-promotions.component.css'
})
export class AssignPromotionsComponent implements OnInit {
  getPromosService = inject(GetPromosListService);
  getClientsService = inject(GetClientsListService);
  postClientToPromo = inject(AssignPromoToClientService)

  // estados
  clienteSearch = '';
  promoSearch = '';
  clientSelected: clientModel | null = null;
  promoSelected: promoModel | null = null;

  promotions: promoModel[] = [];
  promocionesDisponibles: promoModel[] = [];
  clients: clientModel[] = [];
  customerData: any = null;

  ngOnInit() {
    this.getClientsService.getClientsList().subscribe(clis => {
      this.clients = clis;
    });

    this.getPromosService.getPromosList().subscribe(promos => {
      this.promotions = promos;
      this.updatePromociones();   // Inicializa viendo todas las promos
    });
  }

  clientesFiltrados(): clientModel[] {
    return this.clients.filter(c =>
      c.nombre.toLowerCase().includes(this.clienteSearch.toLowerCase()) ||
      c.idSuscriptor.toString().includes(this.clienteSearch)
    );
  }

  updatePromociones(): void {
    this.promocionesDisponibles = this.promocionesFiltradas(this.clientSelected);
  }

  promocionesFiltradas(queryByClient: clientModel | null): promoModel[] {
    if (queryByClient === null) {
      return this.promotions.filter(p =>
        p.nombre.toLowerCase().includes(this.promoSearch.toLowerCase()) ||
        p.idPromocion.toString().includes(this.promoSearch)
      );
    }

    if (!this.customerData || !Array.isArray(this.customerData) || this.customerData.length === 0) {
      return [];
    }

    const serviciosContratados = this.customerData
      .flatMap((c: any) => c.servicios)
      .map((s: any) => s.servicio.idServicio);

    const promocionesAplicadas = this.customerData
      .flatMap((c: any) => c.promociones)
      .map((p: any) => p.promocion.idPromocion);

    return this.promotions.filter(promo =>
      promo.servicios.some(s => serviciosContratados.includes(s.idServicio)) &&
      !promocionesAplicadas.includes(promo.idPromocion) &&
      (
        promo.nombre.toLowerCase().includes(this.promoSearch.toLowerCase()) ||
        promo.idPromocion.toString().includes(this.promoSearch)
      )
    );
  }

  selectClient(client: clientModel): void {
    if (this.clientSelected === client) {
      this.clientSelected = null;
      this.customerData = null;
      this.promocionesDisponibles = [...this.promotions];
    } else {
      this.clientSelected = client;
      this.getClientsService.getCustomerServices(client.idSuscriptor)
        .subscribe(data => {
          this.customerData = data;
          this.updatePromociones();
        });

    }
  }

  selectPromo(p: promoModel): void {
    this.promoSelected = this.promoSelected === p ? null : p;
  }

  assignPromo(customerId: number, promoId: number){
    this.postClientToPromo.assignPromoToCustomer(customerId, promoId).subscribe({
      next: (response) => {
        console.log('Promoción asignada', response)
      },
      error: (error) => {
        console.error('Error al asignar la promoción', error)
      }
    })
  }

}
