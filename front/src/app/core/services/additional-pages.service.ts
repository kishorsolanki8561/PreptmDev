import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { API_ROUTES } from '../api.routes';

@Injectable({
  providedIn: 'root'
})
export class AdditionalPagesService {

  constructor(
    private _apiService:ApiService
  ) { }

  getAdditionalPage(pageType:number){
    return this._apiService.get(API_ROUTES.additionalPages.getPage, { id: pageType})
  }
  
  sendUserMessage(payload:any){
    return this._apiService.post<number>(API_ROUTES.additionalPages.sendUserMessageUrl,payload);
  }
}
