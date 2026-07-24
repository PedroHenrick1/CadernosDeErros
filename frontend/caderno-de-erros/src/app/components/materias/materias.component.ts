import { Component, OnInit, ChangeDetectorRef, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { MateriaService } from '../../services/materia.service';
import { AuthService } from '../../services/auth.service';
import { Materia, CreateMateriaDto } from '../../models/materia.model';

@Component({
  selector: 'app-materias',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './materias.component.html',
  styleUrls: ['./materias.component.css']
})
export class MateriasComponent implements OnInit {
  materias: Materia[] = [];
  novaMateria: CreateMateriaDto = { nome: '' };
  loading = false;
  error: string | null = null;
  itemParaExcluir: Materia | null = null;

  constructor(
    private materiaService: MateriaService,
    public authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  @HostListener('window:keydown.escape')
  handleEscapeKey(): void {
    if (this.itemParaExcluir) {
      this.cancelarExclusao();
    }
  }

  ngOnInit(): void {
    this.carregarMaterias();
  }

  carregarMaterias(): void {
    this.loading = true;
    this.error = null;
    this.materiaService.getMaterias().subscribe({
      next: (data) => {
        this.materias = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar matérias';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  criarMateria(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    if (!this.novaMateria.nome.trim()) {
      return;
    }

    this.loading = true;
    this.materiaService.createMateria(this.novaMateria).subscribe({
      next: (materia) => {
        this.materias.unshift(materia);
        this.novaMateria = { nome: '' };
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao criar matéria. Verifique sua autenticação.';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  verAssuntos(materiaId: number): void {
    this.router.navigate(['/assuntos', materiaId]);
  }

  solicitarExclusao(materia: Materia): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }
    this.itemParaExcluir = materia;
  }

  cancelarExclusao(): void {
    this.itemParaExcluir = null;
  }

  confirmarExclusao(): void {
    if (!this.itemParaExcluir) return;
    const id = this.itemParaExcluir.id;
    this.materiaService.deleteMateria(id).subscribe({
      next: () => {
        this.materias = this.materias.filter(m => m.id !== id);
        this.itemParaExcluir = null;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao excluir matéria.';
        this.itemParaExcluir = null;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }
}
