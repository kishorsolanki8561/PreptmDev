import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { FeedbackFilterModel, UserFilterModel } from "src/app/core/models/report-models/front-user-report-model";
import { CoreService } from "src/app/core/services/core.service";
import { ReportsService } from "src/app/core/services/reports.service";

@Injectable()
export class FrontUserReportResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _reportsService: ReportsService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._coreService.getDdls(DdlsList.FrontUserReport.list),
            this._reportsService.getFrontUserReportList(new UserFilterModel())
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    frontUserReport: data[1]
                }
            })
        );
    }
}


@Injectable()
export class FeedbackReportResolver implements Resolve<any>{
    constructor(
        private _coreService: CoreService,
        private _reportsService: ReportsService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._reportsService.getFrontUserFeedbackReportList(new FeedbackFilterModel())
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    FeedbackReport: data[0]
                }
            })
        );
    }
}