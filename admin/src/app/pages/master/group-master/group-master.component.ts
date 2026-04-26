import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { GroupMasterFilterModel, GroupMasterViewModel } from 'src/app/core/models/master-models/group-master-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-group-master',
  templateUrl: './group-master.component.html',
  styleUrls: ['./group-master.component.scss']
})
export class GroupMasterComponent implements OnInit {
  initialData: ApiResponseModel<GroupMasterViewModel[]>;
  filter: GroupMasterFilterModel = new GroupMasterFilterModel();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].group;
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
      columnKey: 'nameHindi',
      columnText: 'Name(Hindi)',
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
