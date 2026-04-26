import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { FilesWithPrev, Url } from '../models/FormElementsModel';
import { CheckRecruitmentTitleModel, Recruitment, RecruitmentList, RecruitmentListFilter } from '../models/recruitment.model';
import { BaseService } from './base.service';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { ContentTypeLocal, ContentTypeProd } from '../models/fixed-value';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class RecruitmentService {

  constructor(
    private _baseService: BaseService,
    private _route: ActivatedRoute
  ) { }


  getRecruitmentList = (payload: RecruitmentListFilter) => {
    return this._baseService.post<RecruitmentList[]>(
      EndPoints.recruitment.getRecruitmentListUrl,
      payload
    );
  };
  getRedirectMetadata(routeSnapshot: ActivatedRouteSnapshot): any {

    switch (routeSnapshot.data['component']) {
      case 'RecruitmentComponent':
      case 'AddUpdateRecruitmentComponent':
        return {
          viewPageUrl: '/post/recruitments',
          editPageUrl: '/post/recruitments/edit',
          addPageUrl: '/post/recruitments/add',
          blockTypeCode: environment.production ? ContentTypeProd.RECRUITMENT : ContentTypeLocal.RECRUITMENT
        }
      case 'AdmissionComponent':
      case 'AddUpdateAdmissionComponent':
        return {
          viewPageUrl: '/post/admission',
          editPageUrl: '/post/admission/edit',
          addPageUrl: '/post/admission/add',
          blockTypeCode: environment.production ? ContentTypeProd.ADMISSION : ContentTypeLocal.ADMISSION
        }
    }
  }
  getRecruitmentById = (id: number) => {
    return this._baseService.get<Recruitment>(
      EndPoints.recruitment.getRecruitmentByIdUrl,
      { id }
    ).pipe(map((result) => {
      // result.data['recruitmentStartEndDate'] = [new Date(result.data.startDate), new Date(result.data.lastDate)];
      result.data['docs'] = result.data.attachments;
      result.data['thumbnailImage'] = result.data.thumbnail;
      result.data['notificationFile'] = result.data.notificationLink;
      // if (result.data.howToApplyAndQuickLinkLookup) {
      //   result.data.howToApply = result.data.howToApplyAndQuickLinkLookup.filter(x => x.isQuickLink == false);
      //   result.data.otherLinksList = result.data.howToApplyAndQuickLinkLookup.filter(x => x.isQuickLink == true);
      // }

      //#region <html editor>
      let desc: EditorVal = { json: result.data.descriptionJson, html: result.data.description }
      result.data.description = desc;

      let descHi: EditorVal = { json: result.data.descriptionHindiJson, html: result.data.descriptionHindi }
      result.data.descriptionHindi = descHi;

      for (let index = 0; index < result.data.howToApplyAndQuickLinkLookup.length; index++) {
        let element = result.data.howToApplyAndQuickLinkLookup[index];
        if((element.descriptionJson && element.description) || (element.descriptionHindiJson && element.descriptionHindi)){
          let desc: EditorVal = { json: element.descriptionJson ? JSON.parse(element.descriptionJson) : element.descriptionJson, html: element.description }
          element.description = desc
          let descHi: EditorVal = { json: element.descriptionHindiJson ? JSON.parse(element.descriptionHindiJson) : element.descriptionHindiJson, html: element.descriptionHindi }
          element.descriptionHindi = descHi;
        }
        
      }
      
      //#endregion <html editor>

      return result;
    }));
  };

  addUpdateRecruitment = (payload: any, queryParams: Obj<number>) => {

    
    // if (payload['recruitmentStartEndDate']?.length) {
    //   payload['startDate'] = payload['recruitmentStartEndDate'][0];
    //   payload['lastDate'] = payload['recruitmentStartEndDate'][1];
    // }

    // files upload
    if (!payload['howToApplyAndQuickLinkLookup']) {
      delete payload['howToApplyAndQuickLinkLookup']
    }
    //#region region <html editor>
    payload.howToApplyAndQuickLinkLookup.forEach((item: any) => {
      let desc = item.description
      item.description = desc?.html || ''
      if (item.description) {
        item.descriptionJson = Object.keys(desc?.json || "{}").length ? JSON.stringify(desc?.json) : ''
      }

      let descHi = item.descriptionHindi
      item.descriptionHindi = descHi?.html || ''
      if (item.descriptionHindi) {
        item.descriptionHindiJson = Object.keys(descHi?.json || "{}").length ? JSON.stringify(descHi?.json) : ''
      }
    })

    let desc = payload.description
    payload.description = desc?.html || ''
    payload.descriptionJson = Object.keys(desc?.json || "{}").length ? desc?.json : ''

    let descHi = payload.descriptionHindi
    payload.descriptionHindi = descHi?.html || ''
    payload.descriptionHindiJson = (descHi && descHi.json && Object.keys(descHi.json).length) ? descHi.json : ''
    //#endregion  <html editor>

    // payload['attachments'] = [{path:payload['attachments'][0]}]
 


    return this._baseService.post<number>(
      EndPoints.recruitment.addUpdateRecruitment,
      payload,
      queryParams
    );
  };


  deleteRecruitment = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.recruitment.deleteRecruitmentUrl,
      { id }
    );
  };

  CheckRecruitmentTitle = (searchText: string) => {
    return this._baseService.get<CheckRecruitmentTitleModel>(
      EndPoints.recruitment.checkRecruitmentTitleUrl,
      { searchText }
    );
  };

  changeStatusRecruitment = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.recruitment.changeStatusRecruitmentUrl,
      { id }
    );
  };

  updateProgress = (id: number, progressStatus: number) => {
    return this._baseService.get<boolean>(
      EndPoints.recruitment.updateProgressRecruitmentUrl,
      { id, progressStatus }
    );
  };

}
