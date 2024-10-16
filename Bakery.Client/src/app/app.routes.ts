import { Routes } from '@angular/router';
import {AuthPageComponent} from './components/pages/auth-page/auth-page.component';
import {MainPageComponent} from './components/pages/main-page/main-page.component';
import {authGuard} from './guards/auth.guard';
import {NotFoundPageComponent} from './components/pages/not-found-page/not-found-page.component';

export const routes: Routes = [
  {
    path: '',
    title: 'Авторизация',
    component: AuthPageComponent
  },
  {
    path: 'auth',
    title: 'Авторизация',
    component: AuthPageComponent
  },
  {
    path: 'main',
    title: 'Пекарня',
    component: MainPageComponent,
    canActivate: [authGuard]
  },
  {
    path: '**',
    title: 'Страница не найдена',
    component: NotFoundPageComponent,
  }
];
