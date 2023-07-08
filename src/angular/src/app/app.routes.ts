import { Routes } from '@angular/router';
import { ErrorComponent } from './containers/error/error.component';
import { SignInComponent } from './containers/sign-in/sign-in.component';
import { canActivateSignIn, canDeactivateSignIn } from './containers/sign-in/sign-in.guards';
import { canActivateError } from './containers/error/error.guard';

export const routes: Routes = [
  {
    path: 'sign-in',
    component: SignInComponent,
    canActivate: [ canActivateSignIn ],
    canDeactivate: [ canDeactivateSignIn ]
  },
  {
    path: 'error',
    component: ErrorComponent,
    canActivate: [ canActivateError ]
  },
  {
    path: '**',
    redirectTo: '/error?message=RXN0ZSUyMGVuZGVyZSVDMyVBN28lMjBuJUMzJUEzbyUyMGNvcnJlc3BvbmRlJTIwYSUyMHVtYSUyMHAlQzMlQTFnaW5hJTIwb3UlMjByZWN1cnNvJTIwZXhpc3RlbnRlLg=='
  }
];
