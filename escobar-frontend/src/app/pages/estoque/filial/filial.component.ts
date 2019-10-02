import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filial',
  templateUrl: './filial.component.html',
  styleUrls: ['./filial.component.scss']
})
export class FilialComponent implements OnInit {
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
