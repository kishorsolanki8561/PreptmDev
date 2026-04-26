import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {
  progressBarStatus$ = new Subject<boolean>()
  constructor() { }

  showProgressBar() {
    this.progressBarStatus$.next(true);
  }
  hideProgressBar() {
    this.progressBarStatus$.next(false);
  }
}
