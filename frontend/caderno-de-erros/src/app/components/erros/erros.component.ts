import { Component, OnInit, ChangeDetectorRef, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ErroService } from '../../services/erro.service';
import { AssuntoService } from '../../services/assunto.service';
import { AuthService } from '../../services/auth.service';
import { Erro, CreateErroDto } from '../../models/erro.model';
import { Assunto } from '../../models/assunto.model';

@Component({
  selector: 'app-erros',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
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
  itemParaExcluir: Erro | null = null;

  constructor(
    private erroService: ErroService,
    private assuntoService: AssuntoService,
    public authService: AuthService,
    private route: ActivatedRoute,
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
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }
    this.mostrarFormulario = !this.mostrarFormulario;
  }

  criarErro(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

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
        this.error = 'Erro ao criar registro de erro. Verifique sua autenticação.';
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

  filtroStatus: 'todos' | 'pendentes' | 'revisados' = 'todos';
  termoBusca = '';
  sucessoFeedback: string | null = null;

  get errosFiltrados(): Erro[] {
    return this.erros.filter(erro => {
      // Filtro por status
      if (this.filtroStatus === 'pendentes' && erro.revisado) return false;
      if (this.filtroStatus === 'revisados' && !erro.revisado) return false;

      // Filtro por termo de busca
      if (this.termoBusca.trim()) {
        const termo = this.termoBusca.toLowerCase();
        const noEnunciado = erro.questao.toLowerCase().includes(termo);
        const naMateria = erro.nomeMateria?.toLowerCase().includes(termo);
        const noAssunto = erro.nomeAssunto?.toLowerCase().includes(termo);
        const naExplicacao = erro.explicacao?.toLowerCase().includes(termo);
        return noEnunciado || naMateria || noAssunto || naExplicacao;
      }

      return true;
    });
  }

  setFiltroStatus(status: 'todos' | 'pendentes' | 'revisados'): void {
    this.filtroStatus = status;
  }

  marcarRevisado(erro: Erro): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.erroService.marcarComoRevisado(erro.id).subscribe({
      next: (erroAtualizado) => {
        const index = this.erros.findIndex(e => e.id === erro.id);
        if (index !== -1) {
          this.erros[index] = erroAtualizado;
        }
        this.exibirSucesso('🎉 Parabéns! Erro marcado como revisado com sucesso.');
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao marcar como revisado';
        this.cdr.detectChanges();
        console.error(err);
      }
    });
  }

  exibirSucesso(mensagem: string): void {
    this.sucessoFeedback = mensagem;
    setTimeout(() => {
      this.sucessoFeedback = null;
      this.cdr.detectChanges();
    }, 4000);
  }

  solicitarExclusao(erro: Erro): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }
    this.itemParaExcluir = erro;
  }

  cancelarExclusao(): void {
    this.itemParaExcluir = null;
  }

  confirmarExclusao(): void {
    if (!this.itemParaExcluir) return;
    const id = this.itemParaExcluir.id;
    this.erroService.deleteErro(id).subscribe({
      next: () => {
        this.erros = this.erros.filter(e => e.id !== id);
        this.itemParaExcluir = null;
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.error = 'Erro ao excluir o registro de erro.';
        this.itemParaExcluir = null;
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

  get totalErrosCount(): number {
    return this.erros.length;
  }

  get errosRevisadosCount(): number {
    return this.erros.filter(e => e.revisado).length;
  }

  get errosPendentesCount(): number {
    return this.erros.filter(e => !e.revisado).length;
  }

  get taxaRetencao(): number {
    if (this.erros.length === 0) return 0;
    return Math.round((this.errosRevisadosCount / this.erros.length) * 100);
  }

  aplicarTagExplicacao(tag: string): void {
    const prefixo = `[${tag}]`;
    if (!this.novoErro.explicacao) {
      this.novoErro.explicacao = prefixo + ' ';
    } else if (!this.novoErro.explicacao.includes(prefixo)) {
      this.novoErro.explicacao = `${prefixo} ${this.novoErro.explicacao}`;
    }
  }
}
