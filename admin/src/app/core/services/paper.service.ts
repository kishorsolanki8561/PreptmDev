import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ActivatedRoute } from '@angular/router';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { map } from 'rxjs';
import { PaperDetails, PaperFilterDTO, PaperList } from '../models/paper.model';

@Injectable({
  providedIn: 'root'
})
export class PaperService {

  constructor(
    private _baseService: BaseService,
    private _route: ActivatedRoute
  ) { }

  getList = (payload: PaperFilterDTO) => {
    return this._baseService.post<PaperList[]>(
      EndPoints.Paper.getListUrl,
      payload
    );
  };

  getById = (id: number) => {
    return this._baseService.get<PaperDetails>(
      EndPoints.Paper.getByIdUrl,
      { id }
    ).pipe(map((result) => {
      let desc: EditorVal = { json: result.data.descriptionJson, html: result.data.description }
      result.data.description = desc;

      let descHi: EditorVal = { json: result.data.descriptionJsonHindi, html: result.data.descriptionHindi }
      result.data.descriptionHindi = descHi;
      return result;
    }));
  };

  addUpdate = (payload: any, queryParams: Obj<number>) => {
    let desc = payload.description
    payload.description = desc?.html || ''
    payload.descriptionJson = Object.keys(desc?.json || '{}').length ? desc?.json : '{}'

    let descHi = payload.descriptionHindi
    payload.descriptionHindi = descHi?.html || ''
    payload.descriptionJsonHindi = Object.keys(descHi?.json || '{}').length ? descHi?.json : '{}'

    return this._baseService.post<number>(
      EndPoints.Paper.addUpdate,
      payload,
      queryParams
    );
  };

  delete = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Paper.deleteUrl,
      { id }
    );
  };

  changeStatus = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Paper.changeStatusUrl,
      { id }
    );
  };

  updateProgress = (id: number, progressStatus: number) => {
    return this._baseService.get<boolean>(
        EndPoints.Paper.updateProgressUrl,
        { id, progressStatus }
    );
};

}
