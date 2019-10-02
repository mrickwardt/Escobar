import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-deposito-create',
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
      quantidade: ['', Validators.required],
      valor: ['', Validators.required],
      tipo: ['', Validators.required]
    });
  }

  save(){
    if(this.form.valid){
      // this.service.create();
    }
  }
}
