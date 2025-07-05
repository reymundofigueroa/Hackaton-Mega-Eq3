import { Component, inject } from '@angular/core';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  private router = inject(Router);

  goTo(route: string){
    this.router.navigate([route])
  }
}
