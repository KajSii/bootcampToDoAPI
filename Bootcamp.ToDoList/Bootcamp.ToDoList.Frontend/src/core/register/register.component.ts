import { Component } from '@angular/core';
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserApiService } from '../services/user-api.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  user = {} as User;
  passwordConfirm: string | undefined;

  constructor(private userApiService: UserApiService, private router: Router, private snackBar: MatSnackBar) { }

  onSubmit() {
    if (this.checkFormValidation()) {
      this.userApiService.register(this.user);
      this.userApiService.login(this.user);
      if (!localStorage.getItem("token")) {
        this.snackBar.open('Something went wrong...', 'OK');
        return;
      }
      this.router.navigate(['/item-list']);
    }
  }
  
  onLoginClick() {
    this.router.navigate(['/login']);
  }

  checkFormValidation() {
    if (this.user.username !== undefined || this.user.password !== undefined || this.passwordConfirm !== undefined) {
      if (this.user.username.length < 6 || this.user.password.length < 6) {
        this.snackBar.open('User name and password must be longer than 6.', 'OK');
        return false;
      }
      else if (this.user.password !== this.passwordConfirm) {
        this.snackBar.open('Password and confirm password my match.', 'OK');
        return false;
      }
      return true;
    }
    this.snackBar.open('Please enter valid credentials.', 'OK');
    return false;
  }
}