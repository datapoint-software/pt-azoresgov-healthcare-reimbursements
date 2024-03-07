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
import { ProcessCapturePatientComponent } from './components/process-capture-patient/process-capture-patient.component';
import { ProcessCaptureComponent } from './components/process-capture/process-capture.component';
import { canActivateProcessCapture, canDeactivateProcessCapture } from './guards/process-capture/process-capture.guards';
import { ProcessCaptureLegalRepresentativeComponent } from './components/process-capture-legal-representative/process-capture-legal-representative.component';
import { ProcessCaptureSpecialTermsComponent } from './components/process-capture-special-terms/process-capture-special-terms.component';
import { canActivateProcessSearch } from './guards/process-search/process-search.guards';

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
                component: ProcessSearchComponent,
                canActivate: [ canActivateProcessSearch ]
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
                    path: 'capture',
                    component: ProcessCaptureComponent,
                    canActivate: [ canActivateProcessCapture ],
                    canDeactivate: [ canDeactivateProcessCapture ],
                    children: [
                      {
                        path: 'patient',
                        component: ProcessCapturePatientComponent
                      },
                      {
                        path: 'special-terms',
                        component: ProcessCaptureSpecialTermsComponent
                      },
                      {
                        path: 'legal-representative',
                        component: ProcessCaptureLegalRepresentativeComponent
                      }
                    ]
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
