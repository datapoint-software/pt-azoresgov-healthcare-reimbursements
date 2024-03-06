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
import { provideProcessCaptureClient } from './clients/process-capture/process-capture.client';
import { provideProcessCaptureFeature } from './features/process-capture/process-capture.feature';

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
    provideProcessCaptureClient(),
    provideProcessCreationClient(),
    provideSignInClient(),

    // Features
    provideProcessCaptureFeature(),
    provideEnvironmentFeature(),
    provideErrorFeature(),
    provideIdentityFeature(),
    provideLoadingOverlayFeature(),
    provideProcessCreationFeature(),
    provideSignInFeature()
  ]
};
