import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-movimento',
  templateUrl: './movimento.component.html',
  styleUrls: ['./movimento.component.scss']
})
export class MovimentoComponent implements OnInit {
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
