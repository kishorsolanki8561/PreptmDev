import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { SyllabusFilterDTO } from "src/app/core/models/syllabus.model";
import { CoreService } from "src/app/core/services/core.service";
import { SyllabusService } from "src/app/core/services/syllabus.service";

@Injectable()
export class SyllabusResolver implements Resolve<any> {
    constructor(
        private _syllabusService: SyllabusService,
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let payload = new SyllabusFilterDTO()
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._syllabusService.getList(payload),
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    syllabus: data[0],
                }
            })
        );
    }
}

@Injectable()
export class SyllabusAddUpdateResolver implements Resolve<any> {
    constructor(
        private _syllabusService: SyllabusService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.syllabus.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('year')]
        if (route.params['id']) {
            dataGetters.push(this._syllabusService.getById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    lookupData: data[1],
                    syllabus: data[2] || undefined,
                }
            })
        );
    }
}