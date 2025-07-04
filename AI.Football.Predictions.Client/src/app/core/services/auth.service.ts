import { HttpClient } from '@angular/common/http';
import { afterNextRender, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Client, StringServiceResponse, UserLoginDto, UserRegisterDto } from '../api-client/api-client';
import { BrowserStorageService } from './browser-storage.service';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { User } from '../models/user';

const USER_KEY = 'auth-user';
const TOKEN_KEY = 'auth-token';
const REFRESH_TOKEN_KEY = 'refresh-token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = new Client(inject(HttpClient), environment.apiUrl);
  private storageService: BrowserStorageService = inject(BrowserStorageService);

  userData = signal<User | null>(null);

  constructor() {
    afterNextRender(() => {
      this.setUserData(this.getUserIdentity());
    })
  }

  public loginUser(userCredentials: UserLoginDto): Observable<StringServiceResponse> {
    return this.http.login(userCredentials).pipe(
      tap((response) => {
        if(response.success) {
          this.storageService.set(TOKEN_KEY, response.data);
          this.setUserData(this.getUserIdentity());
        }
      })
    )
  }

  public registerUser(userData: UserRegisterDto) {
    return this.http.register(userData);
  }

  public getUserData(): User | null {
    // return this.storageService.get(USER_KEY) || null;
    return this.userData();
  }

  public setUserData(user: User): void {
    this.storageService.set(USER_KEY, user);
    this.userData.set(user);
  }

  public getToken(): any {
    return this.storageService.get(TOKEN_KEY);
  }

  public getRefreshToken(): any {
    return this.storageService.get(REFRESH_TOKEN_KEY);
  }

  public getUserIdentity(): User {
    const decodedToken = this.decodeToken();
    return {
      id: parseInt(decodedToken['nameid']),
      firstName: decodedToken['firstName'],
      lastName: decodedToken['lastName'],
      emailAddress: decodedToken['email']
    } as User;
  }

  public clearUserData(): void {
    this.storageService.remove(USER_KEY);
    this.storageService.remove(TOKEN_KEY);
    this.storageService.remove(REFRESH_TOKEN_KEY);
    this.userData.set(null);
  }

  private decodeToken(): any {
    return jwtDecode(this.getToken());
  }

  // loginUser(userCredentials: UserCredentials) {
  //   return this.http.post(`${environment.apiUrl}/authentication/login`, userCredentials);
  // }
}
