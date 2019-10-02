import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filial',
  templateUrl: './filial.component.html',
  styleUrls: ['./filial.component.scss']
})
export class FilialComponent implements OnInit {
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
