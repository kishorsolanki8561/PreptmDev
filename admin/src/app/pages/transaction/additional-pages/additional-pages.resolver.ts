import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { AdditionalPagesService } from "src/app/core/services/additional-pages.service";
import { CoreService } from "src/app/core/services/core.service";

@Injectable()
export class AdditionalPagesResolver implements Resolve<any>{
    constructor(
        private _additionalPagesService: AdditionalPagesService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._additionalPagesService.getAdditionalpagesList()
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    additionalData: data[0]
                }
            })
        );
    }
}

@Injectable()
export class AdditionalPagesAddUpdateResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _additionalPagesService: AdditionalPagesService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = []
        if (route.params['id']) {
            dataGetters.push(this._additionalPagesService.getAdditionalpagesById(Number(route.params['id'])))
        }else{
            return true
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    additionalData: data[0] ? data[0] : undefined
                }
            })
        );
    }
}