import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler } from "@angular/common/http";
import { AccountService } from "../services/account.service";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService) { }
  intercept(request: HttpRequest<any>, next: HttpHandler) {
    const authToken = this.accountService.getToken();

    request = request.clone({
      setHeaders: {
        Authorization: "Bearer " + authToken
      }
    });

    return next.handle(request);
  }
}
