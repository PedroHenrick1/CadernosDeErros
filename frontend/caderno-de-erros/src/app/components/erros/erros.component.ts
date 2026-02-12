import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ErroService } from '../../services/erro.service';
import { AssuntoService } from '../../services/assunto.service';
import { Erro, CreateErroDto } from '../../models/erro.model';
import { Assunto } from '../../models/assunto.model';

@Component({
  selector: 'app-erros',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './erros.component.html',
  styleUrls: ['./erros.component.css']
})
export class ErrosComponent implements OnInit {
  erros: Erro[] = [];
  assuntos: Assunto[] = [];
  assunto: Assunto | null = null;
  assuntoId: number | null = null;
  novoErro: CreateErroDto = {
    questao: '',
    respostaCorreta: '',
    minhaResposta: '',
    explicacao: '',
    observacoes: '',
    assuntoId: 0
  };
  loading = false;
  error: string | null = null;
  mostrarFormulario = false;

  constructor(
    private erroService: ErroService,
    private assuntoService: AssuntoService,
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      if (params['assuntoId']) {
        this.assuntoId = +params['assuntoId'];
        this.novoErro.assuntoId = this.assuntoId;
        this.carregarAssunto(this.assuntoId);
        this.carregarErrosPorAssunto(this.assuntoId);
      } else {
        this.carregarTodosAssuntos();
        this.carregarTodosErros();
      }
    });
  }

  carregarAssunto(id: number): void {
    this.assuntoService.getAssunto(id).subscribe({
      next: (data) => {
        this.assunto = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar assunto';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarTodosAssuntos(): void {
    this.assuntoService.getAssuntos().subscribe({
      next: (data) => {
        this.assuntos = data;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar assuntos';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarErrosPorAssunto(assuntoId: number): void {
    this.loading = true;
    this.error = null;
    this.erroService.getErrosByAssunto(assuntoId).subscribe({
      next: (data) => {
        this.erros = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar erros';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  carregarTodosErros(): void {
    this.loading = true;
    this.error = null;
    this.erroService.getErros().subscribe({
      next: (data) => {
        this.erros = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao carregar erros';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  toggleFormulario(): void {
    this.mostrarFormulario = !this.mostrarFormulario;
  }

  criarErro(): void {
    if (!this.novoErro.questao.trim() || !this.novoErro.assuntoId) {
      return;
    }

    this.loading = true;
    this.erroService.createErro(this.novoErro).subscribe({
      next: (erro) => {
        this.erros.unshift(erro);
        this.resetarFormulario();
        this.mostrarFormulario = false;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao criar registro de erro';
        this.loading = false;
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  resetarFormulario(): void {
    this.novoErro = {
      questao: '',
      respostaCorreta: '',
      minhaResposta: '',
      explicacao: '',
      observacoes: '',
      assuntoId: this.assuntoId || 0
    };
  }

  marcarRevisado(erro: Erro): void {
    this.erroService.marcarComoRevisado(erro.id).subscribe({
      next: (erroAtualizado) => {
        const index = this.erros.findIndex(e => e.id === erro.id);
        if (index !== -1) {
          this.erros[index] = erroAtualizado;
        }
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao marcar como revisado';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  excluirErro(id: number): void {
    if (!confirm('Deseja realmente excluir este erro?')) {
      return;
    }

    this.erroService.deleteErro(id).subscribe({
      next: () => {
        this.erros = this.erros.filter(e => e.id !== id);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao excluir';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  voltarParaAssuntos(): void {
    if (this.assuntoId && this.assunto) {
      this.router.navigate(['/assuntos', this.assunto.materiaId]);
    } else {
      this.router.navigate(['/assuntos']);
    }
  }

  formatarData(data: Date | null): string {
    if (!data) return '-';
    return new Date(data).toLocaleDateString('pt-BR');
  }
}
