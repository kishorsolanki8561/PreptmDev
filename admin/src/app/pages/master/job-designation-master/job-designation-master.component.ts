import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import {
  jobDesignationList,
  jobDesignationListFilters,
} from 'src/app/core/models/master-models/JobDesignation.Model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-job-designation-master',
  templateUrl: './job-designation-master.component.html',
  styleUrls: ['./job-designation-master.component.scss'],
})
export class JobDesignationMasterComponent implements OnInit {
  initialData: ApiResponseModel<jobDesignationList[]>;
  filter = new jobDesignationListFilters();
  breadcrumb: Breadcrumb[] = []

  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].jobDesignation;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb']
    })
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
