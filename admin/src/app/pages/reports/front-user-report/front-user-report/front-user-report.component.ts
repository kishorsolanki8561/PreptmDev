import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, Ddls, columns } from 'src/app/core/models/core.model';
import { FrontUserListModel, UserFilterModel } from 'src/app/core/models/report-models/front-user-report-model';
import { ReportsService } from 'src/app/core/services/reports.service';

@Component({
  selector: 'app-front-user-report',
  templateUrl: './front-user-report.component.html',
  styleUrls: ['./front-user-report.component.scss']
})
export class FrontUserReportComponent implements OnInit {
  initialData: ApiResponseModel<FrontUserListModel[]>;
  filter = new UserFilterModel();
  ddls: Ddls;
  columns: columns;
  breadcrumb: Breadcrumb[] = [];
  constructor(
    public _reportsService: ReportsService,
    private _route: ActivatedRoute,
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.initialData = initialData.frontUserReport;
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
        columnKey: 'profileImg',
        columnText: 'Profile',
        type: 'image',
        columnWidth: '60px',
      },
      {
        columnKey: 'name',
        columnText: 'Name',
        searchType: 'text',
      },
      {
        columnKey: 'email',
        columnText: 'Email',
        searchType: 'text',
      },
      {
        columnKey: 'mobileNumber',
        columnText: 'Mobile Number',
        searchType: 'text',
      },
      {
        columnKey: 'dateOfBirth',
        columnText: 'Date Of Birth',
        type: 'date',
      },
      {
        columnKey: 'stateName',
        columnText: 'State',
        searchType: 'text',
      },
      {
        columnKey: 'provider',
        columnText: 'Provider',
      },
      {
        columnKey: 'platform',
        columnText: 'Platform',
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
