import { Injectable, PLATFORM_ID, Inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { ApiService } from './api.service';
import { API_ROUTES } from '../api.routes';
import { LoginReq } from '../models/auth.model';
import { user } from '../models/user.model';
import { Subject } from 'rxjs';
import { CoreService } from './core.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  isLoggedIn$ = new Subject();
  _userData: user

  get accessToken(): string {
    return this._coreService.getLocalStorage('user')?.token ?? '';
  }


  constructor(
    private _apiService: ApiService,
    private _coreService: CoreService,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) {
    if (this.isTokenExpired(this.accessToken)) {
      this.logoutUser();
    }
  }

  googleLogin(payload: LoginReq) {
    return this._apiService.post<user>(API_ROUTES.auth.login, payload);
  }

  renderGoogleButton(successCB: Function) {
    if (!isPlatformBrowser(this.platformId)) return;
    // @ts-ignore

    window?.google?.accounts?.id?.initialize({
      client_id: "960886981780-msqop4703pafkek5ba3jkt9l75mq1f69.apps.googleusercontent.com",
      callback: successCB.bind(this),
      auto_select: false,
      cancel_on_tap_outside: true,
    });
    // @ts-ignore
    window?.google?.accounts?.id?.renderButton(
      // @ts-ignore
      document.getElementById("google-button"),
      { theme: "outline", size: "large", width: "100%", text: "signin", type:  "standard" }
      // { theme: "outline", size: "large", width: "100%", text: "signin", type: window.innerWidth >= 1024 ? "standard" : "icon" }
    );
    // @ts-ignore
    window?.google?.accounts?.id?.prompt((notification: PromptMomentNotification) => { });
  }


  logoutUser() {
    this._coreService.remmoveFromLocalStorage('user');
    this.loggedStatusChanged(false)
  }

  loggedStatusChanged(isLoggedIn: boolean) {
    this.isLoggedIn$.next(isLoggedIn)
  }

  isTokenExpired(token: string) {
    if (!token) {
      return true;
    }
    const exp = this._coreService.jwtDecode(token).exp * 1000
    return exp < new Date().getTime()
  }

  getUserDetails(): user | null {
    if (this._userData) {
      return { ...this._userData }
    } else {
      const userData = this._coreService.getLocalStorage('user') ?? null
      if (userData) {
        this._userData = userData
        return { ...this._userData };
      } else {
        return null;
      }
    }
  }


  isUserLoggedIn() {
    let userData = this.getUserDetails();
    if (userData) {
      return !this.isTokenExpired(userData.token)
    } else {
      return false
    }
  }


}
