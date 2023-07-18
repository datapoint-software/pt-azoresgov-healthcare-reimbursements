import { ApplicationConfig, importProvidersFrom, isDevMode } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideEffects } from '@ngrx/effects';
import { provideErrorFeature } from './features/error/error.provider';
import { provideErrorHandler } from './app.providers';
import { provideHttpClient } from '@angular/common/http';
import { provideIdentityFeature } from './features/identity/identity.provider';
import { provideLoadingOverlayFeature } from './features/loading-overlay/loading-overlay.provider';
import { provideRouter } from '@angular/router';
import { provideSignInFeature } from './features/sign-in/sign-in.provider';
import { provideStore } from '@ngrx/store';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [

    // Core
    provideErrorHandler(),
    provideHttpClient(),
    provideRouter(routes),

    importProvidersFrom([
      BrowserAnimationsModule
    ]),

    // App
    provideEffects(),
    provideStore(),
    provideStoreDevtools({
      maxAge: 25,
      logOnly: !isDevMode(),
      name: 'AzoresGov.Healthcare.Reimbursements'
    }),

    // App features
    provideErrorFeature(),
    provideIdentityFeature(),
    provideLoadingOverlayFeature(),
    provideSignInFeature()
  ]
};
