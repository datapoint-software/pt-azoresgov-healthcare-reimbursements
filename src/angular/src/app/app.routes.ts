import { authorize } from './guards/identity/identity.guards';
import { canActivateApp } from './guards/app/app.guards';
import { canActivateError } from './guards/error/error.guards';
import { canActivateProcessCreation } from './guards/process-creation/process-creation.guards';
import { canActivateSignIn } from './guards/sign-in/sign-in.guards';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { ProcessCreationComponent } from './components/process-creation/process-creation.component';
import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { LayoutComponent } from './components/layout/layout.component';
import { ProcessSearchComponent } from './components/process-search/process-search.component';
import { ProcessPatientCaptureComponent } from './components/process-patient-capture/process-patient-capture.component';
import { canActivateProcessPatientCapture } from './guards/process-patient-capture/process-patient-capture.guards';

export const routes: Routes = [
  {
    path: 'error',
    component: ErrorComponent,
    canActivate: [ canActivateError ]
  },
  {
    path: '',
    pathMatch: 'prefix',
    canActivate: [ canActivateApp ],
    children: [
      {
        path: 'sign-in',
        component: SignInComponent,
        canActivate: [ canActivateSignIn ],

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

              },
              {
                path: ':processId',
                children: [
                  {
                    path: 'patient-capture',
                    component: ProcessPatientCaptureComponent,
                    canActivate: [ canActivateProcessPatientCapture ],

                  }
                ]
              }
            ]
          }
        ]
      }
    ]
  }
];
