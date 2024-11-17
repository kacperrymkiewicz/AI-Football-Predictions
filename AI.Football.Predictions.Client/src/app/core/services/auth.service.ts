import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Client, StringServiceResponse, UserLoginDto } from '../api-client/api-client';
import { BrowserStorageService } from './browser-storage.service';
import { Observable, tap } from 'rxjs';

const USER_KEY = 'auth-user';
const TOKEN_KEY = 'auth-token';
const REFRESH_TOKEN_KEY = 'refresh-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = new Client(inject(HttpClient), environment.apiUrl);
  private storageService: BrowserStorageService = inject(BrowserStorageService);

  public loginUser(userCredentials: UserLoginDto): Observable<StringServiceResponse> {
    return this.http.login(userCredentials).pipe(
      tap((response) => {
        if(response.success) {
          this.storageService.set(TOKEN_KEY, response.data);
        }
      })
    )
  }

  // loginUser(userCredentials: UserCredentials) {
  //   return this.http.post(`${environment.apiUrl}/authentication/login`, userCredentials);
  // }
}
