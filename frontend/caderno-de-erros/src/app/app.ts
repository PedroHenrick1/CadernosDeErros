import { Component } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  title = 'Caderno de Erros';
  mobileMenuAberto = false;

  constructor(
    public authService: AuthService,
    private router: Router
  ) {}

  toggleMobileMenu(): void {
    this.mobileMenuAberto = !this.mobileMenuAberto;
  }

  fecharMobileMenu(): void {
    this.mobileMenuAberto = false;
  }

  logout(): void {
    this.fecharMobileMenu();
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
