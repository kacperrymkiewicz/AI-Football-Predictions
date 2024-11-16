import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UserCredentials } from '../models/user-credentials.model';
import { Client, UserLoginDto } from '../api-client/api-client';

const TOKEN_KEY = 'auth-token';
const REFRESH_TOKEN_KEY = 'refresh-token';
const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private client = new Client(this.http, "http://localhost:5000");

  loginUser(userCredentials: UserLoginDto) {
    return this.client.login(userCredentials);
  }

  loginUser2(userCredentials: UserCredentials) {
    return this.http.post(`${environment.apiUrl}/authentication/login`, userCredentials);
  }
}
