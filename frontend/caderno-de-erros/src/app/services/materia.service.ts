import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Materia, CreateMateriaDto } from '../models/materia.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MateriaService {
  private apiUrl = `${environment.apiUrl}/api/Materia`;

  constructor(private http: HttpClient) { }

  getMaterias(): Observable<Materia[]> {
    return this.http.get<Materia[]>(this.apiUrl);
  }

  getMateria(id: number): Observable<Materia> {
    return this.http.get<Materia>(`${this.apiUrl}/${id}`);
  }

  createMateria(materia: CreateMateriaDto): Observable<Materia> {
    return this.http.post<Materia>(this.apiUrl, materia);
  }

  updateMateria(id: number, materia: CreateMateriaDto): Observable<Materia> {
    return this.http.put<Materia>(`${this.apiUrl}/${id}`, materia);
  }

  deleteMateria(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
