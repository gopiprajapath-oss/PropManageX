import { Injectable, inject, signal, computed } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import { RegisterDto } from '../../feature/auth/models/register.dto';
import { TokenService } from './token.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private readonly baseUrl = `${environment.apiUrl}/auth`;
  private tokenService = inject(TokenService);

  // Signal state initialized from the storage service
  private _isAuthenticated = signal<boolean>(this.tokenService.hasToken());
  isAuthenticated = computed(() => this._isAuthenticated());


  
  register(data: RegisterDto) {
    return this.http.post<{ message: string }>(`${this.baseUrl}/register`, data);
  }

  login(data: any) {
    return this.http.post<{ token: string }>(`${this.baseUrl}/login`, data).pipe(
      tap(res => {
        if (res.token) {
          this.tokenService.saveToken(res.token);
          this._isAuthenticated.set(true);
        }
      })
    );
  }

  logout(): void {
    this.tokenService.removeToken();
    this._isAuthenticated.set(false);
    this.router.navigate(['/login']);
  }

  
}