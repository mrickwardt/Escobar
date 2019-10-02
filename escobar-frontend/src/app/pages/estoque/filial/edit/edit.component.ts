import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-filial-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  form: FormGroup;

  masterdetail = false;
  masterDetailData: any[] = [
    { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
    { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
    { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
    { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
    { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
    { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
    { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
    { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
    { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
    { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
  ];
  displayedColumns: string[] = ['nome', 'quantidade', 'valor', 'tipo'];
  dataSource = this.masterDetailData;

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

  save(){
    if(this.form.valid){
      // this.service.create();
    }
  }

  openMasterdetail(element: any) {
    this.masterdetail = true;
  }
  closeMasterdetail(){
    this.masterdetail = false;
  }
}