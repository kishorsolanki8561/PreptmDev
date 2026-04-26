import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { departmentList, departmentListFilters } from 'src/app/core/models/master-models/department.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-department-master',
  templateUrl: './department-master.component.html',
  styleUrls: ['./department-master.component.scss']
})
export class DepartmentMasterComponent implements OnInit {
  initialData: ApiResponseModel<departmentList[]>
  filter = new departmentListFilters();
  breadcrumb: Breadcrumb[] = []

  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService,
  ) {
    this.initialData = this._route.snapshot.data['initialData'].departments;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb']
    })
  }


  columns: columns = [
    {
      columnKey: 'logo',
      columnText: 'Logo',
      type: 'image',
      columnWidth: '60px',
    },
    {
      columnKey: 'name',
      columnText: 'Department Name',
      searchType: 'text',
    },
    {
      columnKey: 'shortName',
      columnText: 'Short Name',
      searchType: 'text',
    },
    {
      columnKey: 'phoneNumber',
      columnText: 'Contact No',
      searchType: 'text',
    },
    {
      columnKey: 'url',
      columnText: 'Website',
    },
    {
      columnKey: 'stateName',
      columnText: 'State Name',
      sorting: true,
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

  ]

}
