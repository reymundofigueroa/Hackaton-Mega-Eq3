import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavBarComponent } from '../nav-bar/nav-bar.component';
import {
  SuscriptoresService,
  Suscriptor,
  ServicioContratado,
  ContratoDetalle,
  MovimientoCuenta
} from '../servicios/suscriptores.service';

interface ProyeccionExpandida {
  servicio: string;
  tipo: string;
  items: {
    mes: string;
    cantidad: number;
    desde: string;
    hasta: string;
  }[];
}

@Component({
  selector: 'app-debt-calculation',
  standalone: true,
  imports: [CommonModule, FormsModule, NavBarComponent],
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
  mostrandoProyeccion = false;

  proyeccionMensual: { mes: string; total: number }[] = [];
  proyeccionExpandida: ProyeccionExpandida[] = [];


  constructor(private suscriptoresService: SuscriptoresService) { }

  buscar() {
    if (this.termino.trim() === '') return;

    this.buscando = true;
    this.suscriptorSeleccionado = null;
    this.servicios = [];
    this.mostrandoProyeccion = false;
    this.proyeccionMensual = [];
    this.proyeccionExpandida = [];

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
    this.mostrandoProyeccion = false;
    this.proyeccionMensual = [];
    this.proyeccionExpandida = [];

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

  private obtenerPrecioBaseServicio(nombreServicio: string): number {
    const servicio = this.servicios.find(s => s.servicio === nombreServicio);
    return servicio ? servicio.precioContratado : 0;
  }

  private calcularDescuento(precioBase: number, tipoDescuento: string, valorDescuento: number): number {
    switch (tipoDescuento) {
      case 'PORCENTAJE':
        const descuentoCalculado = precioBase * (valorDescuento / 100);
        const precioFinal = precioBase - descuentoCalculado;
        return precioFinal;
      case 'MONTO_FIJO':
        const precioConDescuento = Math.max(0, precioBase - valorDescuento);
        return precioConDescuento;
      case 'MESES_GRATIS':
        return 0; // Meses gratis = precio 0
      default:
        return precioBase;
    }
  }

  mostrarProyeccion() {
    if (!this.suscriptorSeleccionado) return;

    this.suscriptoresService.obtenerProyeccionMensual(this.suscriptorSeleccionado.idSuscriptor).subscribe({
      next: ({ contratos }) => {
        const agrupados: { [key: string]: ProyeccionExpandida } = {};
        const mensualAgrupado: { [mes: string]: number } = {};

        // Procesar contratos si existen
        if (contratos && contratos.length > 0) {
          contratos.forEach(contrato => {
            contrato.servicios.forEach(servicio => {
              const nombreServicio = servicio.servicio.nombre;
              const precioBase = servicio.precioContratado;
              const fechaInicio = new Date(servicio.fechaAlta);
              const plazoMeses = contrato.plazoForzosoMeses;

              // Buscar promociones que apliquen a este servicio
              const promocionesAplicables = contrato.promociones.filter(p =>
                p.promocion?.aplicaA === 'MENSUALIDAD'
              );

              // Si hay promociones, tomamos la primera
              const promo = promocionesAplicables.length > 0 ? promocionesAplicables[0].promocion : null;

              const tipo = promo ? promo.tipoDescuento : 'Sin promoción';
              const valor = promo ? promo.valorDescuento : 0;
              const duracionPromo = promo ? promo.duracionMeses : 0;

              // Clave única por servicio
              const key = `${nombreServicio}-${tipo}-${servicio.idContratoServicio}`;
              agrupados[key] = {
                servicio: nombreServicio,
                tipo,
                items: []
              };

              // Generar proyección para todos los meses del plazo
              const plazoReal = Math.max(plazoMeses, 12); // Siempre generar al menos 12 meses

              for (let i = 0; i < plazoReal; i++) {
                const fechaMes = new Date(fechaInicio);
                fechaMes.setMonth(fechaMes.getMonth() + i);
                const desde = new Date(fechaMes);
                const hasta = new Date(fechaMes);
                hasta.setMonth(hasta.getMonth() + 1);
                hasta.setDate(0);

                const mesTexto = desde.toLocaleString('default', { month: 'short', year: 'numeric' });
                const isoDesde = desde.toISOString().split('T')[0];
                const isoHasta = hasta.toISOString().split('T')[0];

                // Calcular monto con descuento si aplica
                let cantidad = precioBase;
                if (promo && i < duracionPromo) {
                  cantidad = this.calcularDescuento(precioBase, tipo, valor);
                }

                agrupados[key].items.push({
                  mes: mesTexto,
                  cantidad,
                  desde: isoDesde,
                  hasta: isoHasta
                });

                const mensualKey = `${desde.getFullYear()}-${(desde.getMonth() + 1).toString().padStart(2, '0')}`;
                if (!mensualAgrupado[mensualKey]) mensualAgrupado[mensualKey] = 0;
                mensualAgrupado[mensualKey] += cantidad;
              }
            });
          });
        }

        // Si no hay contratos o no se generó proyección, usar servicios contratados como fallback
        if (Object.keys(agrupados).length === 0 && this.servicios.length > 0) {
          this.servicios.forEach((servicio, idx) => {
            const nombreServicio = servicio.servicio;
            const precioBase = servicio.precioContratado;
            const tipo = servicio.tipoDescuento || 'Sin promoción';

            // Clave única por servicio
            const key = `${nombreServicio}-${tipo}-${idx}`;
            agrupados[key] = {
              servicio: nombreServicio,
              tipo,
              items: []
            };

            // Generar proyección para 12 meses por defecto
            const fechaInicio = new Date();

            for (let i = 0; i < 12; i++) {
              const fechaMes = new Date(fechaInicio);
              fechaMes.setMonth(fechaMes.getMonth() + i);
              const desde = new Date(fechaMes);
              const hasta = new Date(fechaMes);
              hasta.setMonth(hasta.getMonth() + 1);
              hasta.setDate(0);

              const mesTexto = desde.toLocaleString('default', { month: 'short', year: 'numeric' });
              const isoDesde = desde.toISOString().split('T')[0];
              const isoHasta = hasta.toISOString().split('T')[0];

              let cantidad = precioBase;
              if (servicio.tipoDescuento && servicio.tipoDescuento !== 'Sin promoción') {
                // Aplicar descuento si existe
                const valorDescuento = 0; // Aquí podrías obtener el valor real del descuento
                cantidad = this.calcularDescuento(precioBase, servicio.tipoDescuento, valorDescuento);
              }

              agrupados[key].items.push({
                mes: mesTexto,
                cantidad,
                desde: isoDesde,
                hasta: isoHasta
              });

              const mensualKey = `${desde.getFullYear()}-${(desde.getMonth() + 1).toString().padStart(2, '0')}`;
              if (!mensualAgrupado[mensualKey]) mensualAgrupado[mensualKey] = 0;
              mensualAgrupado[mensualKey] += cantidad;
            }
          });
        }

        // Ordenar los items de cada grupo por mes
        Object.values(agrupados).forEach(grupo => {
          grupo.items.sort((a, b) => {
            const fechaA = new Date(a.desde);
            const fechaB = new Date(b.desde);
            return fechaA.getTime() - fechaB.getTime();
          });
        });

        this.proyeccionExpandida = Object.values(agrupados);
        this.proyeccionMensual = Object.entries(mensualAgrupado)
          .map(([mes, total]) => ({ mes, total }))
          .sort((a, b) => a.mes.localeCompare(b.mes));

        this.mostrandoProyeccion = true;
      },
      error: (err) => {
        console.error('Error al obtener proyección:', err);
        // En caso de error, también mostrar la proyección con servicios contratados
        this.generarProyeccionFallback();
      }
    });
  }

  private generarProyeccionFallback() {
    const agrupados: { [key: string]: ProyeccionExpandida } = {};
    const mensualAgrupado: { [mes: string]: number } = {};

    if (this.servicios.length > 0) {
      this.servicios.forEach((servicio, idx) => {
        const nombreServicio = servicio.servicio;
        const precioBase = servicio.precioContratado;
        const tipo = servicio.tipoDescuento || 'Sin promoción';

        // Clave única por servicio
        const key = `${nombreServicio}-${tipo}-${idx}`;
        agrupados[key] = {
          servicio: nombreServicio,
          tipo,
          items: []
        };

        // Generar proyección para 12 meses
        const fechaInicio = new Date();

        for (let i = 0; i < 12; i++) {
          const fechaMes = new Date(fechaInicio);
          fechaMes.setMonth(fechaMes.getMonth() + i);
          const desde = new Date(fechaMes);
          const hasta = new Date(fechaMes);
          hasta.setMonth(hasta.getMonth() + 1);
          hasta.setDate(0);

          const mesTexto = desde.toLocaleString('default', { month: 'short', year: 'numeric' });
          const isoDesde = desde.toISOString().split('T')[0];
          const isoHasta = hasta.toISOString().split('T')[0];

          let cantidad = precioBase;
          if (servicio.tipoDescuento && servicio.tipoDescuento !== 'Sin promoción') {
            // Aplicar descuento si existe
            const valorDescuento = 0; // Aquí podrías obtener el valor real del descuento
            cantidad = this.calcularDescuento(precioBase, servicio.tipoDescuento, valorDescuento);
          }

          agrupados[key].items.push({
            mes: mesTexto,
            cantidad,
            desde: isoDesde,
            hasta: isoHasta
          });

          const mensualKey = `${desde.getFullYear()}-${(desde.getMonth() + 1).toString().padStart(2, '0')}`;
          if (!mensualAgrupado[mensualKey]) mensualAgrupado[mensualKey] = 0;
          mensualAgrupado[mensualKey] += cantidad;
        }
      });
    }

    // Ordenar los items de cada grupo por mes
    Object.values(agrupados).forEach(grupo => {
      grupo.items.sort((a, b) => {
        const fechaA = new Date(a.desde);
        const fechaB = new Date(b.desde);
        return fechaA.getTime() - fechaB.getTime();
      });
    });

    this.proyeccionExpandida = Object.values(agrupados);
    this.proyeccionMensual = Object.entries(mensualAgrupado)
      .map(([mes, total]) => ({ mes, total }))
      .sort((a, b) => a.mes.localeCompare(b.mes));

    this.mostrandoProyeccion = true;
  }

  tieneProyeccion(): boolean {
    return this.proyeccionMensual.length > 0 || this.proyeccionExpandida.length > 0;
  }
}
