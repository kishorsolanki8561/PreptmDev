import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  BlockContentsFilterModel,
  BlockContentsViewModel,
} from 'src/app/core/models/block-contents-model';
import {
  ApiResponseModel,
  Breadcrumb,
  columns,
  Ddls,
} from 'src/app/core/models/core.model';
import { progressDdl } from 'src/app/core/models/fixed-value';
import { BlockContentsService } from 'src/app/core/services/block-contents.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-block-contents',
  templateUrl: './block-contents.component.html',
  styleUrls: ['./block-contents.component.scss'],
})
export class BlockContentsComponent implements OnInit {
  initialData: ApiResponseModel<BlockContentsViewModel[]>;
  filter: BlockContentsFilterModel = new BlockContentsFilterModel();
  breadcrumb: Breadcrumb[] = [];
  ddls: Ddls;
  columns: columns;
  constructor(
    private _route: ActivatedRoute,
    public blockContentsService: BlockContentsService
  ) {
    let data = this._route.snapshot.data['initialData'];
    this.initialData = data.blockContent;
    this.initialData.data = this.initialData.data?.map((res : any)=>
      {return {...res, "copySlugUrl" :  environment.siteUrl + '/' +res.slugUrl} }) || []
    this.ddls = data?.ddls?.data;
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
        searchType: 'text',

      },
      {
        columnKey: 'blockTypeName',
        columnText: 'block Type',
      },
      // {
      //   columnKey: 'blockTypeName',
      //   columnText: 'Block Type',
      //   filterKey: 'blockTypeId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlBlockType'],
      // },
      // {
      //   columnKey: 'recruitmentTitle',
      //   columnText: 'Recruitment',
      //   filterKey: 'recruitmentId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlRecruitment'],
      // },
      // {
      //   columnKey: 'departmentName',
      //   columnText: 'Department',
      //   filterKey: 'departmentId',
      //   searchType: 'singleSelect',
      //   filterOptions: this.ddls['ddlDepartment'],
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
      {
        columnKey: 'visitCount',
        columnText: 'Visit',
      }
      ,
      {
        columnKey: 'publisherName',
        columnText: 'Publisher',
        searchType: 'singleSelect',
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
        columnKey: 'slugUrl',
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
