import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { PageList, PageListFilter } from 'src/app/core/models/master-models/page.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-page-master',
  templateUrl: './page-master.component.html',
  styleUrls: ['./page-master.component.scss']
})
export class PageMasterComponent implements OnInit {

  initialData: ApiResponseModel<PageList[]>
  filter = new PageListFilter();
  breadcrumb:Breadcrumb[]=[]
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].pages
  }

  ngOnInit(): void {
    this._route.data.subscribe((data)=>{
      this.breadcrumb = data['breadcrumb']
    })
  }


  columns: columns = [
    {
      columnKey: 'name',
      columnText: 'Name',
      searchType: 'text',
      sorting:true

    },
    {
      columnKey: 'menuName',
      columnText: 'Menu Name',
      sorting: true
    },
    {
      columnKey: 'modifiedByName',
      columnText: 'Modified By',
      searchType: 'text',
    },
    {
      columnKey: 'modifiedDate',
      columnText: 'Modified Date',
      sorting: true,
      type: 'date'
    },

  ]

}
