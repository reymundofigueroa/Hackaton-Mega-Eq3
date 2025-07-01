import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { clientModel } from '../../models/data-models';
import { catchError, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AssignPromoToClientService {

  httpClient = inject(HttpClient)
  ApiUrl = 'http://localhost:5267/api/'

  assignPromoToCustomer(customerId: number, promotionId: number): Observable<any>{
    const fechaAplicacionn = new Date().toISOString();
    const body = {
      idContrato: customerId,
      idPromocion: promotionId,
      fechaAplicacion: fechaAplicacionn
    };
    console.log('Body enviado', body)
    return this.httpClient.post(`${this.ApiUrl}ContratoPromociones`, body)

  }


}
