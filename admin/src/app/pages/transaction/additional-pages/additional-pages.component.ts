import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { AdditionalPagesTypeDdl } from 'src/app/core/models/fixed-value';
import { AdditionalPagesViewListModel } from 'src/app/core/models/master-models/transaction/additional-pages-model';
import { AdditionalPagesService } from 'src/app/core/services/additional-pages.service';

@Component({
  selector: 'app-additional-pages',
  templateUrl: './additional-pages.component.html',
  styleUrls: ['./additional-pages.component.scss']
})
export class AdditionalPagesComponent implements OnInit {
  initialData: ApiResponseModel<AdditionalPagesViewListModel[]>;
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public _additionalPagesService: AdditionalPagesService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].additionalData;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
 
    {
      columnKey: 'pageType',
      columnText: 'Page Type',
      type: 'updateProgress',
      statusDdl: AdditionalPagesTypeDdl,
      columnWidth: '400px',
      columnAlign: 'right',
    },
  ];
}
