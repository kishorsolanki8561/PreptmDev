import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { config } from './app/app.config.server';

// Angular 19 SSR: renderApplication() passes { platformRef } as the first argument
// to the bootstrap function. We must forward it as the third parameter of
// bootstrapApplication() so that internalCreateApplication receives a non-null
// platformRef and does not throw NG0401.
const bootstrap = (context?: { platformRef?: unknown }) =>
  bootstrapApplication(AppComponent, config, context as any);

export default bootstrap;
