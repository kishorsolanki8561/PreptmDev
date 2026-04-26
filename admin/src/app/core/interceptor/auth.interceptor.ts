import { Injectable } from '@angular/core';
import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { AuthUtils } from '../auth.utils';
import { CoreService } from '../services/core.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    /**
     * Constructor
     */
    constructor(private _authService: AuthService, private _core: CoreService) {
    }

    /**
     * Intercept
     *
     * @param req
     * @param next
     */
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Clone the request object
        let newReq = req.clone();

        // Request
        //
        // If the access token didn't expire, add the Authorization header.
        // We won't add the Authorization header if the access token expired.
        // This will force the server to return a "401 Unauthorized" response
        // for the protected API routes which our response interceptor will
        // catch and delete the access token from the local storage while logging
        // the user out from the app.
        if (this._authService.accessToken && !AuthUtils.isTokenExpired(this._authService.accessToken)) {
            if (req.url.includes('localhost:3000')) {
                newReq = req.clone({
                    body: (newReq.method === 'POST' && newReq.body && (Object.prototype.hasOwnProperty.call(newReq.body, 'pageSize') && Object.prototype.hasOwnProperty.call(newReq.body, 'page'))) ? { ...this._core.setfilter(req.body, newReq.url.split('api')[2]) } : req.body
                });
            } else
                newReq = req.clone({
                    headers: req.headers.set('Authorization', 'Bearer ' + this._authService.accessToken),
                    body: (newReq.method === 'POST' && newReq.body && (Object.prototype.hasOwnProperty.call(newReq.body, 'pageSize') && Object.prototype.hasOwnProperty.call(newReq.body, 'page'))) ? { ...this._core.setfilter(req.body, newReq.url.split('api')[2]) } : req.body
                });
            // if (newReq.method === 'POST' && newReq.body && (Object.prototype.hasOwnProperty.call(newReq.body, 'pageSize') && Object.prototype.hasOwnProperty.call(newReq.body, 'page')))
            //     req.body = this._core.setfilter(newReq.body, newReq.url.split('api')[2]);
        }

        // Response
        return next.handle(newReq).pipe(
            catchError((error) => {

                // Catch "401 Unauthorized" responses
                if (error instanceof HttpErrorResponse && error.status === 401) {
                    // Sign out
                    this._authService.signOut();

                    // Reload the app
                    location.reload();
                }

                return throwError(error);
            })
        );
    }
}
