import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  ApiResponseModel,
  Breadcrumb,
  columns,
} from 'src/app/core/models/core.model';
import {
  categoryList,
  categoryListFilters,
} from 'src/app/core/models/master-models/category-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-category-master',
  templateUrl: './category-master.component.html',
  styleUrls: ['./category-master.component.scss'],
})
export class CategoryMasterComponent implements OnInit {
  initialData: ApiResponseModel<categoryList[]>;
  filter: categoryListFilters = new categoryListFilters();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].qualification;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
    {
      columnKey: 'icon',
      columnText: 'Icon',
      type: 'image',
      columnWidth: '60px',
    },
    {
      columnKey: 'name',
      columnText: 'Name',
    },
    {
      columnKey: 'nameHindi',
      columnText: 'Name(Hindi)',
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
