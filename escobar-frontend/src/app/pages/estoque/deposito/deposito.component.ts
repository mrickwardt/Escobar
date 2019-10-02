import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deposito',
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.scss']
})
export class DepositoComponent implements OnInit {
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
