import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { API_ROUTES } from '../api.routes';
import { map } from 'rxjs';
import { Post } from '../models/post.model';
import { BannerListModel } from '../models/Banner.model';

@Injectable({
  providedIn: 'root'
})
export class SearchService {

  constructor(
    private _apiService: ApiService
  ) { }




  GetPopularBySearchText(SearchText: string) {
    return this._apiService.get<string[]>(API_ROUTES.popularSearch, { numberOfRecord: 6, SearchText: SearchText });
  }

  GetBanners() {
    return this._apiService.get<BannerListModel[]>(API_ROUTES.getBanners);
  }
}
