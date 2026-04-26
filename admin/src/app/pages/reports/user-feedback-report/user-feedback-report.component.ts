import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { FeedbackFilterModel, FeedbackListModel } from 'src/app/core/models/report-models/front-user-report-model';
import { ReportsService } from 'src/app/core/services/reports.service';

@Component({
  selector: 'app-user-feedback-report',
  templateUrl: './user-feedback-report.component.html',
  styleUrls: ['./user-feedback-report.component.scss']
})
export class UserFeedbackReportComponent implements OnInit {
  initialData: ApiResponseModel<FeedbackListModel[]>;
  filter = new FeedbackFilterModel();

  columns: columns;
  breadcrumb: Breadcrumb[] = [];
  constructor(
    public _reportsService: ReportsService,
    private _route: ActivatedRoute,
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.initialData = initialData.FeedbackReport;
    this.addColumns();
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  addColumns() {
    this.columns = [
      {
        columnKey: 'userName',
        columnText: 'User Name',
        type: 'text',
      },
      {
        columnKey: 'status',
        columnText: 'Status',
      },
      {
        columnKey: 'type',
        columnText: 'Type',
      },
      {
        columnKey: 'message',
        columnText: 'Message',
      },
      {
        columnKey: 'modifiedByName',
        columnText: 'Modified By',
      },
      {
        columnKey: 'modifiedDate',
        columnText: 'Modified Date',
        sorting: true,
        type: 'date',
      },
    ]
  }
}
