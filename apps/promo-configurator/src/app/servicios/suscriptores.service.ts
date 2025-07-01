import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, forkJoin } from 'rxjs';

export interface Suscriptor {
  idSuscriptor: number;
  nombre: string;
  apellidoPaterno: string;
  apellidoMaterno?: string;
  email: string;
  rfc?: string;
  telefonoContacto: string;
  fechaRegistro: string;
  idDomicilio: number;
}

export interface ServicioContratado {
  servicio: string;
  precioContratado: number;
  promocion: string | null;
  tipoDescuento: string | null;
  fechaAplicacion: string | null;
}

export interface ContratoDetalle {
  idContrato: number;
  fechaContratacion: string;
  plazoForzosoMeses: number;
  estado: string;
  servicios: {
    idContratoServicio: number;
    precioContratado: number;
    fechaAlta: string;
    servicio: {
      idServicio: number;
      nombre: string;
      descripcion: string;
      precioBaseActual: number;
    };
  }[];
  promociones: {
    idContratoPromocion: number;
    fechaAplicacion: string;
    metadata?: string;
    promocion: {
      idPromocion: number;
      nombre: string;
      descripcion: string;
      tipoDescuento: string;
      valorDescuento: number;
      aplicaA: string;
      duracionMeses: number;
    };
  }[];
}

export interface MovimientoCuenta {
  idMovimiento: number;
  idContrato: number;
  fechaMovimiento: string;
  concepto: string;
  montoCargo: number;
  montoPago: number;
  saldoResultante: number;
}

@Injectable({
  providedIn: 'root'
})
export class SuscriptoresService {
  private baseApiUrl = 'http://localhost:5267';

  constructor(private http: HttpClient) { }

  buscarSuscriptores(termino: string): Observable<Suscriptor[]> {
    return this.http.get<Suscriptor[]>(`${this.baseApiUrl}/api/busquedasuscriptores/buscar?termino=${termino}`);
  }

  obtenerServiciosContratados(id: number): Observable<ServicioContratado[]> {
    return this.http.get<ServicioContratado[]>(`${this.baseApiUrl}/api/busquedasuscriptores/servicios-contratados?idSuscriptor=${id}`);
  }

  obtenerContratosPorSuscriptor(id: number): Observable<ContratoDetalle[]> {
    return this.http.get<ContratoDetalle[]>(`${this.baseApiUrl}/api/Contratos/suscriptor/${id}`);
  }

  obtenerMovimientosPorSuscriptor(id: number): Observable<MovimientoCuenta[]> {
    return this.http.get<MovimientoCuenta[]>(`${this.baseApiUrl}/api/MovimientosCuenta/suscriptor/${id}`);
  }

  obtenerProyeccionMensual(id: number): Observable<{ contratos: ContratoDetalle[]; movimientos: MovimientoCuenta[] }> {
    return forkJoin({
      contratos: this.obtenerContratosPorSuscriptor(id),
      movimientos: this.obtenerMovimientosPorSuscriptor(id)
    });
  }
}
