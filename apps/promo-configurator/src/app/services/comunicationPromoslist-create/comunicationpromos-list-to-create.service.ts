import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { promoModel } from '../../models/data-models';

@Injectable({
  providedIn: 'root'
})
export class ComunicationpromosListToCreateService {
  private messageSubject = new BehaviorSubject<promoModel>({} as promoModel)
  message$ = this.messageSubject.asObservable()

  sendMessage(message: promoModel){
    this.messageSubject.next(message)
  }
}
