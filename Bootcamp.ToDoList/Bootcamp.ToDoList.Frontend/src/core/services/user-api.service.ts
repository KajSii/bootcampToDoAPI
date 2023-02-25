import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Observable, Subject } from "rxjs";
import { User } from '../../app/models/user';

const AUTH_BASE_URL = 'api/auth'

@Injectable({
  providedIn: "root"
})
export class UserApiService {

  // isLoggedIn$ = new Subject<boolean>();
  user = {} as User;

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) { }

  register(user: User) {
    return this.http.post(`${AUTH_BASE_URL}/register`, user);
  }


  login(user: User) {
    return this.http.post<string>(`${AUTH_BASE_URL}/login`, user).subscribe({
      next: (token: string) => {
        if (!token) {
          this.snackBar.open('No validation token added.', 'OK');
          return;
        }
        localStorage.setItem('token', token);
        localStorage.setItem("username", user.username);
        localStorage.setItem("isLoggedIn", 'true');
        this.router.navigate(['/item-list']);
      },
      error: (error) => this.snackBar.open('Something went wrong...', 'OK')
    });
  }


  isLoggedIn() {
    return (localStorage.getItem("isLoggedIn") === "true");
  }


  getUsername() {
    return localStorage.getItem("username");
  }


  logout() {
    this.user.username = '';
    this.user.password = '';
    localStorage.removeItem("username");
    localStorage.removeItem("token");
    localStorage.setItem("isLoggedIn", String(false));
    // this.isLoggedIn$.next(false);
  }
}