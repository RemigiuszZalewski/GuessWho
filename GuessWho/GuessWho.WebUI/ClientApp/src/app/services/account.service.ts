import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Router} from '@angular/router';
import {BehaviorSubject, Observable, tap} from "rxjs";
import {User} from "../models/user";
import {environment} from "../../environments/environment.prod";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.baseUrl + 'Account'
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(email: string, password: string) : Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + '/Login', {
      email,
      password
    }).pipe(
      tap((response : User) => {
        localStorage.setItem('access_token', JSON.stringify(response.token));
        this._isLoggedIn$.next(true);
      }
    ));
  }

  register(firstName: string, lastName: string, email: string, password: string) : Observable<User> {
    return this.httpClient.post<User>(this.baseUrl + '/Register', {
      firstName,
      lastName,
      email,
      password
    });
  }

  resetPassword(email: string) {
    return this.httpClient.post(this.baseUrl + '/Password/Reset', {
      email
    });
  }

  changePassword(oldPassword: string, newPassword: string, userId: number) {
    return this.httpClient.patch(this.baseUrl + '/Password/Edit', {
      oldPassword,
      newPassword,
      userId
    });
  }

  getToken() {
    return localStorage.getItem('access_token');
  }

  doLogout() {
    localStorage.removeItem('access_token');
    localStorage.removeItem('fullName');

    this._isLoggedIn$.next(false);

    this.router.navigate(['/login']);
  }
}
