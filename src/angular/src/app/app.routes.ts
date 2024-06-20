import { Routes } from "@angular/router";
import { GenericErrorComponent } from "@app/components/generic-error/generic-error.component";
import { GenericSignInComponent } from "@app/components/generic-sign-in/generic-sign-in.component";
import { MainHomeComponent } from "@app/components/main-home/main-home.component";
import { MainProcessCreationConfirmationComponent } from "@app/components/main-process-creation-confirmation/main-process-creation-confirmation.component";
import { MainProcessCreationEntitySelectionComponent } from "@app/components/main-process-creation-entity-selection/main-process-creation-entity-selection.component";
import { MainProcessCreationPatientSelectionComponent } from "@app/components/main-process-creation-patient-selection/main-process-creation-patient-selection.component";
import { MainProcessCreationComponent } from "@app/components/main-process-creation/main-process-creation.component";
import { MainProcessSearchComponent } from "@app/components/main-process-search/main-process-search.component";
import { MainComponent } from "@app/components/main/main.component";
import { GenericErrorGuard } from "@app/guards/generic-error/generic-error.guard";
import { GenericSignInGuard } from "@app/guards/generic-sign-in/generic-sign-in.guard";
import { MainProcessCreationConfirmationGuard } from "@app/guards/main-process-creation-confirmation/main-process-creation-confirmation.guard";
import { MainProcessCreationEntitySelectionGuard } from "@app/guards/main-process-creation-entity-selection/main-process-creation-entity-selection.guard";
import { MainProcessCreationPatientSelectionGuard } from "@app/guards/main-process-creation-patient-selection/main-process-creation-patient-selection.guard";
import { MainProcessCreationGuard } from "@app/guards/main-process-creation/main-process-creation.guard";
import { MainProcessSearchGuard } from "@app/guards/main-process-search/main-process-search.guard";
import { MainGuard } from "@app/guards/main/main.guard";

export const routes: Routes = [
  {
    path: "error",
    component: GenericErrorComponent,
    canActivate: [ GenericErrorGuard.canActivate ],
  },
  {
    path: "sign-in",
    component: GenericSignInComponent,
    canActivate: [ GenericSignInGuard.canActivate ],
    providers: GenericSignInGuard.providers
  },
  {
    path: "",
    pathMatch: "prefix",
    canActivate: [ MainGuard.canActivate ],
    component: MainComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        component: MainHomeComponent
      },
      {
        path: "processes",
        children: [
          {
            path: "",
            pathMatch: "full",
            component: MainProcessSearchComponent,
            canActivate: [ MainProcessSearchGuard.canActivate ],
            providers: MainProcessSearchGuard.providers
          },
          {
            path: "_",
            component: MainProcessCreationComponent,
            canActivate: [ MainProcessCreationGuard.canActivate ],
            providers: MainProcessCreationGuard.providers,
            children: [
              {
                path: "",
                pathMatch: "full",
                redirectTo: "confirmation"
              },
              {
                path: "entity-selection",
                component: MainProcessCreationEntitySelectionComponent,
                canActivate: [ MainProcessCreationEntitySelectionGuard.canActivate ]
              },
              {
                path: "patient-selection",
                component: MainProcessCreationPatientSelectionComponent,
                canActivate: [ MainProcessCreationPatientSelectionGuard.canActivate ]
              },
              {
                path: "confirmation",
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
