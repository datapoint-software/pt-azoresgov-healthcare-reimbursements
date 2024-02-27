import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { canActivateApp, canDeactivateApp } from './guards/app/app.guards';
import { ErrorComponent } from './components/error/error.component';
import { canActivateError, canDeactivateError } from './guards/error/error.guards';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { canActivateSignIn, canDeactivateSignIn } from './guards/sign-in/sign-in.guards';
import { authorize } from './guards/identity/identity.guards';

export const routes: Routes = [
  {
    path: 'error',
    component: ErrorComponent,
    canActivate: [ canActivateError ],
    canDeactivate: [ canDeactivateError ]
  },
  {
    path: '',
    pathMatch: 'prefix',
    canActivate: [ canActivateApp ],
    canDeactivate: [ canDeactivateApp ],
    children: [
      {
        path: 'sign-in',
        component: SignInComponent,
        canActivate: [ canActivateSignIn ],
        canDeactivate: [ canDeactivateSignIn ],
      },
      {
        path: '',
        pathMatch: 'prefix',
        canActivate: [ authorize() ],
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: HomeComponent
          }
        ]
      }
    ]
  }
];
