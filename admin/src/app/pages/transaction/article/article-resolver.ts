import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, forkJoin, map } from "rxjs";
import { DdlsList } from "src/app/core/api";
import { ArticleFilterDTO } from "src/app/core/models/article.model";
import { ApiResponseModel } from "src/app/core/models/core.model";
import { Lookup } from "src/app/core/models/fixed-value";
import { ArticleService } from "src/app/core/services/article.service";
import { CoreService } from "src/app/core/services/core.service";

@Injectable()
export class ArticleResolver implements Resolve<any>{
    constructor(
        private _articleService: ArticleService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let articlePayload = new ArticleFilterDTO()
        let dataGetters: Observable<ApiResponseModel<any>>[] = [
            this._coreService.getDdls(DdlsList.article.list),
            this._articleService.getList(articlePayload),
            this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('article-type', '', '')
        ]
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    article: data[1],
                    Lookup : data[2]
                }
            })
        );
    }
}

@Injectable()
export class ArticleAddUpdateResolver implements Resolve<any>{
    constructor(
        private _articleService: ArticleService,
        private _coreService: CoreService
    ) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        let dataGetters: Observable<ApiResponseModel<any>>[] = [this._coreService.getDdls(DdlsList.article.addUpdate),this._coreService.GetDDLLookupDataByLookupTypeIdAndLookupType('article-type', '', '')]
        if (route.params['id']) {

            dataGetters.push(this._articleService.getById(Number(route.params['id'])))
        }
        return forkJoin(dataGetters).pipe(
            map((data) => {
                return {
                    ddls: data[0],
                    Lookup : data[1],
                    article: data[2] ? data[2] : undefined,
                }
            })
        );
    }
}