import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from "../../services/account.service";
import {AbstractControl, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ConfirmPasswordValidator} from "../../validators/confirm-password.validator";
import {Router} from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  form: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl('')
  });

  submitted = false;

  constructor(private accountService: AccountService, private formBuilder: FormBuilder,
              private router: Router) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group({
        firstName: ['', Validators.compose(
          [Validators.required, Validators.minLength(3), Validators.maxLength(15)])],
        lastName: ['', Validators.compose(
          [Validators.required, Validators.minLength(2), Validators.maxLength(15)])],
        email: ['', Validators.compose([Validators.required, Validators.email])],
        password: ['', Validators.required],
        confirmPassword: ['', Validators.required]
      },
      { validators: ConfirmPasswordValidator('password', 'confirmPassword') }
    )
  }

  onSubmit() {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    this.register();
    this.router.navigate(['/dashboard']);
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  register() {
    this.accountService.register(this.form.get('firstName')?.value, this.form.get('lastName')?.value,
      this.form.get('email')?.value, this.form.get('password')?.value).subscribe({
      next: (response) => {
        localStorage.setItem('access_token', JSON.stringify(response.token));
        localStorage.setItem('fullName', JSON.stringify(response.fullName));
      },
      error: (error) => console.log(error)
    })}
}

