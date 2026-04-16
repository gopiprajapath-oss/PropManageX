import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter, withComponentInputBinding } from '@angular/router';

import { routes } from './app.routes';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/interceptors/auth-interceptor';

registerLocaleData(en);

export const appConfig: ApplicationConfig = {
  providers:
   [provideBrowserGlobalErrorListeners(), provideRouter(routes,withComponentInputBinding()), provideHttpClient(withInterceptors([authInterceptor]))]
};
