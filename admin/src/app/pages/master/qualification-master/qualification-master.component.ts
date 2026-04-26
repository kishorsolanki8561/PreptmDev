import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import {
  qualificationList,
  qualificationListFilters,
} from 'src/app/core/models/master-models/Qualification.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-qualification-master',
  templateUrl: './qualification-master.component.html',
  styleUrls: ['./qualification-master.component.scss'],
})
export class QualificationMasterComponent implements OnInit {
  initialData: ApiResponseModel<qualificationList[]>;
  filter = new qualificationListFilters();
  breadcrumb: Breadcrumb[] = []
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].qualification;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb']
    })
  }
  columns: columns = [
    {
      columnKey: 'title',
      columnText: 'Title',
      searchType: 'text',
    },
    {
      columnKey: 'titleHindi',
      columnText: 'Title(Hindi)',
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
