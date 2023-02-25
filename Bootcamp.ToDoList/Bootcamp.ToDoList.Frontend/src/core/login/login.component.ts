import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserApiService } from '../services/user-api.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  user = {} as User;

  constructor(private userApiService: UserApiService, private snackBar: MatSnackBar, private userService: UserService) { }

  onSubmit() {
    if (this.loginCheck()) {
      this.userApiService.login(this.user).subscribe({
        next: (token: string) => {
          this.userService.handleLogin(token, this.user);
        }, error: () => this.snackBar.open('Something went wrong...', 'OK')
      })
    }
  }

  loginCheck() {
    if (this.user.username !== undefined || this.user.password !== undefined) {
      if (this.user.username.length < 6 || this.user.password.length < 6) {
        this.snackBar.open('Please enter valid credentials.', 'OK');
        return false;
      }
      return true;
    }
    this.snackBar.open('Please enter valid credentials.', 'OK');
    return false;
  }
}
