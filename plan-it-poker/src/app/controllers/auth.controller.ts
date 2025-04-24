import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthController {
  private apiUrl = `${environment.apiUrl}/api`;

  constructor(private http: HttpClient) {}

  login(credentials: { username: string; password: string }): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/auth/login`, credentials);
  }

  register(userData: { email: string; displayName: string; password: string }): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/auth/register`, userData);
  }

  // Mock admin user for testing
  getMockAdminUser(): User {
    return {
      id: '1',
      email: 'admin@example.com',
      displayName: 'Admin User',
      avatar: 'https://via.placeholder.com/150?text=A',
      role: 'Admin',
      createdAt: new Date(),
      lastLoginAt: new Date()
    };
  }
} 