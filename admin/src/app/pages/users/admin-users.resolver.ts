import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, map, Observable } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { AdminUserListReq } from "src/app/core/models/users.model";
import { CoreService } from "src/app/core/services/core.service";
import { UsersService } from "src/app/core/services/users.service";

// @Injectable({
//     providedIn: 'root'
// })
@Injectable()
export class AdminUserResolver implements Resolve<any>{
    constructor(
        private _userService: UsersService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return forkJoin([
            this._userService.getAdminUsersList(new AdminUserListReq())
        ]).pipe(
            map((data) => {
                return {
                    adminUsers: data[0]
                }
            })
        );
    }
}

@Injectable()
export class AdminUserAddResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _userService: UsersService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.masters.adminUserAddUpdateDdls)]
        if (route.params['id']) {
            dataGetters.push(this._userService.getAdminUserById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    userData: data[1] ? data[1] : undefined
                }
            })
        );
    }
}
@Injectable()
export class PagePermissionResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _userService: UsersService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.masters.adminUserAddUpdateDdls)]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                }
            })
        );
    }
}

@Injectable()
export class UserTypeResolver implements Resolve<any>{
    constructor(
        private _userService: UsersService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return forkJoin([
            this._userService.getUsersTypeList()
        ]).pipe(
            map((data) => {
                return {
                    UsersType: data[0]
                }
            })
        );
    }
}

@Injectable()
export class UserTypeAddResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _userService: UsersService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = []
        if (route.params['id']) {
            dataGetters.push(this._userService.getUsersTypeById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    userData: data[0] ? data[0] : undefined
                }
            })
        );
    }
}