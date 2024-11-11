import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private authService: AuthService = inject(AuthService);

  error = signal<string | undefined>(undefined);

  enteredEmail?: string;
  enteredPassword?: string;

  onLoginSubmit() {
    this.authService.loginUser({
      emailAddress: this.enteredEmail!,
      password: this.enteredPassword!,
    }).subscribe({
      next: (response) => {
        console.log(response);
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
