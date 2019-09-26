import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { UserService } from '../register/user.service';

@Component({
  selector: 'app-editar',
  templateUrl: './editar.component.html',
  styleUrls: ['./editar.component.scss']
})
export class EditarComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private userService: UserService) { 
    userService.getUser();
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      cpf: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      oldPassword: ['',[Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });
  }
  save() {
    if (this.registerForm.valid) {
      const firstName = this.registerForm.get('firstName').value;
      const lastName = this.registerForm.get('lastName').value;
      const login = this.registerForm.get('login').value;
      const email = this.registerForm.get('email').value;
      const cpf = this.registerForm.get('cpf').value;
      const senhaAntiga = this.registerForm.get('oldPassword').value;
      const senhaNova = this.registerForm.get('newPassword').value;
      const confirmPassword = this.registerForm.get('confirmPassword').value;
      if (senhaNova !== confirmPassword) {
        return;
      }
      if (senhaAntiga === senhaNova) {
        return;
      }
      // const user: UserDtoRegister = new UserDtoRegister({ nome: firstName + ' ' + lastName, email, senha, cpf, login });
      // this.userService.register(user).subscribe(result => {
      //   this.router.navigateByUrl('/home');
      // });
    }
  }
}
