import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterDto } from '../../models/auth.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerData: RegisterDto = {
    nome: '',
    email: '',
    senha: ''
  };

  confirmarSenha = '';
  loading = false;
  errorMessage: string | null = null;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  onRegister(): void {
    if (!this.registerData.nome.trim() || !this.registerData.email.trim() || !this.registerData.senha) {
      this.errorMessage = 'Por favor, preencha todos os campos.';
      return;
    }

    if (this.registerData.senha !== this.confirmarSenha) {
      this.errorMessage = 'As senhas não coincidem.';
      return;
    }

    if (this.registerData.senha.length < 6) {
      this.errorMessage = 'A senha deve conter no mínimo 6 caracteres.';
      return;
    }

    this.loading = true;
    this.errorMessage = null;

    this.authService.register(this.registerData).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/erros']);
      },
      error: (err) => {
        this.loading = false;
        if (err.error && err.error.message) {
          this.errorMessage = err.error.message;
        } else {
          this.errorMessage = 'Erro ao realizar cadastro. Tente novamente.';
        }
      }
    });
  }
}
