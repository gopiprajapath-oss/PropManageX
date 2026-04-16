import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../../environments/environment';
import { Observable } from 'rxjs';

export interface UserDto {
  userID: number;
  name: string;
  role: string;
  email: string;
  phone: string;
}

export interface CreateUserDto {
  name: string;
  role: string;
  email: string;
  phone: string;
  password?: string; 
}

@Injectable({
  providedIn: 'root'
})
export class AdminUserService {
  private http = inject(HttpClient);
  
  private apiUrl = `${environment.apiUrl}/Auth/users`;
  private registerUrl = `${environment.apiUrl}/Auth/register`;

  getAllUsers(): Observable<UserDto[]> {
    return this.http.get<UserDto[]>(this.apiUrl);
  }

  addUser(userData: CreateUserDto): Observable<string> {
    return this.http.post(this.registerUrl, userData, { responseType: 'text' });
  }

  deleteUser(userId: number): Observable<string> {
    return this.http.delete(`${this.apiUrl}/${userId}`, { responseType: 'text' });
  }
}