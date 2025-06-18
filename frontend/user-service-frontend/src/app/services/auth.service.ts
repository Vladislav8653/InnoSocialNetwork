import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { LoginDto, RegisterDto, AuthResponse, PasswordResetRequestDto, ResetPasswordDto } from '../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:2000/api/users';
  private currentUserSubject = new BehaviorSubject<AuthResponse | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    const storedUser = localStorage.getItem('currentUser');
    if (storedUser) {
      this.currentUserSubject.next(JSON.parse(storedUser));
    }
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An error occurred';
    if (error.error instanceof ErrorEvent) {
      // Client-side error
      errorMessage = error.error.message;
    } else {
      // Server-side error
      errorMessage = error.error?.message || `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(() => new Error(errorMessage));
  }

  login(loginDto: LoginDto): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, loginDto).pipe(
      tap(response => {
        localStorage.setItem('currentUser', JSON.stringify(response));
        this.currentUserSubject.next(response);
      }),
      catchError(this.handleError)
    );
  }

  register(registerDto: RegisterDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/register`, registerDto).pipe(
      catchError(this.handleError)
    );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  requestPasswordReset(email: string): Observable<void> {
    const dto: PasswordResetRequestDto = { email };
    return this.http.post<void>(`${this.apiUrl}/request-password-reset`, dto).pipe(
      catchError(this.handleError)
    );
  }

  resetPassword(dto: ResetPasswordDto): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/reset-password`, dto).pipe(
      catchError(this.handleError)
    );
  }

  isAuthenticated(): boolean {
    return !!this.currentUserSubject.value;
  }

  getAccessToken(): string | null {
    return this.currentUserSubject.value?.accessToken || null;
  }

  getRefreshToken(): string | null {
    return this.currentUserSubject.value?.refreshToken || null;
  }
} 