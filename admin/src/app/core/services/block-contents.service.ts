import { Injectable } from '@angular/core';
import { EndPoints } from '../api';
import {
  BlockContentsFilterModel,
  BlockContentsModel,
  BlockContentsViewModel,
  CheckBlockTitleModel,
} from '../models/block-contents-model';
import { EditorVal, Obj } from '../models/core.model';
import { BaseService } from './base.service';
import { map } from 'rxjs';
import { FilesWithPrev, Url } from '../models/FormElementsModel';

@Injectable({
  providedIn: 'root',
})
export class BlockContentsService {
  constructor(private _baseService: BaseService) { }

  //#region <BlockContent>
  getBlockContentsList = (payload: BlockContentsFilterModel) => {
    payload.orderBy = "ModifiedDate";
    return this._baseService.post<BlockContentsViewModel[]>(
      EndPoints.BlockContents.getBlockContentsListUrl,
      payload
    );
  };

  getBlockContentsById = (id: number) => {
    return this._baseService.get<BlockContentsModel>(
      EndPoints.BlockContents.getBlockContentsByIdUrl,
      { id }
    ).pipe(map((result) => {
      
      //#region <html editor>
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

      //#endregion <html editor>

      return result
    }));
  };
  addUpdateBlockContents = (payload: any, queryParams: Obj<number>) => {

    //#region region <html editor>
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
    //#endregion  <html editor>


    return this._baseService.post<number>(
      EndPoints.BlockContents.addUpdateBlockContents,
      payload,
      queryParams
    );
  };
  deleteBlockContents = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BlockContents.deleteBlockContentsUrl,
      { id }
    );
  };

  CheckTitle = (searchText: string) => {
    return this._baseService.get<CheckBlockTitleModel>(
      EndPoints.BlockContents.checkTitleUrl,
      { searchText }
    );
  };

  changeStatusBlockContents = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BlockContents.changeStatusBlockContentsUrl,
      { id }
    );
  };
  updateProgress = (id: number, progressStatus: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BlockContents.BlockContentProgressStatusUrl,
      { id, progressStatus }
    );
  };
  //#endregion
}
