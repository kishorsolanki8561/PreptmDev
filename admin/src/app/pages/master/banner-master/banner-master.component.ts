import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { BannerFilterModel, BannerViewListModel } from 'src/app/core/models/master-models/banner-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-banner-master',
  templateUrl: './banner-master.component.html',
  styleUrls: ['./banner-master.component.scss']
})
export class BannerMasterComponent implements OnInit {
  initialData: ApiResponseModel<BannerViewListModel[]>;
  filter: BannerFilterModel = new BannerFilterModel();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].Banner;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
    {
      columnKey: 'attachmentUrl',
      columnText: 'Banner',
      type: 'image',
      columnWidth: '100px',
    },
    {
      columnKey: 'title',
      columnText: 'Title',
      searchType: 'text',
    },
    {
      columnKey: 'displayOrder',
      columnText: 'Display Order',
      searchType: 'text',
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
  ];
}
