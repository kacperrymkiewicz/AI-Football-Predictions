import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';
import { BrowserStorageService } from '../../../core/services/browser-storage.service';
import { UserLoginDto } from '../../../core/api-client/api-client';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private authService: AuthService = inject(AuthService);
  private storageService: BrowserStorageService = inject(BrowserStorageService);

  error = signal<string | undefined>(undefined);

  enteredEmail?: string;
  enteredPassword?: string;

  onLoginSubmit() {
    this.authService.loginUser({
      emailAddress: this.enteredEmail!,
      password: this.enteredPassword!,
    } as UserLoginDto).subscribe({
      next: (response) => {
        console.log(response);
        this.storageService.set('auth-token', response);
      },
      error: (error: Error) => {
        console.log(error.message);
      },
      complete: () => {
        // this.isFetching.set(false);
      },
    })
  }
}
