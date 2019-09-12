import { Component, OnInit } from '@angular/core';
import { UserAccess } from 'src/swagger/swag-proxy';

import { AuthenticationService } from '../authentication/authentication.service';
import { UserService } from '../register/user.service';

@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss']
})
export class PagesComponent implements OnInit {
  history: UserAccess[];

  constructor(private authService: AuthenticationService, private userService: UserService) { }

  ngOnInit() {
    this.authService.runInitialLoginSequence().then(() => {
      this.userService.getHistory().subscribe(result => {
        this.history = result;
        console.log(this.history);
      });
    });
  }

}
