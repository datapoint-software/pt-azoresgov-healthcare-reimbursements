import { Routes } from '@angular/router';
import { ErrorComponent } from './containers/error/error.component';
import { SignInComponent } from './containers/sign-in/sign-in.component';
import { canActivateSignIn, canDeactivateSignIn } from './containers/sign-in/sign-in.guards';
import { canActivateError, canDeactivateError } from './containers/error/error.guard';
import { canActivateApp } from './app.guards';
import { LayoutComponent } from './containers/layout/layout.component';
import { canActivateProcessCreation } from './containers/process-creation/process-creation.guards';
import { canActivateLayout, canDeactivateLayout } from './containers/layout/layout.guards';
import { authorize, canActivateSequence } from './app.helpers';
import { ProcessSearchComponent } from './containers/process-search/process-search.component';
import { ProcessCreationComponent } from './containers/process-creation/process-creation.component';

export const routes: Routes = [

  {
    path: '',
    pathMatch: 'prefix',
    canActivate: [ canActivateApp ],
    children: [
      {
        path: 'sign-in',
        component: SignInComponent,
        canActivate: [ canActivateSignIn ],
        canDeactivate: [ canDeactivateSignIn ]
      },
      {
        path: '',
        pathMatch: 'prefix',
        component: LayoutComponent,
        canActivate: [
          canActivateSequence([
            authorize([]),
            canActivateLayout
          ])
        ],
        canDeactivate: [ canDeactivateLayout ],
        children: [
          {
            path: 'processes',
            children: [
              {
                path: '',
                pathMatch: 'full',
                component: ProcessSearchComponent,
                canActivate: [
                  authorize([ 'process-search' ])
                ],
              },
              {
                path: '_',
                component: ProcessCreationComponent,
                canActivate: [
                  canActivateSequence([
                    authorize([ 'process-creation' ]),
                    canActivateProcessCreation
                  ])
                ]
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: 'error',
    component: ErrorComponent,
    canActivate: [ canActivateError ],
    canDeactivate: [ canDeactivateError ]
  },
  {
    path: '**',
    redirectTo: '/error?message=RXN0ZSUyMGVuZGVyZSVDMyVBN28lMjBuJUMzJUEzbyUyMGNvcnJlc3BvbmRlJTIwYSUyMHVtYSUyMHAlQzMlQTFnaW5hJTIwb3UlMjByZWN1cnNvJTIwZXhpc3RlbnRlLg=='
  }
];
