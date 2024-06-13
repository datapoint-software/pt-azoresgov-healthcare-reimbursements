import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { CoreIdentityFeatureClient } from '@app/api/core-identity-feature/core-identity-feature.client';
import { CoreIdentityFeature } from '@app/features/core-identity/core-identity.feature';
import { CoreLoadingOverlayFeature } from '@app/features/core-loading-overlay/core-loading-overlay.feature';
import { routes } from '@app/routes';

export const appConfig: ApplicationConfig = {
  providers: [

    // Angular
    provideAnimations(),
    provideHttpClient(),
    provideRouter(routes),

    // Core features
    CoreIdentityFeature,
    CoreIdentityFeatureClient,
    CoreLoadingOverlayFeature
  ]
};
