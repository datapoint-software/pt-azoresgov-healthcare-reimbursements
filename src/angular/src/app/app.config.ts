import { ApplicationConfig, isDevMode } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideEffects } from '@ngrx/effects';
import { provideEnvironmentClient } from './clients/environment/environment.client';
import { provideEnvironmentFeature } from './features/environment/environment.feature';
import { provideErrorFeature } from './features/error/error.feature';
import { provideErrorHandler } from './handlers/error.handler';
import { provideHttpClient } from '@angular/common/http';
import { provideIdentityClient } from './clients/identity/identity.client';
import { provideIdentityFeature } from './features/identity/identity.feature';
import { provideLoadingOverlayFeature } from './features/loading-overlay/loading-overlay.feature';
import { provideProcessCaptureClient } from './clients/process-capture/process-capture.client';
import { provideProcessCaptureFeature } from './features/process-capture/process-capture.feature';
import { provideProcessCreationClient } from './clients/process-creation/process-creation.client';
import { provideProcessCreationFeature } from './features/process-creation/process-creation.feature';
import { provideProcessSearchClient } from './clients/process-search/process-search.client';
import { provideProcessSearchFeature } from './features/process-search/process-search.feature';
import { provideRouter } from '@angular/router';
import { provideSignInClient } from './clients/sign-in/sign-in.client';
import { provideSignInFeature } from './features/sign-in/sign-in.feature';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { routes } from './app.routes';

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
    provideProcessSearchClient(),
    provideSignInClient(),

    // Features
    provideProcessCaptureFeature(),
    provideProcessSearchFeature(),
    provideEnvironmentFeature(),
    provideErrorFeature(),
    provideIdentityFeature(),
    provideLoadingOverlayFeature(),
    provideProcessCreationFeature(),
    provideSignInFeature()
  ]
};
