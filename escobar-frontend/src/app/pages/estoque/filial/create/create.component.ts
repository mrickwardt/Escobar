import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-filial-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent implements OnInit {
  form: FormGroup;

  constructor(private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ['', Validators.required],
      enderecoRua: ['', Validators.required],
      enderecoBairro: ['', Validators.required],
      enderecoCidade: ['', Validators.required],
      enderecoNúmero: ['', Validators.required],
      enderecoCEP: ['', Validators.required],
      contato: ['', Validators.required],
    });
  }

  save() {
    if (this.form.valid) {
      // this.service.create();
    }
  }
}