import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-produto',
  templateUrl: './produto.component.html',
  styleUrls: ['./produto.component.scss']
})
export class ProdutoComponent implements OnInit {
  navLinks = [
    {
      label: 'Visualizar',
      link: './edit',
      index: 1
    },
    {
      label: 'Cadastrar',
      link: './create',
      index: 0
    }
  ];
  constructor() { }

  ngOnInit() {
  }

}
