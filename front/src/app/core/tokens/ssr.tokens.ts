import { InjectionToken } from '@angular/core';

/** Injection token for the Express `Response` object on the server.
 *  Provided in server.ts; `null` in the browser. Always inject with `@Optional()`.
 */
export const SERVER_RESPONSE = new InjectionToken<any>('SERVER_RESPONSE');
