import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { forkJoin, map, Observable } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { Lookup } from "src/app/core/models/fixed-value";
import { RecruitmentListFilter } from "src/app/core/models/recruitment.model";
import { CoreService } from "src/app/core/services/core.service";
import { RecruitmentService } from "src/app/core/services/recruitment.service";

@Injectable()
export class RecruitmentResolver implements Resolve<any>{
    constructor(
        private _recruitmentService: RecruitmentService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let recruitmentPayload = new RecruitmentListFilter()
        recruitmentPayload.blockTypeCode = this._recruitmentService.getRedirectMetadata(route)?.blockTypeCode

        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._coreService.getDdls(DdlsList.recruitment.addUpdate),
            this._recruitmentService.getRecruitmentList(recruitmentPayload)
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    recruitments: data[1]
                }
            })
        );
    }
}

@Injectable()
export class RecruitmentAddUpdateResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _recruitmentService: RecruitmentService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.recruitment.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('', '', `${Lookup.IconClassQuickLinks},${Lookup.UpcomingCalendarGroup}`)
        ]
        if (route.params['id']) {
            dataGetters.push(this._recruitmentService.getRecruitmentById(Number(route.params['id'])))
        }

        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    lookupsData: data[1] ? data[1] : undefined,
                    recruitmentData: data[2] ? data[2] : undefined,

                }
            })
        );
    }
}