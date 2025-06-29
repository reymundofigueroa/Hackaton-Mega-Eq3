import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SuscriptoresService, Suscriptor, ServicioContratado } from '../servicios/suscriptores.service';

@Component({
  selector: 'app-debt-calculation',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './debt-calculation.component.html',
  styleUrls: ['./debt-calculation.component.css']
})
export class DebtCalculationComponent {
  termino = '';
  resultados: Suscriptor[] = [];
  servicios: ServicioContratado[] = [];
  suscriptorSeleccionado: Suscriptor | null = null;
  buscando = false;
  cargandoServicios = false;

  constructor(private suscriptoresService: SuscriptoresService) {}

  buscar() {
    if (this.termino.trim() === '') return;

    this.buscando = true;
    this.suscriptorSeleccionado = null;
    this.servicios = [];

    this.suscriptoresService.buscarSuscriptores(this.termino).subscribe({
      next: (data) => {
        this.resultados = data;
        this.buscando = false;
      },
      error: (err) => {
        console.error('Error en búsqueda de suscriptores:', err);
        this.buscando = false;
      }
    });
  }

seleccionarSuscriptor(s: Suscriptor) {
  this.suscriptorSeleccionado = s;
  this.cargandoServicios = true;
  this.servicios = [];
  this.suscriptoresService.obtenerServiciosContratados(s.idSuscriptor).subscribe({
    next: (data) => {
      this.servicios = data;
      this.cargandoServicios = false;
    },
    error: (err) => {
      console.error('Error al obtener servicios:', err);
      this.cargandoServicios = false;
    }
  });
}



}
