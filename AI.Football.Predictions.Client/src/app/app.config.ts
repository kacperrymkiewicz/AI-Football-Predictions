import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withInMemoryScrolling } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { BrowserStorageService } from './core/services/browser-storage.service';
import { provideToastr } from 'ngx-toastr';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(
      routes,
      withInMemoryScrolling({
        scrollPositionRestoration: "top",
      })
    ), 
    provideClientHydration(),
    provideHttpClient(
      withFetch(),
      // withInterceptors([httpInterceptor])
    ),
    provideAnimations(),
    provideToastr({
      timeOut: 4000,
      positionClass: 'toast-top-right',
      progressBar: true,
      preventDuplicates: true
    }), 
    BrowserStorageService
  ]
};
