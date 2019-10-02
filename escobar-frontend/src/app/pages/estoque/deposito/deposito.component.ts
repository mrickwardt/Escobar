import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-deposito',
  templateUrl: './deposito.component.html',
  styleUrls: ['./deposito.component.scss']
})
export class DepositoComponent implements OnInit {
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
