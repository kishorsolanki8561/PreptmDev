import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { map } from 'rxjs';
import { AdditionalPagesViewListModel, AdditionalPagesViewModel } from '../models/master-models/transaction/additional-pages-model';

@Injectable({
  providedIn: 'root'
})
export class AdditionalPagesService {

  constructor(private _baseService: BaseService) { }


  getAdditionalpagesList = () => {
    return this._baseService.get<AdditionalPagesViewListModel[]>(
      EndPoints.additionalpages.getadditionalpagesListUrl
    );
  };
  getAdditionalpagesById = (id: number) => {
    return this._baseService.get<AdditionalPagesViewModel>(
      EndPoints.additionalpages.getadditionalpagesByIdUrl,
      { id }
    ).pipe(map((result) => {

      let desc: EditorVal = { json: result.data.contentJson, html: result.data.content }
      result.data.content = desc;

      let descHi: EditorVal = { json: result.data.contentHindiJson, html: result.data.contentHindi }
      result.data.contentHindi = descHi;

      return result;
    }));
  };
  addUpdateAdditionalpages = (payload: any, queryParams: Obj<number>) => {

    let desc = payload.content
    payload.content = desc?.html || ''
    payload.contentJson = Object.keys(desc?.json || {}).length?desc?.json : '{}'

    let descHi = payload.contentHindi
    payload.contentHindi = descHi?.html || ''
    payload.contentHindiJson = Object.keys(descHi?.json || {}).length?descHi?.json : '{}'

    return this._baseService.post<number>(
      EndPoints.additionalpages.addUpdateadditionalpages,
      payload,
      queryParams
    );
  };
}
