import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { IdentityFeature } from './features/identity/identity.feature';

export const appConfig: ApplicationConfig = {
  providers: [

    // Angular
    provideHttpClient(),
    provideRouter(routes),

    // Core features
    IdentityFeature

  ]
};
