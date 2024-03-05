import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideEffects } from '@ngrx/effects';
import { provideEnvironmentFeature } from './features/environment/environment.feature';
import { provideEnvironmentClient } from './clients/environment/environment.client';
import { provideHttpClient } from '@angular/common/http';
import { provideErrorFeature } from './features/error/error.feature';
import { provideSignInFeature } from './features/sign-in/sign-in.feature';
import { provideSignInClient } from './clients/sign-in/sign-in.client';
import { provideErrorHandler } from './handlers/error.handler';
import { provideLoadingOverlayFeature } from './features/loading-overlay/loading-overlay.feature';
import { provideIdentityFeature } from './features/identity/identity.feature';
import { provideIdentityClient } from './clients/identity/identity.client';
import { provideProcessCreationFeature } from './features/process-creation/process-creation.feature';
import { provideProcessCreationClient } from './clients/process-creation/process-creation.client';
import { provideProcessPatientCaptureFeature } from './features/process-patient-capture/process-patient-capture.feature';
import { provideProcessPatientCaptureClient } from './clients/process-patient-capture/process-patient-capture.client';

export const appConfig: ApplicationConfig = {
  providers: [

    // Core
    provideAnimations(),
    provideErrorHandler(),
    provideHttpClient(),
    provideRouter(routes),
    provideStore(),
    provideStoreDevtools({ maxAge: 25, logOnly: !isDevMode() }),
    provideEffects(),

    // Clients
    provideEnvironmentClient(),
    provideIdentityClient(),
    provideProcessPatientCaptureClient(),
    provideProcessCreationClient(),
    provideSignInClient(),

    // Features
    provideEnvironmentFeature(),
    provideErrorFeature(),
    provideIdentityFeature(),
    provideLoadingOverlayFeature(),
    provideProcessPatientCaptureFeature(),
    provideProcessCreationFeature(),
    provideSignInFeature()
  ]
};
