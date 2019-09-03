import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DefaultLayoutComponent } from './containers';
import { AuthGuard } from './core/auth/auth.guard';
import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { LoginCallbackComponent } from './views/login/login-callback.component';
import { LoginComponent } from './views/login/login.component';
import { RegisterComponent } from './views/register/register.component';
import { UnauthorizedComponent } from './views/unauthorized/unauthorized.component';


// Import Containers
export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full', },
  {
    path: '404',
    component: P404Component,
    data: {
      title: 'Page 404'
    }
  },
  {
    path: '500',
    component: P500Component,
    data: {
      title: 'Page 500'
    }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: {
      title: 'Login Page'
    }
  },
  {
    path: 'register',
    component: RegisterComponent,
    data: {
      title: 'Register Page'
    }
  },
  { // -- Feature from this project
    path: 'login-callback',
    component: LoginCallbackComponent,
    data: {
      title: 'Login Page'
    }
  },
  { // -- Feature from this project
    path: 'unauthorized',
    component: UnauthorizedComponent,
    data: {
      title: 'Unauthorized Page'
    }
  },
  {
    path: '',
    component: DefaultLayoutComponent,
    canActivate: [AuthGuard], // -- Feature from this project
    data: {
      title: 'Home'
    },
    children: [
      { path: 'dashboard', loadChildren: './views/dashboard/dashboard.module#DashboardModule' },
      { path: '**', redirectTo: 'dashboard' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [AuthGuard], // -- AQUI
  exports: [RouterModule]
})
export class AppRoutingModule { }
