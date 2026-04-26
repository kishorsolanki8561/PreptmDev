import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DdlsList } from 'src/app/core/api';
import { ApiResponseModel, Breadcrumb, Ddls, columns } from 'src/app/core/models/core.model';
import { LookupFilterModel, LookupViewListModel } from 'src/app/core/models/master-models/lookupmodel';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-lookup',
  templateUrl: './lookup.component.html',
  styleUrls: ['./lookup.component.scss']
})
export class LookupComponent implements OnInit {
  ddls: Ddls;
  initialData: ApiResponseModel<LookupViewListModel[]>;
  filter: LookupFilterModel = new LookupFilterModel();
  breadcrumb: Breadcrumb[] = [];
  columns: columns;

  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.filter.lookupTypeId = -1;
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.initialData = initialData.Lookup;
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
        columnText: 'Lookup',
      },
      {
        columnKey: 'titleHindi',
        columnText: 'Lookup(Hindi)',
      },
      {
        columnKey: 'description',
        columnText: 'Description',
      },
      {
        columnKey: 'lookupTypeName',
        columnText: 'lookup Type Name',
        filterKey: 'lookupTypeId',
        searchType: 'singleSelect',
        filterOptions: this.ddls[DdlsList.masters.lookupType],
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
}
