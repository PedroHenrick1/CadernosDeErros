import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Erro, CreateErroDto } from '../models/erro.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ErroService {
  private apiUrl = `${environment.apiUrl}/api/Erro`;

  constructor(private http: HttpClient) { }

  getErros(): Observable<Erro[]> {
    return this.http.get<Erro[]>(this.apiUrl);
  }

  getErro(id: number): Observable<Erro> {
    return this.http.get<Erro>(`${this.apiUrl}/${id}`);
  }

  getErrosByAssunto(assuntoId: number): Observable<Erro[]> {
    return this.http.get<Erro[]>(`${this.apiUrl}/Assunto/${assuntoId}`);
  }

  createErro(erro: CreateErroDto): Observable<Erro> {
    return this.http.post<Erro>(this.apiUrl, erro);
  }

  updateErro(id: number, erro: any): Observable<Erro> {
    return this.http.put<Erro>(`${this.apiUrl}/${id}`, erro);
  }

  marcarComoRevisado(id: number): Observable<Erro> {
    return this.http.patch<Erro>(`${this.apiUrl}/${id}/revisado`, {});
  }

  deleteErro(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
