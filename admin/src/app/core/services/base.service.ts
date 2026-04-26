import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first, Observable } from 'rxjs';
import { ApiResponseModel, Obj } from '../models/core.model';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(
    private _httpClient: HttpClient
  ) {
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

  get<T>(url: string, queryParams: Obj<any> | null = null): Observable<ApiResponseModel<T>> {
    let query = this._objToQueryString(queryParams)
    return this._httpClient.get(url + query).pipe(first()) as Observable<ApiResponseModel<T>>
  }
  post<T>(url: string, reqBody: Obj<any>, otherData?: Obj<any>): Observable<ApiResponseModel<T>> {
    //adding otherData to reqBody for send to api
    if (otherData) {
      for (let key in otherData) {
        if (otherData[key])
          reqBody[key] = otherData[key];
      }
    }
    return this._httpClient.post(url, reqBody).pipe(first()) as Observable<ApiResponseModel<T>>
  }

  postExternal<T>(url: string, reqBody: Obj<any>, otherData?: Obj<any>): Observable<T> {
    //adding otherData to reqBody for send to api
    if (otherData) {
      for (let key in otherData) {
        if (otherData[key])
          reqBody[key] = otherData[key];
      }
    }
    return this._httpClient.post(url, reqBody).pipe(first()) as Observable<T>
  }
  getExternal<T>(url: string, queryParams: Obj<any> | null = null): Observable<T> {
    let query = this._objToQueryString(queryParams)
    return this._httpClient.get(url + query).pipe(first()) as Observable<T>
  }
}
