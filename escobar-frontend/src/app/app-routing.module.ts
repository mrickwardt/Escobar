import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { MustBeLoggedAuthGuard } from './authentication/auth.guard';

const routes: Routes = [
  { path: 'register', component: RegisterComponent, canActivate: [/*MustBeLoggedAuthGuard*/] },
  { path: '**', redirectTo: 'register' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
