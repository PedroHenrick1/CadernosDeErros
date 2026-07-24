export interface RegisterDto {
  nome: string;
  email: string;
  senha: string;
}

export interface LoginDto {
  email: string;
  senha: string;
}

export interface AuthResponse {
  id: number;
  nome: string;
  email: string;
  token: string;
  expiration: string;
}

export interface UserProfile {
  id: number;
  nome: string;
  email: string;
  dataCriacao?: string;
}
