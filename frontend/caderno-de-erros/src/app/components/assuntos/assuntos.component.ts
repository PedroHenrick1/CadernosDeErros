import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AssuntoService } from '../../services/assunto.service';
import { MateriaService } from '../../services/materia.service';
import { Assunto, CreateAssuntoDto } from '../../models/assunto.model';
import { Materia } from '../../models/materia.model';

@Component({
  selector: 'app-assuntos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './assuntos.component.html',
  styleUrls: ['./assuntos.component.css']
})
export class AssuntosComponent implements OnInit {
  assuntos: Assunto[] = [];
  materias: Materia[] = [];
  materia: Materia | null = null;
  materiaId: number | null = null;
  novoAssunto: CreateAssuntoDto = { nome: '', materiaId: 0 };
  loading = false;
  error: string | null = null;

  constructor(
    private assuntoService: AssuntoService,
    private materiaService: MateriaService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['materiaId']) {
        this.materiaId = +params['materiaId'];
        this.novoAssunto.materiaId = this.materiaId;
        this.carregarMateria(this.materiaId);
        this.carregarAssuntosPorMateria(this.materiaId);
      } else {
        this.carregarTodasMaterias();
        this.carregarTodosAssuntos();
      }
    });
  }

  carregarMateria(id: number): void {
    this.materiaService.getMateria(id).subscribe({
      next: (data) => {
        this.materia = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar matéria';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarTodasMaterias(): void {
    this.materiaService.getMaterias().subscribe({
      next: (data) => {
        this.materias = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar matérias';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarAssuntosPorMateria(materiaId: number): void {
    this.loading = true;
    this.error = null;
    this.assuntoService.getAssuntosByMateria(materiaId).subscribe({
      next: (data) => {
        this.assuntos = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar assuntos';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarTodosAssuntos(): void {
    this.loading = true;
    this.error = null;
    this.assuntoService.getAssuntos().subscribe({
      next: (data) => {
        this.assuntos = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar assuntos';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  criarAssunto(): void {
    if (!this.novoAssunto.nome.trim() || !this.novoAssunto.materiaId) {
      return;
    }

    this.loading = true;
    this.assuntoService.createAssunto(this.novoAssunto).subscribe({
      next: (assunto) => {
        this.assuntos.unshift(assunto);
        this.novoAssunto = { nome: '', materiaId: this.materiaId || 0 };
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao criar assunto';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  verErros(assuntoId: number): void {
    this.router.navigate(['/erros', assuntoId]);
  }

  excluirAssunto(id: number): void {
    if (!confirm('Deseja realmente excluir este assunto?')) {
      return;
    }

    this.assuntoService.deleteAssunto(id).subscribe({
      next: () => {
        this.assuntos = this.assuntos.filter(a => a.id !== id);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao excluir assunto';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  voltarParaMaterias(): void {
    this.router.navigate(['/materias']);
  }
}
