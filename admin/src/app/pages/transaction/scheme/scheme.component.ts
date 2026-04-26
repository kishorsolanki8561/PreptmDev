import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, Ddls, columns } from 'src/app/core/models/core.model';
import { progressDdl } from 'src/app/core/models/fixed-value';
import { SchemeFilterModel, SchemeViewListModel } from 'src/app/core/models/scheme-model';
import { SchemeService } from 'src/app/core/services/scheme.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-scheme',
  templateUrl: './scheme.component.html',
  styleUrls: ['./scheme.component.scss']
})
export class SchemeComponent implements OnInit {
  ddls: Ddls;
  initialData: ApiResponseModel<SchemeViewListModel[]>;
  filter: SchemeFilterModel = new SchemeFilterModel();
  breadcrumb: Breadcrumb[] = [];
  columns: columns;

  constructor(
    private _route: ActivatedRoute,
    public _schemeService: SchemeService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.initialData = initialData.scheme;
    this.initialData.data = this.initialData.data?.map((res : any)=>
      {return {...res, "copySlugUrl" :  environment.siteUrl + '/' +res.slug} })
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
        columnKey: 'title',
        columnText: 'Title',
      },
      // {
      //   columnKey: 'titleHindi',
      //   columnText: 'Scheme(Hindi)',
      // },
      {
        columnKey: 'visitCount',
        columnText: 'Visit',
        type: 'text',
      },
      // {
      //   columnKey: 'description',
      //   columnText: 'Description',
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
      {
        columnKey: 'slug',
        columnText: 'Slug Url',
        type: 'siteView',
        isHide: true,
      },
      {
        columnKey: 'copySlugUrl',
        columnText: 'Attachment',
        type: 'copyIcon',
        isHide: true,
      },
    ];
  }
}
