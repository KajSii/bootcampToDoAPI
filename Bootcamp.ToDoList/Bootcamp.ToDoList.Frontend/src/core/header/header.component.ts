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

  username: string | null = localStorage.getItem("username");

  constructor(private userApiService: UserApiService, private router: Router) { }

  ngOnInit(): void {
  }
  
  isLoggedIn() {
    if (localStorage.getItem("token")) return true;
    return false;
  }

  onLogoutClick() {
    this.userApiService.logout();
    this.router.navigate(['/login']);
  }
}
