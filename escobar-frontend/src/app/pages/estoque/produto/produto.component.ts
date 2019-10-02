import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent implements OnInit {
  navLinks = [
    {
      label: 'Cadastrar',
      link: './first',
      index: 0
    }, {
      label: 'Editar',
      link: './edit',
      index: 1
    }
  ];
  constructor() { }

  ngOnInit() {
  }

}
