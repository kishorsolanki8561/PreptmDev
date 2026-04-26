import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { API_ROUTES } from '../api.routes';
import { ArticleDetails, ArticleList, ArticleListFilter, BlockContaintDetails, BookmarkFilter, BookmarkReq, CockpitPanelsPosts, Post, PostDetails, PostDetailsFilter, PostListFilter, RecruitmentDetails, SchemeModel, Search } from '../models/post.model';
import { MixedModuleListTypeEnum, ModuleEnum, PAGE_SIZE } from '../fixed-values';
import { DepartmentDetail, DepartmentDetailsFilter } from '../models/department.model';
import { ddl } from '../models/core.models';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(
    private _apiService: ApiService
  ) { }

  getPostsForCockpitPanels() {
    return this._apiService.get<CockpitPanelsPosts>(API_ROUTES.cockpitPanelsPosts, { pageSize: 6 });
  }

  getPostLists(payload: PostListFilter) {
    return this._apiService.post<Post[]>(API_ROUTES.list, payload)
  }

  getBlockContaintDetails(slugurl: string) {
    return this._apiService.get<BlockContaintDetails>(API_ROUTES.postDetails, { slugurl })
  }

  getArticleList(payload: ArticleListFilter) {
    return this._apiService.post<{ articles: ArticleList[], latestArticle: ArticleList[] }>(API_ROUTES.article.list, payload)
  }

  getTagsList(filterDll: string) {
    return this._apiService.get<ddl>(API_ROUTES.ddl + filterDll)
  }

  getArticles(filterDll:string) {
    return this._apiService.get<ddl>(API_ROUTES.ddl+filterDll)
  }

  getArticleDetails(slugurl: string) {
    return this._apiService.get<ArticleDetails>(API_ROUTES.article.articleDetail, { slugurl })
  }


  getRecruitmentDetails(slugUrl: string) {
    return this._apiService.get<RecruitmentDetails>(API_ROUTES.post.recruitmentDetails, { slugUrl })
  }
  getAdmissionDetails(slugUrl: string) {
    return this._apiService.get<RecruitmentDetails>(API_ROUTES.post.admissionDetails, { slugUrl })
  }

  getSchemeDetails(slugUrl: string) {
    return this._apiService.get<SchemeModel>(API_ROUTES.post.schemeDetails, { slugUrl })
  }

  getBlockContentDetails(slugUrl: string) {
    return this._apiService.get<BlockContaintDetails>(API_ROUTES.post.blockContaintDetails, { slugUrl })
  }

  getDepartmentDetails(payload: DepartmentDetailsFilter) {
    return this._apiService.get<DepartmentDetail>(API_ROUTES.departmentDetails, payload)
  }

  manageBookmark = (shouldAdd: boolean, post: any) => {
    let payload = new BookmarkReq()
    if (post) {
      // remove bookmark model
      if (post.bookmarkId) {
        payload.bookmarkId = post.bookmarkId
      }
      else {
        // add bookmark model
        payload.postId = post.id || 0;
        payload.blockTypeId = post.blockTypeId || 0;
      }

    } else {
      console.error('manageBookmark: post data is missing');
    }
    return this._apiService.get<number>(API_ROUTES.post.bookmark, payload, false)
  }

  search = (payload: Search) => {
    return this._apiService.post<Post[]>(API_ROUTES.post.search, payload)
  }

  bookmarks = (payload: BookmarkFilter) => {
    return this._apiService.post<Post[]>(API_ROUTES.post.bookmarkList, payload, undefined, false)
  }

}
