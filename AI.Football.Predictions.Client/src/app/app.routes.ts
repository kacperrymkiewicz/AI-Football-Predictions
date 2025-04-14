import { Routes } from '@angular/router';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { MatchDetailsComponent } from './features/match-details/match-details.component';
import { AccountComponent } from './features/account/account.component';

export const routes: Routes = [
  { path: '', component: DashboardComponent },
  {
    path: 'auth',
    children: [
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'register', component: RegisterComponent }
    ],
  },
  {
    path: 'account',
    children: [
      { path: '', redirectTo: 'profile', pathMatch: 'full' },
      { path: 'profile', component: AccountComponent },
    ],
  },
  { path: 'match/:id/:slug', component: MatchDetailsComponent },
];
