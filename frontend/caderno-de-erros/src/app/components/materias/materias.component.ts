import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MateriaService } from '../../services/materia.service';
import { Materia, CreateMateriaDto } from '../../models/materia.model';

@Component({
  selector: 'app-materias',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './materias.component.html',
  styleUrls: ['./materias.component.css']
})
export class MateriasComponent implements OnInit {
  materias: Materia[] = [];
  novaMateria: CreateMateriaDto = { nome: '' };
  loading = false;
  error: string | null = null;

  constructor(
    private materiaService: MateriaService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

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
        this.error = 'Erro ao criar matéria';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  verAssuntos(materiaId: number): void {
    this.router.navigate(['/assuntos', materiaId]);
  }

  excluirMateria(id: number): void {
    if (!confirm('Deseja realmente excluir esta matéria?')) {
      return;
    }

    this.materiaService.deleteMateria(id).subscribe({
      next: () => {
        this.materias = this.materias.filter(m => m.id !== id);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao excluir matéria';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }
}
