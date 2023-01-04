import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

  form: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl('')
  });

  submitted = false;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.required]
      }
    )
  }

  login() {
    this.accountService.login(this.form.get('email')?.value, this.form.get('password')?.value).subscribe({
      next: (response) => {
        localStorage.setItem('access_token', JSON.stringify(response.token));
      },
      error: (error) => console.log(error)
    })}

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.login();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
}
