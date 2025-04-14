import { Component, inject } from '@angular/core';
import { AuthService } from '../../core/services/auth.service';
import { User } from '../../core/models/user';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [],
  templateUrl: './account.component.html',
  styleUrl: './account.component.scss'
})
export class AccountComponent {
  private authService: AuthService = inject(AuthService);
  user?: User | null;
  role?: string | null;

  constructor() {
    this.user = this.authService.getUserData();
  }
}
