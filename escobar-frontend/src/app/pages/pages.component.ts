import { Component, OnInit } from '@angular/core';
import { UserAccess } from 'src/swagger/swag-proxy';

import { UserService } from './user/register/user.service';

@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.scss']
})
export class PagesComponent implements OnInit {
  historyList: UserAccess[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getHistory().subscribe(result => {
      this.historyList = result;
    });
  }

}
