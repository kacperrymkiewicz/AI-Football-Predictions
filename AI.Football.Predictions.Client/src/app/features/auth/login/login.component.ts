import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { FormsModule } from '@angular/forms';
import { BrowserStorageService } from '../../../core/services/browser-storage.service';
import { UserLoginDto } from '../../../core/api-client/api-client';
import { Router, RouterLink } from '@angular/router';
import { finalize } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private authService: AuthService = inject(AuthService);
  private toastr: ToastrService = inject(ToastrService);
  private router: Router = inject(Router);

  error = signal<string | undefined>(undefined);
  isFetching = signal<boolean>(false);

  enteredEmail?: string;
  enteredPassword?: string;

  onLoginSubmit() {
    this.isFetching.set(true);
    this.authService.loginUser({
      emailAddress: this.enteredEmail!,
      password: this.enteredPassword!,
    } as UserLoginDto)
    .pipe(
      finalize(() => {
        this.isFetching.set(false);
      })
    )
    .subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error: any) => {
        if(error.status == 400) {
          this.error.set('Nieprawidłowy login lub hasło');
        }
        else {
          this.error.set('Wystąpił nieznany błąd.');
          console.log(error.message);
        }
      },
      complete: () => {
        this.toastr.success('Zalogowano pomyślnie!');
        this.router.navigate(['/']);
      },
    })
  }
}
