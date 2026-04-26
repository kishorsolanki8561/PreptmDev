import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  ApiResponseModel,
  Breadcrumb,
  columns,
} from 'src/app/core/models/core.model';
import {
  BlockTypeFilterModel,
  BlockTypeViewModel,
} from 'src/app/core/models/master-models/block-type-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-block-type-master',
  templateUrl: './block-type-master.component.html',
  styleUrls: ['./block-type-master.component.scss'],
})
export class BlockTypeMasterComponent implements OnInit {
  initialData: ApiResponseModel<BlockTypeViewModel[]>;
  filter: BlockTypeFilterModel = new BlockTypeFilterModel();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].blockType;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }

  columns: columns = [
    {
      columnKey: 'name',
      columnText: 'Name',
      searchType: 'text',
      
    },
    {
      columnKey: 'slugUrl',
      columnText: 'SlugUrl',
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
