import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MustBeLoggedAuthGuard } from '../authentication/auth.guard';
import { PagesComponent } from './pages.component';

const routes: Routes = [
  {
    path: 'user',
    loadChildren: () => import('./user/user.module').then(m => m.UserModule)
  },
  {
    path: '',
    component: PagesComponent,
    canActivate: [MustBeLoggedAuthGuard]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
