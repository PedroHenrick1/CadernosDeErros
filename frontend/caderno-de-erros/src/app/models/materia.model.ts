export interface Materia {
  id: number;
  nome: string;
  dataCriacao: Date;
  quantidadeAssuntos: number;
}

export interface CreateMateriaDto {
  nome: string;
}
