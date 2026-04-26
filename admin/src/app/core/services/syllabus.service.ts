import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ActivatedRoute } from '@angular/router';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { map } from 'rxjs';
import { NotesDetails, NotesFilterDTO, NotesList } from '../models/notes.model';
import { SyllabusDetails, SyllabusFilterDTO, SyllabusList } from '../models/syllabus.model';

@Injectable({
  providedIn: 'root'
})
export class SyllabusService {

  constructor(
    private _baseService: BaseService,
    private _route: ActivatedRoute
  ) { }

  getList = (payload: SyllabusFilterDTO) => {
    return this._baseService.post<SyllabusList[]>(
      EndPoints.Syllabus.getListUrl,
      payload
    );
  };

  getById = (id: number) => {
    return this._baseService.get<SyllabusDetails>(
      EndPoints.Syllabus.getByIdUrl,
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
      EndPoints.Syllabus.addUpdate,
      payload,
      queryParams
    );
  };

  delete = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Syllabus.deleteUrl,
      { id }
    );
  };

  changeStatus = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Syllabus.changeStatusUrl,
      { id }
    );
  };

  updateProgress = (id: number, progressStatus: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Syllabus.updateProgressUrl,
      { id, progressStatus }
    );
  };

}
