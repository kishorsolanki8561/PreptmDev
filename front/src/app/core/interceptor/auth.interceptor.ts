import { HttpErrorResponse, HttpEvent, HttpHandlerFn, HttpInterceptorFn, HttpRequest, HttpResponse } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { catchError, map, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { CoreService } from '../services/core.service';
import { EncryptionServiceService } from '../services/encryption-service.service';
import { environment } from 'src/environments/environment';

export const authInterceptorFn: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);
  const router = inject(Router);
  const coreService = inject(CoreService);
  const encrypt = inject(EncryptionServiceService);
  const platformId = inject(PLATFORM_ID);

  let adminUrl = '';
  const params = coreService.getGetSearchTag();
  if (params && params['adminurl']) {
    adminUrl = params['adminurl'];
  }

  const islocal = environment.isEncrypt;
  const encryptedBody = req.body && islocal ? encrypt.encrypt(JSON.stringify(req.body)) : req.body;
  const encrypturlquery = islocal && req.url.split('?')[1]
    ? req.url.split('?')[0] + '?' + encrypt.encrypt(req.url.split('?')[1])
    : req.url;

  let newReq: HttpRequest<any>;
  if (authService.accessToken && !authService.isTokenExpired(authService.accessToken)) {
    newReq = req.clone({
      headers: req.headers
        .set('Authorization', 'Bearer ' + authService.accessToken)
        .set('lang', coreService.getCurrentLang())
        .set('adminurl', adminUrl)
        .set('isMobileMode', 'true'),
      responseType: islocal ? 'text' : 'json',
      url: encrypturlquery,
      body: encryptedBody,
    });
  } else {
    newReq = req.clone({
      headers: req.headers
        .set('lang', coreService.getCurrentLang())
        .set('adminurl', adminUrl)
        .set('isMobileMode', 'true'),
      responseType: islocal ? 'text' : 'json',
      url: encrypturlquery,
      body: encryptedBody,
    });
  }

  return next(newReq).pipe(
    map((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        if (event.body && islocal) {
          try {
            const decryptedData = encrypt.decrypt(event.body);
            return event.clone({ body: JSON.parse(decryptedData) });
          } catch {
            throw new HttpErrorResponse({
              error: 'Decryption failed',
              status: event.status,
              statusText: event.statusText,
              url: event.url ?? undefined
            });
          }
        }
      }
      return event;
    }),
    catchError((error) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        authService.logoutUser();
        if (isPlatformBrowser(platformId)) {
          alert('Please login');
          router.navigate(['/']);
        }
      }
      return throwError(() => error);
    })
  );
};
