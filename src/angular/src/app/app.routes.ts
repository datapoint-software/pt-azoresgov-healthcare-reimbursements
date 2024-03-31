import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignInGuard } from './guards/sign-in/sign-in.guard';

export const routes: Routes = [
  {
    canActivate: [ SignInGuard.canActivate ],
    component: SignInComponent,
    path: 'sign-in',
    providers: SignInGuard.providers
  }
];
