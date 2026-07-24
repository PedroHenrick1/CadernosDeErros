import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { MateriasComponent } from './components/materias/materias.component';
import { AssuntosComponent } from './components/assuntos/assuntos.component';
import { ErrosComponent } from './components/erros/erros.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'materias', component: MateriasComponent, canActivate: [authGuard] },
  { path: 'assuntos', component: AssuntosComponent, canActivate: [authGuard] },
  { path: 'assuntos/:materiaId', component: AssuntosComponent, canActivate: [authGuard] },
  { path: 'erros', component: ErrosComponent, canActivate: [authGuard] },
  { path: 'erros/:assuntoId', component: ErrosComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: '' }
];
