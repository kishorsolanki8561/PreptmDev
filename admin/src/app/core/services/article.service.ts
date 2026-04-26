import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { ActivatedRoute } from '@angular/router';
import { ArticleFilterDTO, ArticleResponseDTO, ArticleTitleCheckDTO, ArticleViewListDTO } from '../models/article.model';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ArticleService {

  constructor(
    private _baseService: BaseService,
    private _route: ActivatedRoute
  ) { }

  getList = (payload: ArticleFilterDTO) => {
    payload.orderBy = "ModifiedDate";
    return this._baseService.post<ArticleViewListDTO[]>(
      EndPoints.Article.getArticleListUrl,
      payload
    );
  };

  getById = (id: number) => {
    return this._baseService.get<ArticleResponseDTO>(
      EndPoints.Article.getArticleByIdUrl,
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
      EndPoints.Article.addUpdateArticle,
      payload,
      queryParams
    );
  };

  CheckTitle = (searchText: string) => {
    return this._baseService.get<ArticleTitleCheckDTO>(
      EndPoints.Article.CheckArticleTitleURL,
      { searchText }
    );
  };

  delete = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Article.deleteArticleUrl,
      { id }
    );
  };

  changeStatus = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.Article.changeStatusArticleUrl,
      { id }
    );
  };

  updateProgress = (id: number, progressStatus: number) => {
    return this._baseService.get<boolean>(
        EndPoints.Article.updateProgressArticleUrl,
        { id, progressStatus }
    );
};

}
