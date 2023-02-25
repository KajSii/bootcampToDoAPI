import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserApiService } from '../services/user-api.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit{

  username: string | null | undefined;

  constructor(private userApiService: UserApiService, private router: Router) { }

  ngOnInit(): void {
    this.username = localStorage.getItem("username");
  }
  
  isLoggedIn() {
    // this.isLoggedIn$ = this.userApiService.isLoggedIn();
    return this.userApiService.isLoggedIn();
  }

  onLogoutClick() {
    this.userApiService.logout();
    this.router.navigate(['/login']);
  }
}
