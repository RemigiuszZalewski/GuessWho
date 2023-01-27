import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {AccountService} from "../../services/account.service";

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  form: FormGroup = new FormGroup({
    email: new FormControl('')
  });

  submitted = false;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
        email: ['', Validators.compose([Validators.required, Validators.email])]
      }
    )
  }

  forgotPassword() {
    this.accountService.resetPassword(this.form.get('email')?.value).subscribe({
      error: (error) => console.log(error)
    })
  }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.forgotPassword();
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
}
