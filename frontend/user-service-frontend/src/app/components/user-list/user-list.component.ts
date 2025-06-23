import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user.model';
import { debounceTime, distinctUntilChanged, Subject } from 'rxjs';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  totalCount: number = 0;
  currentPage: number = 1;
  pageSize: number = 10;
  isLoading: boolean = false;
  errorMessage: string = '';
  searchQuery: string = '';
  private searchSubject = new Subject<string>();

  constructor(private userService: UserService) {
    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(query => {
      this.searchQuery = query;
      this.currentPage = 1;
      this.loadUsers();
    });
  }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.isLoading = true;
    this.errorMessage = '';

    if (this.searchQuery) {
      this.userService.searchUsers(this.searchQuery).subscribe({
        next: (users) => {
          this.users = users;
          this.totalCount = users.length;
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'An error occurred while loading users';
        },
        complete: () => {
          this.isLoading = false;
        }
      });
    } else {
      this.userService.getUsers(this.currentPage, this.pageSize).subscribe({
        next: (response) => {
          this.users = response.users;
          this.totalCount = response.totalCount;
        },
        error: (error) => {
          this.errorMessage = error.error.message || 'An error occurred while loading users';
        },
        complete: () => {
          this.isLoading = false;
        }
      });
    }
  }

  onSearch(query: string): void {
    this.searchSubject.next(query);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadUsers();
  }

  get totalPages(): number {
    return Math.ceil(this.totalCount / this.pageSize);
  }

  get pages(): number[] {
    const pages: number[] = [];
    for (let i = 1; i <= this.totalPages; i++) {
      pages.push(i);
    }
    return pages;
  }
} 