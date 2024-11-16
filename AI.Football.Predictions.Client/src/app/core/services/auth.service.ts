import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { UserCredentials } from '../models/user-credentials.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);

  loginUser(userCredentials: UserCredentials) {
    return this.http.post(`${environment.apiUrl}/authentication/login`, userCredentials);
  }
}
