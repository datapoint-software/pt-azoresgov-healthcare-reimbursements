import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { ProcessCreationConfirmationComponent } from './components/process-creation-confirmation/process-creation-confirmation.component';
import { ProcessCreationEntitySelectionComponent } from './components/process-creation-entity-selection/process-creation-entity-selection.component';
import { ProcessCreationPatientComponent } from './components/process-creation-patient/process-creation-patient.component';
import { ProcessCreationComponent } from './components/process-creation/process-creation.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { LayoutGuard } from './guards/layout/layout.guard';
import { ProcessCreationEntitySelectionGuard } from './guards/process-creation-entity-selection/process-creation-entity-selection.guard';
import { ProcessCreationGuard } from './guards/process-creation/process-creation.guard';
import { SignInGuard } from './guards/sign-in/sign-in.guard';
import { ProcessCreationPatientSelectionGuard } from './guards/process-creation-patient-selection/process-creation-patient-selection.guard';
import { ProcessCreationConfirmationGuard } from './guards/process-creation-confirmation/process-creation-confirmation.guard';

export const routes: Routes = [
  {
    canActivate: [ SignInGuard.canActivate ],
    component: SignInComponent,
    path: 'sign-in',
    providers: SignInGuard.providers
  },
  {
    canActivate: [ LayoutGuard.canActivate ],
    component: LayoutComponent,
    path: '',
    pathMatch: 'prefix',
    children: [
      {
        component: HomeComponent,
        path: '',
        pathMatch: 'full'
      },
      {
        path: 'processes',
        children: [
          {
            canActivate: [ ProcessCreationGuard.canActivate ],
            component: ProcessCreationComponent,
            path: '_',
            providers: ProcessCreationGuard.providers,
            children: [
              {
                path: '',
                pathMatch: 'full',
                redirectTo: 'entity'
              },
              {
                canActivate: [ ProcessCreationConfirmationGuard.canActivate ],
                path: 'confirmation',
                component: ProcessCreationConfirmationComponent
              },
              {
                canActivate: [ ProcessCreationEntitySelectionGuard.canActivate ],
                component: ProcessCreationEntitySelectionComponent,
                path: 'entity'
              },
              {
                canActivate: [ ProcessCreationPatientSelectionGuard.canActivate ],
                component: ProcessCreationPatientComponent,
                path: 'patient'
              }
            ]
          }
        ]
      }
    ]
  }
];
