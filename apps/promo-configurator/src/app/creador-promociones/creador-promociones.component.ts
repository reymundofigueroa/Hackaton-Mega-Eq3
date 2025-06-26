import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-creador-promociones',
  imports: [CommonModule, FormsModule],
  templateUrl: './creador-promociones.component.html',
  styleUrl: './creador-promociones.component.css'
})
export class CreadorPromocionesComponent {
   tipoDescuento: 'monto' | 'porcentaje' = 'monto';
  duracionMeses: number[] = [1, 3, 6, 12];

  ciudades = [
    {
      nombre: 'Ciudad de México',
      colonias: [
        {
          nombre: 'Roma Norte',
          sucursales: [
            { nombre: 'Sucursal Insurgentes' },
            { nombre: 'Sucursal Álvaro Obregón' }
          ]
        },
        {
          nombre: 'Condesa',
          sucursales: [
            { nombre: 'Sucursal Michoacán' }
          ]
        }
      ]
    },
    {
      nombre: 'Guadalajara',
      colonias: [
        {
          nombre: 'Providencia',
          sucursales: [
            { nombre: 'Sucursal Chapultepec' },
            { nombre: 'Sucursal Américas' }
          ]
        },
        {
          nombre: 'Zapopan',
          sucursales: [
            { nombre: 'Sucursal Andares' }
          ]
        }
      ]
    },
    {
      nombre: 'Monterrey',
      colonias: [
        {
          nombre: 'San Pedro',
          sucursales: [
            { nombre: 'Sucursal Valle' },
            { nombre: 'Sucursal Calzada' }
          ]
        },
        {
          nombre: 'Centro',
          sucursales: [
            { nombre: 'Sucursal Juárez' }
          ]
        }
      ]
    }
  ];

  ciudadSeleccionada: any = null;
  coloniaSeleccionada: any = null;

  seleccionarCiudad(ciudad: any) {
    this.ciudadSeleccionada = ciudad;
    this.coloniaSeleccionada = null; // Reinicia sucursales
  }

  seleccionarColonia(colonia: any) {
    this.coloniaSeleccionada = colonia;
}
}
