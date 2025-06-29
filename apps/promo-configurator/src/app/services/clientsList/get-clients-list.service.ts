import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { clientModel } from '../../models/data-models';
import { catchError, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GetClientsListService {

  private http = inject(HttpClient)

  ApiUrl = 'http://localhost:5267/api/'

  getClientsList(): Observable<clientModel[]>{
    return this.http.get<clientModel[]>(`${this.ApiUrl}Suscriptores`).pipe(
      map((response: clientModel[]) => response),
      catchError(error => throwError(error))
    )
  }

  getCustomerServices(): Observable<any>{
    return this.http.get<any>(`${this.ApiUrl}`).pipe(
      map((response: any) => response),
      catchError(error => throwError(error))
    )
  }

}
