import { indexModel } from "./core.model";

export class AdminUserListReq extends indexModel {
    name: string = '';
    email: string = '';
}
export class AdminUserList {
    id: number;
    name: string;
    email: string;
    userTypeCode: number;
    isActive: boolean = true;
    modifiedByName: number;
    modifiedDate: string;
}
export class AdminUser {
    id: number;
    name: string;
    email: string;
    userTypeCode: number;
    isActive: boolean;
    modifiedBy: number;
    modifiedDate: string;
}
export class PagePermission
{
    id: number;
    action: number;
    isAllowed:boolean;
    menuName: string;
    pageName: string;
    pageComponentName: string;
}
// export class PagePermissionModel
// {
//     id: number;
//     isAdd: Boolean;
//     isEdit: Boolean;
//     isDelete: Boolean;
//     isView: Boolean;
//   }