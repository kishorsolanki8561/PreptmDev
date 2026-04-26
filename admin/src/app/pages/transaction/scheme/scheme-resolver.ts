import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { Lookup } from "src/app/core/models/fixed-value";
import { SchemeFilterModel } from "src/app/core/models/scheme-model";
import { CoreService } from "src/app/core/services/core.service";
import { SchemeService } from "src/app/core/services/scheme.service";

@Injectable()
export class SchemeResolver implements Resolve<any>{
    constructor(
        private _schemeService: SchemeService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let recruitmentPayload = new SchemeFilterModel()
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._coreService.getDdls(DdlsList.scheme.addUpdate),
            this._schemeService.getList({...recruitmentPayload,publisherId: 0} as any),
            this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('', '', `${Lookup.GovtDocument}`)
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    scheme: data[1],
                    lookupsData: data[2]
                }
            })
        );
    }
}

@Injectable()
export class SchemeAddUpdateResolver implements Resolve<any>{
    constructor(
        private _schemeService: SchemeService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.scheme.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('', '', `${Lookup.GovtDocument},${Lookup.SchemeEligibility},${Lookup.IconClassQuickLinks},${Lookup.UpcomingCalendarGroup}`)]
        if (route.params['id']) {

            dataGetters.push(this._schemeService.getById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    lookupsData: data[1],
                    scheme: data[2] ? data[2] : undefined,
                }
            })
        );
    }
}