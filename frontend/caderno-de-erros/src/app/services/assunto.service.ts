import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Assunto, CreateAssuntoDto } from '../models/assunto.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AssuntoService {
  private apiUrl = `${environment.apiUrl}/api/Assunto`;

  constructor(private http: HttpClient) { }

  getAssuntos(): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(this.apiUrl);
  }

  getAssunto(id: number): Observable<Assunto> {
    return this.http.get<Assunto>(`${this.apiUrl}/${id}`);
  }

  getAssuntosByMateria(materiaId: number): Observable<Assunto[]> {
    return this.http.get<Assunto[]>(`${this.apiUrl}/Materia/${materiaId}`);
  }

  createAssunto(assunto: CreateAssuntoDto): Observable<Assunto> {
    return this.http.post<Assunto>(this.apiUrl, assunto);
  }

  updateAssunto(id: number, assunto: CreateAssuntoDto): Observable<Assunto> {
    return this.http.put<Assunto>(`${this.apiUrl}/${id}`, assunto);
  }

  deleteAssunto(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
