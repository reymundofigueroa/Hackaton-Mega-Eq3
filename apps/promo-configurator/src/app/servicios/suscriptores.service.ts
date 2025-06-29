import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

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

@Injectable({
  providedIn: 'root'
})
export class SuscriptoresService {
  private apiUrl = 'http://localhost:5267/api/busquedasuscriptores';

  constructor(private http: HttpClient) {}

  buscarSuscriptores(termino: string): Observable<Suscriptor[]> {
    return this.http.get<Suscriptor[]>(`${this.apiUrl}/buscar?termino=${termino}`);
  }

  obtenerServiciosContratados(id: number): Observable<ServicioContratado[]> {
    return this.http.get<ServicioContratado[]>(`${this.apiUrl}/servicios-contratados?idSuscriptor=${id}`);
  }
}
