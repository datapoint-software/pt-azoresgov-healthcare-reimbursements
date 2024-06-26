import { Routes } from "@angular/router";
import { GenericErrorComponent } from "@app/components/generic-error/generic-error.component";
import { GenericSignInComponent } from "@app/components/generic-sign-in/generic-sign-in.component";
import { MainHomeComponent } from "@app/components/main-home/main-home.component";
import { MainProcessCaptureConfirmationComponent } from "@app/components/main-process-capture-confirmation/main-process-capture-confirmation.component";
import { MainProcessCaptureFamilyIncomeStatementComponent } from "@app/components/main-process-capture-family-income-statement/main-process-capture-family-income-statement.component";
import { MainProcessCaptureLegalRepresentativeComponent } from "@app/components/main-process-capture-legal-representative/main-process-capture-legal-representative.component";
import { MainProcessCapturePatientComponent } from "@app/components/main-process-capture-patient/main-process-capture-patient.component";
import { MainProcessCapturePaymentComponent } from "@app/components/main-process-capture-payment/main-process-capture-payment.component";
import { MainProcessCaptureUnemploymentStatementComponent } from "@app/components/main-process-capture-unemployment-statement/main-process-capture-unemployment-statement.component";
import { MainProcessCaptureComponent } from "@app/components/main-process-capture/main-process-capture.component";
import { MainProcessCreationConfirmationComponent } from "@app/components/main-process-creation-confirmation/main-process-creation-confirmation.component";
import { MainProcessCreationEntitySelectionComponent } from "@app/components/main-process-creation-entity-selection/main-process-creation-entity-selection.component";
import { MainProcessCreationPatientSelectionComponent } from "@app/components/main-process-creation-patient-selection/main-process-creation-patient-selection.component";
import { MainProcessCreationComponent } from "@app/components/main-process-creation/main-process-creation.component";
import { MainProcessSearchComponent } from "@app/components/main-process-search/main-process-search.component";
import { MainComponent } from "@app/components/main/main.component";
import { GenericErrorGuard } from "@app/guards/generic-error/generic-error.guard";
import { GenericSignInGuard } from "@app/guards/generic-sign-in/generic-sign-in.guard";
import { MainProcessCaptureConfirmationGuard } from "@app/guards/main-process-capture-confirmation/main-process-capture-confirmation.guard";
import { MainProcessCaptureFamilyIncomeStatementGuard } from "@app/guards/main-process-capture-family-income-statement/main-process-capture-family-income-statement.guard";
import { MainProcessCaptureLegalRepresentativeGuard } from "@app/guards/main-process-capture-legal-representative/main-process-capture-legal-representative.guard";
import { MainProcessCapturePatientGuard } from "@app/guards/main-process-capture-patient/main-process-capture-patient.guard";
import { MainProcessCapturePaymentGuard } from "@app/guards/main-process-capture-payment/main-process-capture-payment.guard";
import { MainProcessCaptureUnemploymentStatementGuard } from "@app/guards/main-process-capture-unemployment-statement/main-process-capture-unemployment-statement.guard";
import { MainProcessCaptureGuard } from "@app/guards/main-process-capture/main-process-capture.guard";
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
          },
          {
            path: ":processId",
            children: [
              {
                path: "capture",
                component: MainProcessCaptureComponent,
                canActivate: [ MainProcessCaptureGuard.canActivate ],
                providers: MainProcessCaptureGuard.providers,
                children: [
                  {
                    path: "",
                    pathMatch: "full",
                    redirectTo: "patient"
                  },
                  {
                    path: "confirmation",
                    component: MainProcessCaptureConfirmationComponent,
                    canActivate: [ MainProcessCaptureConfirmationGuard.canActivate ]
                  },
                  {
                    path: "family-income-statement",
                    component: MainProcessCaptureFamilyIncomeStatementComponent,
                    canActivate: [ MainProcessCaptureFamilyIncomeStatementGuard.canActivate ]
                  },
                  {
                    path: "legal-representative",
                    component: MainProcessCaptureLegalRepresentativeComponent,
                    canActivate: [ MainProcessCaptureLegalRepresentativeGuard.canActivate ],
                    canDeactivate: [ MainProcessCaptureLegalRepresentativeGuard.canDeactivate ]
                  },
                  {
                    path: "patient",
                    component: MainProcessCapturePatientComponent,
                    canActivate: [ MainProcessCapturePatientGuard.canActivate ],
                    canDeactivate: [ MainProcessCapturePatientGuard.canDeactivate ]
                  },
                  {
                    path: "payment",
                    component: MainProcessCapturePaymentComponent,
                    canActivate: [ MainProcessCapturePaymentGuard.canActivate ]
                  },
                  {
                    path: "unemployment-statement",
                    component: MainProcessCaptureUnemploymentStatementComponent,
                    canActivate: [ MainProcessCaptureUnemploymentStatementGuard.canActivate ]
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
