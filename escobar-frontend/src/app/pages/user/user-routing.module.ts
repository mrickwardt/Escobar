import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EditarComponent } from './editar/editar.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'edit',
    component: EditarComponent
  },
  { path: '**', redirectTo: 'register' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
