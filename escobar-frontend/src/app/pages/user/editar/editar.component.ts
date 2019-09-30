import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserEditDto } from 'src/swagger/swag-proxy';

import { UserService } from '../register/user.service';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.scss']
})
export class EditarComponent implements OnInit {
  registerForm: FormGroup;
  passwordFailedRequirements: boolean;
  errorEditing: boolean;

  constructor(private formBuilder: FormBuilder, private userService: UserService, private router: Router) {
  }

  ngOnInit() {
    this.userService.getUser().subscribe(user => {
      this.registerForm = this.formBuilder.group({
        firstName: [user.nome.split(' ')[0], Validators.required],
        lastName: [user.nome.split(' ')[1], Validators.required],
        cpf: [user.cpf, Validators.required],
        login: [user.login, Validators.required],
        email: [user.email, [Validators.required, Validators.email]],
        oldPassword: ['', []],
        newPassword: ['', []],
        confirmPassword: ['', Validators.required]
      });
    });
  }
  save() {
    if (this.registerForm.valid) {
      this.errorEditing = false;
      const firstName = String(this.registerForm.get('firstName').value);
      const lastName = String(this.registerForm.get('lastName').value);
      const email = String(this.registerForm.get('email').value);
      const cpf = String(this.registerForm.get('cpf').value);
      const login = String(this.registerForm.get('login').value);
      const senhaAntiga = String(this.registerForm.get('oldPassword').value);
      const senhaNova = String(this.registerForm.get('newPassword').value);
      const confirmPassword = String(this.registerForm.get('confirmPassword').value)
      this.passwordFailedRequirements = (senhaAntiga || senhaNova || confirmPassword) &&
        !senhaAntiga || !senhaNova || !confirmPassword ||
        senhaAntiga.length < 6 || senhaNova.length < 6 || confirmPassword.length < 6;
      if (senhaNova !== confirmPassword || senhaAntiga === senhaNova || this.passwordFailedRequirements) {
        return;
      }
      const user: UserEditDto = new UserEditDto({ nome: firstName + ' ' + lastName, cpf, senhaAntiga, email, login, senha: senhaNova });
      this.userService.edit(user).subscribe(result => {
        if (result) {
          this.router.navigateByUrl('/home');
        } else {
          this.errorEditing = true;
        }
      });
    }
  }
}
