import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, switchMap, throwError } from 'rxjs';
import { EndPoints } from '../api';
import { userDetails, credentials } from '../models/auth.model';
import { Obj } from '../models/core.model';
import { BaseService } from './base.service';
import { CoreService } from './core.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _authenticated: boolean = false;
  pagePermissions: any

  constructor(
    private _coreService: CoreService,
    private _baseService: BaseService,
    private _router: Router
  ) { 
  }

  get accessToken(): string {
    return this._coreService.getLocalStorage('userData')?.token ?? '';
  }

  signIn(credentials: credentials) {
    // Throw error, if the user is already logged in
    if (this._authenticated) {
      return throwError(() => new Error('User is already logged in.'));
    }

    return this._baseService.post<userDetails>(EndPoints.loginUrl, credentials).pipe(
      switchMap((res) => {
        this._authenticated = res.isSuccess
        return of(res);
      })
    )
  }

  signOut() {
    // Remove the access token from the local storage
    localStorage.removeItem('userData');

    // Set the authenticated flag to false
    this._authenticated = false;

    this._router.navigate(['/sign-in'])
  }

  check(): Observable<boolean> {
    // Check if the user is logged in
    if (this._authenticated) {
      return of(true);
    }

    // Check the access token availability
    if (!this.accessToken) {
      return of(false);
    }
    return of(true);
  }

  processPermission(rawPermission: any[]) {
    let permissions: any = {}

    // creating group of permissions
    rawPermission.forEach((item: any) => {
      if (permissions[item.pageComponentName]) {
        permissions[item.pageComponentName].push(item.action)
      } else {
        permissions[item.pageComponentName] = []
        permissions[item.pageComponentName].push(item.action)
      }
    });
    return permissions;
  }

  getPermissions() {
    if (!this.pagePermissions)
      this.updatePagePermissions();
    return this.pagePermissions
  }

  updatePagePermissions() {
    this.pagePermissions = this._coreService.getLocalStorage('userData')?.pageComponents
    if (!this.pagePermissions)
      this.signOut();
  }




}
