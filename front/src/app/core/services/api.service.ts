import { HttpClient } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID, TransferState, makeStateKey } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { first } from 'rxjs';
import { ApiResponseModel, Obj } from '../models/core.models';
import { environment } from 'src/environments/environment';
import { isPlatformServer, DOCUMENT } from '@angular/common';
import { API_ROUTES } from '../api.routes';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private _isServer = false;
  constructor(
    private _httpClient: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object,
    private tstate: TransferState,
    @Inject(DOCUMENT) private _doc: Document,
  ) {
    this._isServer = isPlatformServer(platformId);
  }

  /** Returns 'hi' when the current URL contains /hi/ segment, otherwise 'en'.
   *  Used to namespace TransferState keys so English and Hindi responses never
   *  share the same cache entry (prevents stale-language data during hydration). */
  private _getLang(): string {
    const href: string = (this._doc as any)?.location?.href ?? '';
    return href.split('/')[3] === 'hi' ? 'hi' : 'en';
  }

  private _objToQueryString(obj: any) {
    if (!obj)
      return ''
    let str = [];
    for (let key in obj)
      if (obj.hasOwnProperty(key)) {
        str.push(encodeURIComponent(key) + "=" + encodeURIComponent(obj[key]));
      }
    return '?' + str.join("&");
  }

  get<T>(url: string, queryParams: Obj<any> | null = null, shouldCache = true): Observable<ApiResponseModel<T>> {
    let query = this._objToQueryString(queryParams);
    return this.checkAndGetData(url, query, this._httpClient.get(environment.baseApiUrl + url + query).pipe(first()), {}, shouldCache) as Observable<ApiResponseModel<T>>;
  }

  post<T>(url: string, reqBody: Obj<any>, otherData?: Obj<any>, shouldCache = true): Observable<ApiResponseModel<T>> {
    if (otherData) {
      for (let key in otherData) {
        if (otherData[key])
          reqBody[key] = otherData[key];
      }
    }
    return this.checkAndGetData(url, JSON.stringify(reqBody), this._httpClient.post(environment.baseApiUrl + url, reqBody).pipe(first()), {}, shouldCache) as Observable<ApiResponseModel<T>>;
  }

  private checkAndGetData(url: string, additionalData: string, getDataObservable: Observable<any>, defaultValue: any = {}, shouldCache = true) {
    // Include the UI language in the key so English and Hindi responses are stored
    // as separate TransferState entries.  Without this, whichever locale renders
    // first could have its cached data served to the other locale during hydration.
    let key = url + (additionalData || '') + `|${this._getLang()}`;
    let sKey = makeStateKey<any>(key);

    if (shouldCache && this.tstate.hasKey(sKey)) {
      return of(this.tstate.get(sKey, defaultValue));
    } else {
      return getDataObservable.pipe(
        tap((data) => {
          if (shouldCache && this._isServer) {
            this.tstate.set(sKey, data);
          }
        })
      );
    }
  }
}
