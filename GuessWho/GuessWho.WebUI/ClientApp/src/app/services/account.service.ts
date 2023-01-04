import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from '@angular/router';
import {Observable} from "rxjs";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:44377/api/Account/';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(email: string, password: string) : Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + 'Login', {
      email,
      password
    });
  }

  register(firstName: string, lastName: string, email: string, password: string) : Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + 'Register', {
      firstName,
      lastName,
      email,
      password
    });
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  get isLoggedIn(): boolean {
    let authToken = localStorage.getItem('access_token');
    return authToken !== null;
  }

  doLogout() {
    localStorage.removeItem('access_token');
    this.router.navigateByUrl("/login");
  }
}
