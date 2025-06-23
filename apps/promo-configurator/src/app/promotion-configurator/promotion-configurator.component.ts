import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Paquete {
  id: string;
  nombre: string;
}

interface Promocion {
  nombre: string;
  tipoDescuento: 'porcentaje' | 'fijo';
  montoDescuento: number;
  aplicaMensualidad: boolean;
  aplicaInstalacion: boolean;
  ciudades: string[];        // múltiples ciudades
  colonias: string[];        // múltiples colonias
  paquetes: string[];        // múltiples paquetes (ids)
  fechaInicio: string;
  fechaFin: string;
  activa: boolean;
}

@Component({
  selector: 'app-configurador-promocion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './promotion-configurator.component.html',
  styleUrls: ['./promotion-configurator.component.css']
})
export class PromotionConfiguratorComponent {

  // Datos simulados que vendrían de la base de datos
  todasLasCiudades = ['Ciudad A', 'Ciudad B', 'Ciudad C'];
  coloniasPorCiudad: { [key: string]: string[] } = {
    'Ciudad A': ['Colonia A1', 'Colonia A2'],
    'Ciudad B': ['Colonia B1', 'Colonia B2'],
    'Ciudad C': ['Colonia C1', 'Colonia C2']
  };
  todosLosPaquetes: Paquete[] = [
    { id: '1', nombre: 'Telefonia' },
    { id: '2', nombre: 'Internet ' },
    { id: '3', nombre: 'TV' }
  ];

  tiposDescuento = ['porcentaje', 'fijo'] as const;

  promocion: Promocion = {
    nombre: '',
    tipoDescuento: 'porcentaje',
    montoDescuento: 0,
    aplicaMensualidad: false,
    aplicaInstalacion: false,
    ciudades: [],
    colonias: [],
    paquetes: [],
    fechaInicio: '',
    fechaFin: '',
    activa: true
  };

  opcionesColonias: string[] = [];

  enviada = false;

  actualizarOpcionesColonias() {
    const conjuntoColonias = new Set<string>();
    this.promocion.ciudades.forEach(ciudad => {
      const colonias = this.coloniasPorCiudad[ciudad] || [];
      colonias.forEach(colonia => conjuntoColonias.add(colonia));
    });
    this.opcionesColonias = Array.from(conjuntoColonias);
    // Opcional: Limpiar colonias seleccionadas que ya no estén en opciones
    this.promocion.colonias = this.promocion.colonias.filter(c => this.opcionesColonias.includes(c));
  }

  alternarSeleccion(array: string[], valor: string) {
    const indice = array.indexOf(valor);
    if (indice === -1) {
      array.push(valor);
    } else {
      array.splice(indice, 1);
    }
    if (array === this.promocion.ciudades) {
      this.actualizarOpcionesColonias();
    }
  }

  estaSeleccionado(array: string[], valor: string) {
    return array.includes(valor);
  }

  enviarFormulario() {
    this.enviada = true;
    console.log('Promoción creada:', this.promocion);
    // Aquí se llamaría a un servicio para enviar la promoción al backend
  }
}
