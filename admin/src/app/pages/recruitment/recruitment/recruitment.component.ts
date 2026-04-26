import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  ApiResponseModel,
  Breadcrumb,
  columns,
  Ddls,
} from 'src/app/core/models/core.model';
import { progressDdl } from 'src/app/core/models/fixed-value';
import {
  RecruitmentList,
  RecruitmentListFilter,
} from 'src/app/core/models/recruitment.model';
import { RecruitmentService } from 'src/app/core/services/recruitment.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-recruitment',
  templateUrl: './recruitment.component.html',
  styleUrls: ['./recruitment.component.scss'],
})
export class RecruitmentComponent implements OnInit {
  initialData: ApiResponseModel<RecruitmentList[]>;
  filter = new RecruitmentListFilter();
  ddls: Ddls;
  columns: columns;
  breadcrumb: Breadcrumb[] = [];
  editPageUrl: string;
  viewPageUrl: string;
  addPageUrl: string;

  constructor(
    private _route: ActivatedRoute,
    public recruitmentService: RecruitmentService
  ) {
    this.filter.blockTypeCode = this.recruitmentService.getRedirectMetadata(this._route.snapshot)?.blockTypeCode
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.initialData = initialData.recruitments;
    this.initialData.data = this.initialData.data?.map((res : any)=>
      {return {...res, "copySlugUrl" :  environment.siteUrl + '/' +res.slugUrl} }) || []
    this.addColumns();
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
      this.updatePageUrls();
    });
  }
  addColumns() {
    this.columns = [
      {
        columnKey: 'title',
        columnText: 'Title',
        searchType: 'text',
      },
      // {
      //   columnKey: 'departmentName',
      //   columnText: 'Department Name',
      //   filterKey: 'departmentId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlDepartment'],
      // },
      // {
      //   columnKey: 'blockTypeName',
      //   columnText: 'block Type',
      // },
      // {
      //   columnKey: 'categoryName',
      //   columnText: 'Category',
      //   filterKey: 'categoryId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlCategory'],
      // },

      // {
      //   columnKey: 'subCategoryName',
      //   columnText: 'Sub Category',
      //   filterKey: 'subCategoryId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlSubCategory'],
      // },
      // {
      //   columnKey: 'startDate',
      //   columnText: 'Start Date',
      //   sorting: true,
      //   type: 'date',
      // },
      {
        columnKey: 'visitCount',
        columnText: 'Visit',
        type: 'text',
      },
      // {
      //   columnKey: 'lastDate',
      //   columnText: 'Last Date',
      //   sorting: true,
      //   type: 'date',
      // },
      {
        columnKey: 'publisherName',
        columnText: 'Publisher',
        filterKey: 'publisherId',
        searchType: 'singleSelect',
        filterOptions: this.ddls['ddlPublisher'],
      },
      {
        columnKey: 'publishedDate',
        columnText: 'Published Date',
        sorting: true,
        type: 'date',
      },

      // {
      //   columnKey: 'totalPost',
      //   columnText: 'total Post',
      // },
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
      {
        columnKey: 'status',
        columnText: 'Update Progress',
        type: 'updateProgress',
        statusDdl: progressDdl,
        columnWidth: '150px',
        columnAlign: 'right',
      },
    ];
  }

  updatePageUrls() {
    let data: any = this.recruitmentService.getRedirectMetadata(this._route.snapshot)
    this.editPageUrl = data.editPageUrl
    this.viewPageUrl = data.viewPageUrl
    this.addPageUrl = data.addPageUrl
  }
}
