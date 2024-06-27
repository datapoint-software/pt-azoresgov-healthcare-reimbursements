import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideRouter } from '@angular/router';
import { CoreIdentityFeatureClient } from '@app/api/core-identity-feature/core-identity-feature.client';
import { CoreErrorFeature } from '@app/features/core-error/core-error.feature';
import { CoreIdentityFeature } from '@app/features/core-identity/core-identity.feature';
import { CoreLoadingOverlayFeature } from '@app/features/core-loading-overlay/core-loading-overlay.feature';
import { CoreTaskOverlayFeature } from '@app/features/core-task-overlay/core-task-overlay.feature';
import { AppErrorHandler } from '@app/handlers';
import { routes } from '@app/routes';

export const appConfig: ApplicationConfig = {
  providers: [

    // Angular
    provideAnimations(),
    provideHttpClient(),
    provideRouter(routes),

    // Handlers
    ({ provide: ErrorHandler, useClass: AppErrorHandler }),

    // Core features
    CoreErrorFeature,
    CoreIdentityFeature,
    CoreIdentityFeatureClient,
    CoreLoadingOverlayFeature,
    CoreTaskOverlayFeature
  ]
};
