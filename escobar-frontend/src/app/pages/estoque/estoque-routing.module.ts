import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
    path: 'produto',
    loadChildren: () => import('./produto/produto.module').then(m => m.ProdutoModule)
  },
  {
    path: 'deposito',
    loadChildren: () => import('./deposito/deposito.module').then(m => m.DepositoModule)
  },
  {
    path: 'movimento',
    loadChildren: () => import('./movimento/movimento.module').then(m => m.MovimentoModule)
  },
  {
    path: 'filial',
    loadChildren: () => import('./filial/filial.module').then(m => m.FilialModule)
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EstoqueRoutingModule { }
