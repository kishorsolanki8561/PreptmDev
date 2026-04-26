import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns, Ddls } from 'src/app/core/models/core.model';
import { SubCategoryFilterModel, SubCategoryViewModel } from 'src/app/core/models/master-models/sub-category-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-sub-category-master',
  templateUrl: './sub-category-master.component.html',
  styleUrls: ['./sub-category-master.component.scss']
})
export class SubCategoryMasterComponent implements OnInit {
  ddls: Ddls;
  initialData: ApiResponseModel<SubCategoryViewModel[]>;
  filter: SubCategoryFilterModel = new SubCategoryFilterModel();
  breadcrumb: Breadcrumb[] = [];
  columns: columns;

  constructor(
    private _route: ActivatedRoute,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.initialData = initialData.subcategory;
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
        columnKey: 'categoryName',
        columnText: 'Category Name',
        filterKey: 'categoryId',
        searchType: 'singleSelect',
        filterOptions: this.ddls['ddlCategory'],
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
