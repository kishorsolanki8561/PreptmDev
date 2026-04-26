import { Injectable } from '@angular/core';
import { EndPoints } from '../api';
import { indexModel, Obj } from '../models/core.model';
import { RoleList } from '../models/rolemaster.model';

import { AdminUser, AdminUserList, PagePermission } from '../models/users.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(
    private _baseService: BaseService
  ) { }

  //#region <User Master>
  getAdminUsersList = (data: indexModel) => {
    return this._baseService.post<AdminUserList[]>(EndPoints.user.getAdminUserPaginationUrl, data)
  }
  addUpdateAdminUser = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(EndPoints.user.addUpdateAdminUserUrl, payload, queryParams)
  }
  getAdminUserById = (id: number) => {
    return this._baseService.get<AdminUser[]>(EndPoints.user.getAdminUserByIdUrl, { id })
  }
  deleteAdminUser = (id: number) => {
    return this._baseService.get<boolean>(EndPoints.user.deleteAdminUserUrl, { id })
  }
  changeStatusAdminUser = (id: number) => {
    return this._baseService.get<boolean>(EndPoints.user.changeStatusAdminUserUrl, { id })
  }
  //#endregion

  //#region <Permission>

  PagePermissionListByUserTypeCode(userTypeCode: number) {
    return this._baseService.get<PagePermission[]>(EndPoints.user.PagePermissionListByUserTypeCodeUrl, { userTypeCode })
  }
  PageMasterPermissionModifiedById(model: any[], userTypeCode: number) {
    return this._baseService.post<any>(EndPoints.user.PageMasterPermissionModifiedByIdUrl + userTypeCode, model)
  }
  //#endregion

  //#region <Role (User type)>
  getUsersTypeList = () => {
    return this._baseService.get<RoleList[]>(EndPoints.UserTypeMaster.getUserTypeMasterListUrl)
  }
  addUpdateUsersType = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(EndPoints.UserTypeMaster.addUpdateUserTypeMasterUrl, payload, queryParams)
  }
  getUsersTypeById = (id: number) => {
    return this._baseService.get<RoleList[]>(EndPoints.UserTypeMaster.getUserTypeMasterByIdUrl, { id })
  }
  deleteUsersType = (id: number) => {
    return this._baseService.get<boolean>(EndPoints.UserTypeMaster.deleteUserTypeMasterUrl, { id })
  }
  changeStatusUsersType = (id: number) => {
    return this._baseService.get<boolean>(EndPoints.UserTypeMaster.changeUserTypeMasterUrl, { id })
  }
  //#endregion
}
