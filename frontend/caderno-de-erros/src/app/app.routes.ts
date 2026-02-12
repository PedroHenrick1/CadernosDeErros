import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MateriasComponent } from './components/materias/materias.component';
import { AssuntosComponent } from './components/assuntos/assuntos.component';
import { ErrosComponent } from './components/erros/erros.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'materias', component: MateriasComponent },
  { path: 'assuntos', component: AssuntosComponent },
  { path: 'assuntos/:materiaId', component: AssuntosComponent },
  { path: 'erros', component: ErrosComponent },
  { path: 'erros/:assuntoId', component: ErrosComponent },
  { path: '**', redirectTo: '' }
];
