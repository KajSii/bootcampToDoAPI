import { Component } from '@angular/core';
import { AbstractControl, FormControl, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { catchError, filter, mergeMap, of, switchMap } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserApiService } from '../services/user-api.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  user = {} as User;
  passwordConfirm: string | undefined;

  constructor(private userApiService: UserApiService, private router: Router, private snackBar: MatSnackBar, private userService: UserService) { }

  onSubmit() {
    if (this.checkFormValidation()) {
      this.userApiService.register(this.user).pipe(
        mergeMap((id: number) => {
          if (id) {
            return this.userApiService.login(this.user);
          } else {
            return of(null);
          }
        }),
        catchError(() => of(null))
      ).subscribe({
        next: (token: string | null) => {
          console.log(token)
          this.userService.handleLogin(token, this.user);
        },
        error: () => this.snackBar.open('Something went wrong...', 'OK')
      });
    }
  }

  onLoginClick() {
    this.router.navigate(['/login']);
  }

  checkFormValidation() {
    if (!this.user.username || !this.user.password || !this.passwordConfirm) {
      this.snackBar.open('Please enter valid credentials.', 'OK');
      return false;
    }
    if (this.user.username.length < 6 || this.user.password.length < 6) {
      this.snackBar.open('User name and password must be longer than 6.', 'OK');
      return false;
    }
    if (this.user.password !== this.passwordConfirm) {
      this.snackBar.open('Password and confirm password my match.', 'OK');
      return false;
    }
    return true;
  }
}