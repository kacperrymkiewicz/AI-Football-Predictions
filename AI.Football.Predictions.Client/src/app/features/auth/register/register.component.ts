import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../../core/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { UserRegisterDto } from '../../../core/api-client/api-client';
import { finalize } from 'rxjs';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  private authService: AuthService = inject(AuthService);
  private toastr: ToastrService = inject(ToastrService);
  private router: Router = inject(Router);

  error = signal<string | undefined>(undefined);
  isFetching = signal<boolean>(false);

  enteredEmail?: string;
  enteredFirstName?: string;
  enteredLastName?: string;
  enteredPassword?: string;

  onRegisterSubmit() {
    this.isFetching.set(true);
    this.authService.registerUser({
      emailAddress: this.enteredEmail!,
      firstName: this.enteredFirstName!,
      lastName: this.enteredLastName!,
      password: this.enteredPassword!,
    } as UserRegisterDto)
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
          this.error.set('Wystąpiły błędy w formularzu.');
        }
        else {
          this.error.set('Wystąpił nieznany błąd.');
          console.log(error.message);
        }
      },
      complete: () => {
        this.toastr.success('Konto zostało utworzone pomyślnie!');
        this.router.navigate(['/auth/login']);
      },
    })
  }
}
