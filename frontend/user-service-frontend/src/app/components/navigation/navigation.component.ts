import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  isAuthenticated: boolean = false;
  currentUser: User | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.isAuthenticated = !!user;
      if (user) {
        // Fetch user data using the access token
        this.http.get<User>('http://localhost:2000/api/users/profile', {
          headers: { Authorization: `Bearer ${user.accessToken}` }
        }).subscribe({
          next: (userData) => {
            this.currentUser = userData;
          },
          error: (error) => {
            console.error('Error fetching user data:', error);
            this.currentUser = null;
          }
        });
      } else {
        this.currentUser = null;
      }
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
} 