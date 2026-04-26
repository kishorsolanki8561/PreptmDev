import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { finalize, Observable, tap } from "rxjs";
import { Message } from "../models/fixed-value";
import { AlertService } from "../services/alert.service";
import { AuthService } from "../services/auth.service";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(
        private _router: Router,
        private _alertService: AlertService,
        private _authService: AuthService
    ) { }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


        return next.handle(req).pipe(
            tap({
                next: (event) => {
                    if (event instanceof HttpResponse) {
                        // handling soft errors
                        if (!event?.body?.isSuccess && !event?.url?.includes('localhost:3000')) {

                            // body contains data which gets from resonse of api
                            this._alertService.error(event.body.message);
                        }
                        if (event.status == 401) {
                            this._authService.signOut();
                            this._router.navigate(['/unauthorized'])
                        }
                    }
                    return event;
                },
                error: (error) => {
                    if (error.status == 401) {
                        this._authService.signOut();
                        this._router.navigate(['/sign-in'])
                        this._alertService.error(Message.error.unauthorized)
                        // this._router.navigate(['/unauthorized'])

                    } else if (error.status == 404) {
                        this._alertService.error(Message.error.notFound)
                    } else {
                        this._alertService.error(Message.error.server)
                    }
                }

            }
            ),
            finalize(() => {
                // Set the status to false if there are any errors or the request is completed
                // this._fuseLoadingService._setLoadingStatus(false, req.url);
            }));
    }
}