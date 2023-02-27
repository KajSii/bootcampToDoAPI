import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private snackBar: MatSnackBar, private router: Router) { }

  handleLogin(token: string | null, user: User): void {
    if (!token) {
      this.snackBar.open('Something went wrong...', 'OK');
      return;
    }
    localStorage.setItem('token', token);
    localStorage.setItem("username", user.username);
    this.router.navigate(['/item-list']);
  }
}
