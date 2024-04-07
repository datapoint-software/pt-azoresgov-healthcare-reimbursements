import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignInGuard } from './guards/sign-in/sign-in.guard';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';
import { LayoutGuard } from './guards/layout/layout.guard';
import { ProcessCreationComponent } from './components/process-creation/process-creation.component';
import { ProcessCreationPatientComponent } from './components/process-creation-patient/process-creation-patient.component';
import { ProcessCreationEntityComponent } from './components/process-creation-entity/process-creation-entity.component';
import { ProcessCreationConfirmationComponent } from './components/process-creation-confirmation/process-creation-confirmation.component';
import { ProcessCreationGuard } from './guards/process-creation/process-creation.guard';

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
                path: 'confirmation',
                component: ProcessCreationConfirmationComponent
              },
              {
                component: ProcessCreationEntityComponent,
                path: 'entity'
              },
              {
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
