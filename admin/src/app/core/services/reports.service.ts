import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { EndPoints } from '../api';
import { FeedbackFilterModel, FeedbackListModel, FrontUserListModel, UserFilterModel } from '../models/report-models/front-user-report-model';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(private _baseService: BaseService) { }
  //#region <FrontUserReports>
  getFrontUserReportList = (payload: UserFilterModel) => {
    return this._baseService.post<FrontUserListModel[]>(
      EndPoints.FrontUserReport.FrontUserReportUrl,
      payload
    );
  };
  //#endregion


  //#region <FrontUserReports>
  getFrontUserFeedbackReportList = (payload: FeedbackFilterModel) => {
    return this._baseService.post<FeedbackListModel[]>(
      EndPoints.FrontUserReport.GetFrontUserFeedbackReportUrl,
      payload
    );
  };
  //#endregion
}
