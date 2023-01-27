import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../services/account.service";
import {Observable} from "rxjs";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(public accountService: AccountService) { }

  ngOnInit(): void { }

  onLogout() {
    this.accountService.doLogout();
  }
}
