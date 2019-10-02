import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ProdutoComponent } from './produto.component';


const routes: Routes = [
  {
    path: '',
    component: ProdutoComponent, children: [
      {
        path: 'create',
        component: CreateComponent
      },
      // {
      //   path: 'edit',
      //   component: EditComponent
      // },
      { path: '**', redirectTo: 'create' }
    ]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdutoRoutingModule { }
