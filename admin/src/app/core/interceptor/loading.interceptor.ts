import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { ProgressBarService } from '../services/progress-bar.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  requestCount = 0
  constructor(
    private progressBarService: ProgressBarService
  ) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (this.requestCount === 0) {
      this.progressBarService.showProgressBar();
    }
    this.requestCount++;
    return next.handle(request).pipe(finalize(() => {
      if (this.requestCount > 0) {
        this.requestCount--;
      }
      if (this.requestCount === 0) {
        this.progressBarService.hideProgressBar();
      }
    }));
  }
}
