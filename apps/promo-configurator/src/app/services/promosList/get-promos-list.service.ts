import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { promoModel } from '../../models/data-models';

@Injectable({
  providedIn: 'root'
})
export class GetPromosListService {

  private http = inject(HttpClient);

  ApiUrl = 'http://localhost:5267/api/';

  getPromosList(): Observable<promoModel[]>{
    return this.http.get<promoModel[]>(`${this.ApiUrl}Promociones`).pipe(
      map((response:promoModel[]) => response),
      catchError(error => throwError(error))
    )
  }

}
