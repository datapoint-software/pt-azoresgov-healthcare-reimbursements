import { ApplicationConfig, ErrorHandler, isDevMode, makeEnvironmentProviders } from '@angular/core';
import { provideEffects } from '@ngrx/effects';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { provideSignInFeature } from './features/sign-in/sign-in.provider';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { routes } from './app.routes';
import { provideLoadingOverlayFeature } from './features/loading-overlay/loading-overlay.providers';
import { provideErrorHandler } from './app.providers';

export const appConfig: ApplicationConfig = {
  providers: [

    // Core
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
    provideLoadingOverlayFeature(),
    provideSignInFeature()
  ]
};
