import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";
import { EndPoints } from "../api";
import { FilesWithPrev, Url } from "../models/FormElementsModel";
import { map } from "rxjs";
import { EditorVal, Obj } from "../models/core.model";
import { CheckSchemeTitleModel, SchemeFilterModel, SchemeRequestModel, SchemeViewListModel, SchemeViewModel } from "../models/scheme-model";

@Injectable({
    providedIn: 'root'
})
export class SchemeService {

    constructor(
        private _baseService: BaseService,
        private _route: ActivatedRoute
    ) { }


    getList = (payload: SchemeFilterModel) => {
        payload.orderBy = "ModifiedDate";
        return this._baseService.post<SchemeViewListModel[]>(
            EndPoints.Scheme.getSchemeListUrl,
            payload
        );
    };

    getById = (id: number) => {
        return this._baseService.get<SchemeViewModel>(
            EndPoints.Scheme.getSchemeByIdUrl,
            { id }
        ).pipe(map((result) => {

            // let Attachs = result.data.schemeAttachmentLookups || undefined;
            // for (let index = 0; index < Attachs.length; index++) {
            //     Attachs[index].attchPath = new FilesWithPrev([], [new Url(Attachs[index].path)]);
            // }

            let desc: EditorVal = { json: result.data.descriptionJson, html: result.data.description }
            result.data.description = desc;

            let descHi: EditorVal = { json: result.data.descriptionHindiJson, html: result.data.descriptionHindi }
            result.data.descriptionHindi = descHi;

            for (let index = 0; index < result.data.howToApplyAndQuickLinkLookup.length; index++) {
                let element = result.data.howToApplyAndQuickLinkLookup[index];
                if ((element.descriptionJson && element.description) || (element.descriptionHindiJson && element.descriptionHindi)) {
                    let desc: EditorVal = { json: element.descriptionJson ? JSON.parse(element.descriptionJson) : element.descriptionJson, html: element.description }
                    element.description = desc
                    let descHi: EditorVal = { json: element.descriptionHindiJson ? JSON.parse(element.descriptionHindiJson) : element.descriptionHindiJson, html: element.descriptionHindi }
                    element.descriptionHindi = descHi;
                }
            }
            return result;
        }));
    };

    addUpdate = (payload: any, queryParams: Obj<number>) => {

        if (!payload['contactDetail']) {
            delete payload['contactDetail']
        }
        if (!payload['howToApplyAndQuickLinkLookup']) {
            delete payload['howToApplyAndQuickLinkLookup']
        }
       

        // payload['thumbnail'] = payload?.thumbnail?.urls[0]?.path || null;
        payload['documentIds'] = payload?.documentIds?.toString() || '';
        payload['eligibilityIds'] = payload?.eligibilityIds?.toString() || '';

        payload.howToApplyAndQuickLinkLookup.forEach((item: any) => {
            let desc = item.description
            item.description = desc?.html || ''
            if (item.description) {
                item.descriptionJson = Object.keys(desc?.json || null).length ? JSON.stringify(desc?.json) : ''
            }

            let descHi = item.descriptionHindi
            item.descriptionHindi = descHi?.html || ''
            if (item.descriptionHindi) {
                item.descriptionHindiJson = Object.keys(descHi?.json || null).length ? JSON.stringify(descHi?.json) : ''
            }
        })

        let desc = payload.description
        payload.description = desc?.html || ''
        payload.descriptionJson = Object.keys(desc?.json || {}).length ? desc?.json : '{}'

        let descHi = payload.descriptionHindi
        payload.descriptionHindi = descHi?.html || ''
        payload.descriptionHindiJson = Object.keys(descHi?.json || {}).length ? descHi?.json : '{}'


        return this._baseService.post<number>(
            EndPoints.Scheme.addUpdateScheme,
            payload,
            queryParams
        );
    };

    CheckTitle = (searchText: string) => {
        return this._baseService.get<CheckSchemeTitleModel>(
            EndPoints.Scheme.CheckSchemeTitleURL,
            { searchText }
        );
    };

    delete = (id: number) => {
        return this._baseService.get<boolean>(
            EndPoints.Scheme.deleteSchemetUrl,
            { id }
        );
    };
    
    changeStatus = (id: number) => {
        return this._baseService.get<boolean>(
            EndPoints.Scheme.changeStatusSchemeUrl,
            { id }
        );
    };

    updateProgress = (id: number, progressStatus: number) => {
        return this._baseService.get<boolean>(
            EndPoints.Scheme.updateProgressSchemeUrl,
            { id, progressStatus }
        );
    };

}