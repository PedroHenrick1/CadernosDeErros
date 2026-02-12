export interface Erro {
  id: number;
  questao: string;
  respostaCorreta: string | null;
  minhaResposta: string | null;
  explicacao: string | null;
  observacoes: string | null;
  assuntoId: number;
  nomeAssunto: string;
  nomeMateria: string;
  dataErro: Date;
  dataRevisao: Date | null;
  revisado: boolean;
}

export interface CreateErroDto {
  questao: string;
  respostaCorreta?: string;
  minhaResposta?: string;
  explicacao?: string;
  observacoes?: string;
  assuntoId: number;
}
