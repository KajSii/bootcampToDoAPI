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

  constructor(private http: HttpClient, private router: Router, private snackBar: MatSnackBar) { }

  register(user: User): Observable<string> {
    return this.http.post<string>(`${AUTH_BASE_URL}/register`, user);
  }


  login(user: User) {
    return this.http.post<string>(`${AUTH_BASE_URL}/login`, user);
  }


  isLoggedIn() {
    return !!localStorage.getItem("token");
  }


  getUsername() {
    return localStorage.getItem("username");
  }


  logout() {
    localStorage.removeItem("username");
    localStorage.removeItem("token");
  }
}