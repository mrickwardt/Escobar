import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-movimento',
  templateUrl: './movimento.component.html',
  styleUrls: ['./movimento.component.scss']
})
export class MovimentoComponent implements OnInit {
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
