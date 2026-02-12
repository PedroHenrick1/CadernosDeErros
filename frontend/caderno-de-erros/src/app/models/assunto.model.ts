export interface Assunto {
  id: number;
  nome: string;
  materiaId: number;
  nomeMateria: string;
  dataCriacao: Date;
  quantidadeErros: number;
}

export interface CreateAssuntoDto {
  nome: string;
  materiaId: number;
}
