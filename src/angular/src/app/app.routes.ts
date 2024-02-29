import { authorize } from './guards/identity/identity.guards';
import { canActivateApp, canDeactivateApp } from './guards/app/app.guards';
import { canActivateError, canDeactivateError } from './guards/error/error.guards';
import { canActivateProcessCreation, canDeactivateProcessCreation } from './guards/process-creation/process-creation.guards';
import { canActivateSignIn, canDeactivateSignIn } from './guards/sign-in/sign-in.guards';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { ProcessCreationComponent } from './components/process-creation/process-creation.component';
import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { LayoutComponent } from './components/layout/layout.component';
import { ProcessSearchComponent } from './components/process-search/process-search.component';

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
        component: LayoutComponent,
        canActivate: [ authorize() ],
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: HomeComponent
          },
          {
            path: 'processes',
            children: [
              {
                path: '',
                pathMatch: 'full',
                component: ProcessSearchComponent
              },
              {
                path: '_',
                component: ProcessCreationComponent,
                canActivate: [ canActivateProcessCreation ],
                canDeactivate: [ canDeactivateProcessCreation ]
              }
            ]
          }
        ]
      }
    ]
  }
];
