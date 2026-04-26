import { Injectable } from '@angular/core';
import { EndPoints } from '../api';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(
    private _baseService: BaseService
  ) { }

  getDynamicMenuList = (userTypeCode: number) => {
    return this._baseService.get<boolean>(EndPoints.menuMaster.getDynamicMenuUrl, { userTypeCode })
  }
}
