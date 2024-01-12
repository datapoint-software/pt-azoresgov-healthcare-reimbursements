import { provideHttpClient } from "@angular/common/http";
import { ApplicationConfig, importProvidersFrom, isDevMode } from "@angular/core";
import { provideAnimations } from "@angular/platform-browser/animations";
import { provideRouter } from "@angular/router";
import { provideEffects } from "@ngrx/effects";
import { provideStore } from "@ngrx/store";
import { provideStoreDevtools } from "@ngrx/store-devtools";
import { provideErrorHandler } from "./app.providers";
import { routes } from "./app.routes";
import { provideErrorFeature } from "./features/error/error.provider";
import { provideIdentityFeature } from "./features/identity/identity.provider";
import { provideLoadingOverlayFeature } from "./features/loading-overlay/loading-overlay.provider";
import { provideSignInFeature } from "./features/sign-in/sign-in.provider";
import { provideProcessCreationFeature } from "./features/process-creation/process-creation.providers";
import { provideEnvironmentFeature } from "./features/environment/environment.provider";

export const appConfig: ApplicationConfig = {
  providers: [

    // Core
    provideAnimations(),
    provideErrorHandler(),
    provideHttpClient(),
    provideRouter(routes),

    // App
    provideEffects(),
    provideStore(),
    provideStoreDevtools({
      maxAge: 25,
      logOnly: !isDevMode(),
      name: 'AzoresGov.Healthcare.Reimbursements'
    }),

    // App features
    provideEnvironmentFeature(),
    provideErrorFeature(),
    provideIdentityFeature(),
    provideLoadingOverlayFeature(),
    provideProcessCreationFeature(),
    provideSignInFeature()
  ]
};
