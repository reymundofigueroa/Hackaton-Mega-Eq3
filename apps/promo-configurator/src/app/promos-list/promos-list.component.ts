import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { GetPromosListService } from '../services/promosList/get-promos-list.service';
import { promoModel } from '../models/data-models';
import { NavBarComponent } from "../nav-bar/nav-bar.component";

@Component({
  selector: 'app-promos-list',
  standalone: true,
  imports: [CommonModule, HttpClientModule, NavBarComponent],
  templateUrl: './promos-list.component.html',
  styleUrl: './promos-list.component.css'
})
export class PromosListComponent implements OnInit {
  selectedCard: promoModel | null = null;

  httpService = inject(GetPromosListService);

  ngOnInit(): void {
    this.httpService.getPromosList().subscribe((response: promoModel[]) => {
      this.cards = response
      console.log(response)
    });
  }

cards: promoModel[] = [];

openModal(card: promoModel) {
  this.selectedCard = card;
}

closeModal() {
  this.selectedCard = null;

}
}
