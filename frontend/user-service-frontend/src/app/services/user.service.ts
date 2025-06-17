import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, UserUpdateDto } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:2000';

  constructor(private http: HttpClient) { }

  getCurrentUser(): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/me`);
  }

  updateUser(userId: string, updateDto: UserUpdateDto): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${userId}`, updateDto);
  }

  deleteUser(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${userId}`);
  }

  uploadProfilePicture(userId: string, file: File): Observable<User> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<User>(`${this.apiUrl}/${userId}/profile-picture`, formData);
  }

  searchUsers(query: string): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/search`, { params: { query } });
  }

  getUsers(page: number = 1, pageSize: number = 10): Observable<{ users: User[], totalCount: number }> {
    return this.http.get<{ users: User[], totalCount: number }>(`${this.apiUrl}`, {
      params: { page: page.toString(), pageSize: pageSize.toString() }
    });
  }
} 