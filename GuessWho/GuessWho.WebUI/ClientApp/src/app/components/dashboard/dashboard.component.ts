import { Component, OnInit } from '@angular/core';
import {AccountService} from "../../services/account.service";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  userName = '';

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.userName = this.getUser();
  }

  getUser() {
    let fullName = localStorage.getItem('fullName');

    if (fullName === null) {
      return '';
    }

    return fullName;
  }
}
