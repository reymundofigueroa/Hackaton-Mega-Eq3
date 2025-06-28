import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-assign-promotions',
  imports: [CommonModule, FormsModule],
  templateUrl: './assign-promotions.component.html',
  styleUrl: './assign-promotions.component.css'
})
export class AssignPromotionsComponent {
  clienteSearch = '';
  promoSearch = '';
  clientSelected: any = null;
  promoSelected: any = null;

  clients = [
    { idContrato: 1, nombreCliente: 'Carlos Pérez', idSucursal: 101 },
    { idContrato: 2, nombreCliente: 'Ana Gómez', idSucursal: 102 },
    { idContrato: 3, nombreCliente: 'Luis Martínez', idSucursal: 101 },
    { idContrato: 4, nombreCliente: 'María Fernández', idSucursal: 103 },
    { idContrato: 5, nombreCliente: 'Pedro Ramírez', idSucursal: 102 },
    { idContrato: 6, nombreCliente: 'Laura Salazar', idSucursal: 101 },
    { idContrato: 7, nombreCliente: 'Jorge Hernández', idSucursal: 104 },
    { idContrato: 8, nombreCliente: 'Mónica Castillo', idSucursal: 103 },
    { idContrato: 9, nombreCliente: 'Ricardo Ortega', idSucursal: 102 },
    { idContrato: 10, nombreCliente: 'Valeria Cruz', idSucursal: 101 },
  ];
  clientsToShow = []

  promotions = [
    { idPromocion: 201, nombreDePromo: 'Descuento 2x1', idSucursal: 101 },
    { idPromocion: 202, nombreDePromo: '25% Off', idSucursal: 102 },
    { idPromocion: 203, nombreDePromo: 'Envío Gratis', idSucursal: 101 },
    { idPromocion: 204, nombreDePromo: 'Combo Familiar', idSucursal: 103 },
    { idPromocion: 205, nombreDePromo: 'Precio Especial', idSucursal: 101 },
    { idPromocion: 206, nombreDePromo: '3x2 en Productos', idSucursal: 102 },
    { idPromocion: 207, nombreDePromo: 'Regalo Sorpresa', idSucursal: 104 },
    { idPromocion: 208, nombreDePromo: 'Descuento Estudiantes', idSucursal: 101 },
    { idPromocion: 209, nombreDePromo: 'Envío Prioritario', idSucursal: 103 },
    { idPromocion: 210, nombreDePromo: 'Prueba Gratuita', idSucursal: 102 },
  ];

  get clientesFiltrados() {
    return this.clients.filter(cliente =>
      cliente.nombreCliente.toLowerCase().includes(this.clienteSearch.toLowerCase()) ||
       cliente.idContrato.toString().includes(this.clienteSearch)
    );
  }

  get promocionesFiltradas() {
    return this.promotions.filter(promo =>
      promo.nombreDePromo.toLowerCase().includes(this.promoSearch.toLowerCase()) ||
             promo.idPromocion.toString().includes(this.promoSearch)

    );
  }

  selectClient(client: any) {
    if (this.clientSelected === client) {
      this.clientSelected = null;
    } else {
      this.clientSelected = this.clientSelected === client ? null : client;
    }
  }
  selectPromo(promo: any) {
    if (this.promoSelected === promo) {
      this.promoSelected = null;
    } else {
        this.promoSelected = this.promoSelected === promo ? null : promo;
    }
  }
}
