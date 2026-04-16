import { Injectable } from '@angular/core';
import { UserToken } from './token-model';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private readonly TOKEN_KEY = 'token';

  /**
   * Saves the JWT token to local storage
   */
  saveToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  /**
   * Retrieves the token for API requests
   */
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  /**
   * Removes the token upon logout
   */
  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.clear();
  }

  /**
   * Quick check if a token exists (does not validate expiration)
   */
  hasToken(): boolean {
    return !!this.getToken();
  }
  /**
   * Decodes the JWT and returns the payload object
   */
  decodeToken(): any | null {
    const token = this.getToken();
    if (!token) return null;

    try {
      const payloadBase64 = token.split('.')[1]; // middle part
      const payloadJson = atob(payloadBase64.replace(/-/g, '+').replace(/_/g, '/'));
      return JSON.parse(payloadJson);
    } catch (error) {
      console.error('Invalid token format', error);
      return null;
    }
  }

  /**
   * Retrieves the user object from the decoded token payload
   */



getUser(): UserToken | null {
  const token = this.getToken();
  if (!token) return null;

  try {
    const payload = JSON.parse(atob(token.split('.')[1]));

    return {
      id: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      name: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
      email: payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
      role: payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    };
  } catch (err) {
    console.error('Invalid token', err);
    return null;
  }
}


  getAuthHeader() {
    const token = this.getToken();
    return token ? { Authorization: `Bearer ${token}` } : {};
  }
}

