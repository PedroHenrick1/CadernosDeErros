import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  {
    path: 'assuntos/:materiaId',
    renderMode: RenderMode.Server
  },
  {
    path: 'erros/:assuntoId',
    renderMode: RenderMode.Server
  },
  {
    path: '**',
    renderMode: RenderMode.Prerender
  }
];

