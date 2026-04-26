import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  ApiResponseModel,
  Breadcrumb,
  columns,
} from 'src/app/core/models/core.model';
import {
  AssetsMasterFilterModel,
  AssetsMasterViewModel,
} from 'src/app/core/models/master-models/assets-master-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-assets-master',
  templateUrl: './assets-master.component.html',
  styleUrls: ['./assets-master.component.scss'],
})
export class AssetsMasterComponent implements OnInit {
  initialData: ApiResponseModel<AssetsMasterViewModel[]>;
  filter: AssetsMasterFilterModel = new AssetsMasterFilterModel();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].assets;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
    {
      columnKey: 'title',
      columnText: 'Title',
      searchType: 'text',
    },
    {
      columnKey: 'directoryName',
      columnText: 'Directory',
      searchType: 'text',
    },
    {
      columnKey: 'path',
      columnText: 'Attachment',
      type: 'copyIcon',
      isHide: true,
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
