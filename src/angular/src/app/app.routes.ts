import { Routes } from '@angular/router';
import { ErrorComponent } from './components/error/error.component';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { ProcessCaptureConfigurationComponent } from './components/process-capture-configuration/process-capture-configuration.component';
import { ProcessCaptureLegalRepresentativeComponent } from './components/process-capture-legal-representative/process-capture-legal-representative.component';
import { ProcessCapturePatientComponent } from './components/process-capture-patient/process-capture-patient.component';
import { ProcessCaptureComponent } from './components/process-capture/process-capture.component';
import { ProcessCreationComponent } from './components/process-creation/process-creation.component';
import { ProcessSearchComponent } from './components/process-search/process-search.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { canActivateApp } from './guards/app/app.guards';
import { canActivateError } from './guards/error/error.guards';
import { authorize } from './guards/identity/identity.guards';
import { canActivateProcessCapture, canDeactivateProcessCapture } from './guards/process-capture/process-capture.guards';
import { canActivateProcessCreation } from './guards/process-creation/process-creation.guards';
import { canActivateProcessSearch } from './guards/process-search/process-search.guards';
import { canActivateSignIn } from './guards/sign-in/sign-in.guards';
import { ProcessCaptureFamilyIncomeStatementComponent } from './components/process-capture-family-income-statement/process-capture-family-income-statement.component';
import { ProcessCapturePaymentComponent } from './components/process-capture-payment/process-capture-payment.component';
import { ProcessCaptureSimulationComponent } from './components/process-capture-simulation/process-capture-simulation.component';
import { ProcessCaptureDocumentsComponent } from './components/process-capture-documents/process-capture-documents.component';

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
                        path: 'configuration',
                        component: ProcessCaptureConfigurationComponent
                      },
                      {
                        path: 'documents',
                        component: ProcessCaptureDocumentsComponent
                      },
                      {
                        path: 'legal-representative',
                        component: ProcessCaptureLegalRepresentativeComponent
                      },
                      {
                        path: 'family-income-statement',
                        component: ProcessCaptureFamilyIncomeStatementComponent
                      },
                      {
                        path: 'payment',
                        component: ProcessCapturePaymentComponent
                      },
                      {
                        path: 'simulation',
                        component: ProcessCaptureSimulationComponent
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
