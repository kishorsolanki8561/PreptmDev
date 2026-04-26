import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { PaperFilterDTO } from "src/app/core/models/paper.model";
import { CoreService } from "src/app/core/services/core.service";
import { PaperService } from "src/app/core/services/paper.service";

@Injectable()
export class PaperResolver implements Resolve<any> {
    constructor(
        private _paperService: PaperService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let payload = new PaperFilterDTO()
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._paperService.getList(payload),
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    papers: data[0],
                }
            })
        );
    }
}

@Injectable()
export class PaperAddUpdateResolver implements Resolve<any> {
    constructor(
        private _paperService: PaperService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.paper.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('year')]
        if (route.params['id']) {
            dataGetters.push(this._paperService.getById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    lookupData: data[1],
                    paper: data[2] || undefined,
                }
            })
        );
    }
}