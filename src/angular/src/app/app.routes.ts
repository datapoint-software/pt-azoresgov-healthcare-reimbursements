import { Routes } from '@angular/router';
import { GenericSignInComponent } from '@app/components/generic-sign-in/generic-sign-in.component';
import { MainHomeComponent } from '@app/components/main-home/main-home.component';
import { MainProcessCreationConfirmationComponent } from '@app/components/main-process-creation-confirmation/main-process-creation-confirmation.component';
import { MainProcessCreationEntitySelectionComponent } from '@app/components/main-process-creation-entity-selection/main-process-creation-entity-selection.component';
import { MainProcessCreationPatientSelectionComponent } from '@app/components/main-process-creation-patient-selection/main-process-creation-patient-selection.component';
import { MainProcessCreationComponent } from '@app/components/main-process-creation/main-process-creation.component';
import { MainProcessSearchComponent } from '@app/components/main-process-search/main-process-search.component';
import { MainComponent } from '@app/components/main/main.component';
import { GenericSignInGuard } from '@app/guards/generic-sign-in/generic-sign-in.guard';
import { MainProcessCreationConfirmationGuard } from '@app/guards/main-process-creation-confirmation/main-process-creation-confirmation.guard';
import { MainProcessCreationEntitySelectionGuard } from '@app/guards/main-process-creation-entity-selection/main-process-creation-entity-selection.guard';
import { MainProcessCreationPatientSelectionGuard } from '@app/guards/main-process-creation-patient-selection/main-process-creation-patient-selection.guard';
import { MainProcessCreationGuard } from '@app/guards/main-process-creation/main-process-creation.guard';
import { MainGuard } from '@app/guards/main/main.guard';

export const routes: Routes = [
  {
    canActivate: [ GenericSignInGuard.canActivate ],
    component: GenericSignInComponent,
    path: 'sign-in',
    providers: GenericSignInGuard.providers
  },
  {
    canActivate: [ MainGuard.canActivate ],
    component: MainComponent,
    path: '',
    pathMatch: 'prefix',
    children: [
      {
        component: MainHomeComponent,
        path: '',
        pathMatch: 'full'
      },
      {
        path: 'processes',
        children: [
          {
            path: '',
            pathMatch: 'full',
            component: MainProcessSearchComponent
          },
          {
            canActivate: [ MainProcessCreationGuard.canActivate ],
            component: MainProcessCreationComponent,
            path: '_',
            providers: MainProcessCreationGuard.providers,
            children: [
              {
                path: '',
                pathMatch: 'full',
                redirectTo: 'confirmation'
              },
              {
                path: 'entity-selection',
                component: MainProcessCreationEntitySelectionComponent,
                canActivate: [ MainProcessCreationEntitySelectionGuard.canActivate ]
              },
              {
                path: 'patient-selection',
                component: MainProcessCreationPatientSelectionComponent,
                canActivate: [ MainProcessCreationPatientSelectionGuard.canActivate ]
              },
              {
                path: 'confirmation',
                component: MainProcessCreationConfirmationComponent,
                canActivate: [ MainProcessCreationConfirmationGuard.canActivate ]
              }
            ]
          }
        ]
      }
    ]
  }
];
