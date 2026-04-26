import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { menuList, menuListFilters } from 'src/app/core/models/master-models/menu.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-menu-master',
  templateUrl: './menu-master.component.html',
  styleUrls: ['./menu-master.component.scss']
})
export class MenuMasterComponent implements OnInit {
  initialData: ApiResponseModel<menuList[]>
  filter = new menuListFilters();
  breadcrumb: Breadcrumb[] = []
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].menus
  }

  ngOnInit(): void {
    this._route.data.subscribe((data)=>{
      this.breadcrumb = data['breadcrumb']
    })
  }


  columns: columns = [
    {
      columnKey: 'menuName',
      columnText: 'Menu Name',
      searchType: 'text',

    },
    {
      columnKey: 'displayName',
      columnText: 'Display Name',
      sorting: true
    },
    {
      columnKey: 'parentMenuName',
      columnText: 'Parent Menu Name',
      searchType: 'text',
      sorting: true
    },
    {
      columnKey: 'position',
      columnText: 'Position',
    },
    {
      columnKey: 'modifiedByName',
      columnText: 'Modified By',
    },
    {
      columnKey: 'modifiedDate',
      columnText: 'Modified Date',
      sorting: true,
      type: 'date'
    },

  ]

}
