import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Input() firstName = "";
  @Input() lastName = "";
  @Input() email = "";
  @Input() password = "";

  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.register();
  }

  register() {
    this.accountService.register(this.firstName, this.lastName, this.email, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('token', JSON.stringify(response));
      },
      error: (error) => console.log(error)
    })}
  }
