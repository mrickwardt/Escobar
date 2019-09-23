import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDtoRegister } from 'src/swagger/swag-proxy';

import { AuthenticationService } from '../authentication/authentication.service';
import { UserService } from './user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private authService: AuthenticationService,
    private router: Router) {
    if (this.authService.isLoggedIn()) {
      this.router.navigateByUrl('/home');
    }
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      login: ['', Validators.required],
      cpf: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
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
      const senha = this.registerForm.get('password').value;
      const confirmPassword = this.registerForm.get('confirmPassword').value;
      if (senha !== confirmPassword) {
        return;
      }
      const user: UserDtoRegister = new UserDtoRegister({ nome: firstName + ' ' + lastName, email, senha, cpf, login });
      this.userService.register(user).subscribe(result => {
        this.router.navigateByUrl('/home');
      });
    }
  }

}
