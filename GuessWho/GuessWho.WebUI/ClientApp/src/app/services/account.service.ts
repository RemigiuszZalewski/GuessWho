import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:44377/api/Account/';

  constructor(private httpClient: HttpClient) { }

  login(email: string, password: string) {
    return this.httpClient.post<string>(this.baseUrl + 'Login', {
      email,
      password
    });
  }

  register(firstName: string, lastName: string, email: string, password: string) {
    return this.httpClient.post<string>(this.baseUrl + 'Register', {
      firstName,
      lastName,
      email,
      password
    });
  }
}
