import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { NotesFilterDTO } from "src/app/core/models/notes.model";
import { CoreService } from "src/app/core/services/core.service";
import { NotesService } from "src/app/core/services/notes.service";

@Injectable()
export class NotesResolver implements Resolve<any> {
    constructor(
        private _notesService: NotesService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let payload = new NotesFilterDTO()
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._notesService.getList(payload),
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    notes: data[0],
                }
            })
        );
    }
}

@Injectable()
export class NotesAddUpdateResolver implements Resolve<any> {
    constructor(
        private _notesService: NotesService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.notes.addUpdate), this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('year')]
        if (route.params['id']) {
            dataGetters.push(this._notesService.getById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    lookupData: data[1],
                    notes: data[2] || undefined,
                }
            })
        );
    }
}