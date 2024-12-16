import { Component, effect, inject, signal } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../../../core/services/auth.service';
import { User } from '../../../core/models/user';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  private authService: AuthService = inject(AuthService);
  private toastr: ToastrService = inject(ToastrService);
  private router: Router = inject(Router);
  user?: User | null;
  role?: string | null;
  isDropdownMenuOpen = signal(false);

  constructor() {
    effect(() => {
      this.user = this.authService.getUserData();
    });
  }

  onLogout() {
    this.authService.clearUserData();
    this.user = null;
    this.router.navigate(['/auth/login'])
    this.toastr.success('Wylogowano pomyślnie!');
  }

  toggleDropdownMenu() {
    this.isDropdownMenuOpen.set(!this.isDropdownMenuOpen());
  }
}