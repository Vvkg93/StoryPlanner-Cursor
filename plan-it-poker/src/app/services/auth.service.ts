import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { User } from '../models/user.model';
import { AuthController } from '../controllers/auth.controller';

export type UserRole = "Admin" | "User" | "Viewer";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();
  private roles: UserRole[] = ["Admin", "User", "Viewer"];

  constructor(private authController: AuthController) {
    const storedUser = localStorage.getItem('currentUser');
    if (storedUser) {
      this.currentUserSubject.next(JSON.parse(storedUser));
    }
  }

  login(credentials: { username: string; password: string }): Observable<User> {
    // Check for admin credentials
    if (credentials.username === 'admin' && credentials.password === 'admin123') {
      const adminUser: User = {
        id: 'admin',
        email: 'admin@example.com',
        displayName: 'Admin User',
        avatar: 'https://via.placeholder.com/150?text=A',
        role: 'Admin',
        createdAt: new Date(),
        lastLoginAt: new Date()
      };
      
      localStorage.setItem('currentUser', JSON.stringify(adminUser));
      this.currentUserSubject.next(adminUser);
      return new Observable<User>(observer => {
        observer.next(adminUser);
        observer.complete();
      });
    }
    
    return this.authController.login(credentials).pipe(
      tap(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
      })
    );
  }

  register(userData: { email: string; displayName: string; password: string }): Observable<User> {
    return this.authController.register(userData).pipe(
      tap(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    return !!this.currentUserSubject.value;
  }

  isAdmin(): boolean {
    return this.currentUserSubject.value?.role === 'Admin';
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  // Helper method to create a new user (for demo purposes)
  createUser(name: string, role: UserRole, email: string, displayName: string): User {
    const newUser: User = {
      id: Math.random().toString(36).substr(2, 9),
      email,
      displayName,
      avatar: `https://via.placeholder.com/150?text=${displayName.charAt(0)}`,
      role,
      createdAt: new Date(),
      lastLoginAt: new Date()
    };

    localStorage.setItem('currentUser', JSON.stringify(newUser));
    this.currentUserSubject.next(newUser);
    return newUser;
  }

  // Method to login as admin (for testing purposes)
  loginAsAdmin(): Observable<User> {
    const adminUser = this.authController.getMockAdminUser();
    return new Observable<User>(observer => {
      observer.next(adminUser);
      observer.complete();
    }).pipe(
      tap(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
      })
    );
  }
} 