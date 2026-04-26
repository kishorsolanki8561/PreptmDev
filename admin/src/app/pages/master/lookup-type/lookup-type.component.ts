import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { LookupTypeFilterModel, LookupTypeViewListModel, LookupTypeViewModel } from 'src/app/core/models/master-models/LookupTypeModel';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-lookup-type',
  templateUrl: './lookup-type.component.html',
  styleUrls: ['./lookup-type.component.scss']
})
export class LookupTypeComponent implements OnInit {
  initialData: ApiResponseModel<LookupTypeViewListModel[]>;
  filter: LookupTypeFilterModel = new LookupTypeFilterModel();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].LookupType;
  }
  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
    {
      columnKey: 'title',
      columnText: 'Lookup Type',
      searchType: 'text',
    },
    {
      columnKey: 'titleHindi',
      columnText: 'Lookup Type(Hindi)',
      searchType: 'text',
    },
    {
      columnKey: 'slug',
      columnText: 'Slug',
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
