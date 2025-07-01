import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Estado {
  idEstado: number;
  nombre: string;
}

export interface Municipio {
  idMunicipio: number;
  nombre: string;
  estado: Estado;
}

export interface Ciudad {
  idCiudad: number;
  nombre: string;
  municipio: Municipio;
}

export interface Colonia {
  idColonia: number;
  nombre: string;
  codigoPostal: string;
  ciudad: Ciudad;
}

export interface Sucursal {
  idSucursal: number;
  nombre: string;
  telefono: string;
  idDomicilio: number;
  colonias: Colonia[] | number[]; 
}

export interface Servicio {
  idServicio: number;
  nombre: string;
  descripcion?: string;
  precioBaseActual: number;
}

@Injectable({
    providedIn: 'root',
})
export class UbicacionService {
    private apiUrl = 'http://localhost:5267/api/';

    constructor(private http: HttpClient) { }

    getEstados(): Observable<Estado[]> {
        return this.http.get<Estado[]>(`${this.apiUrl}Estados`);
    }

    getMunicipiosPorEstado(estadoId: number): Observable<Municipio[]> {
        return this.http.get<Municipio[]>(`${this.apiUrl}Municipios?estadoId=${estadoId}`);
    }


    getCiudadesPorMunicipio(municipioId: number): Observable<Ciudad[]> {
        return this.http.get<Ciudad[]>(`${this.apiUrl}Ciudades/municipio/${municipioId}`);
    }

    getColoniasPorCiudad(ciudadId: number): Observable<Colonia[]> {
        return this.http.get<Colonia[]>(`${this.apiUrl}Colonias/ciudad/${ciudadId}`);
    }

    getSucursalesPorColonia(coloniaId: number): Observable<Sucursal[]> {
        return this.http.get<Sucursal[]>(`${this.apiUrl}SucursalColonias/colonia/${coloniaId}`);
    }

    getUbicacionCompleta(): Observable<any> {
        return this.http.get<any>(`${this.apiUrl}Sucursales/info-completa`);
    }

    getServicios(): Observable<Servicio[]> {
        return this.http.get<Servicio[]>(`${this.apiUrl}Servicios`);
    }
}
