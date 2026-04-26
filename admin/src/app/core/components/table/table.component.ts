import { Component, ElementRef, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { environment } from 'src/environments/environment';
import { ApiResponseModel } from '../../models/core.model';
import { Message, DefaultTableLength, ActionTypes } from '../../models/fixed-value';
import { AlertService } from '../../services/alert.service';
import { AuthService } from '../../services/auth.service';
import { CoreService } from '../../services/core.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  message = Message;
  @Input() initialData: ApiResponseModel<any[]>;
  @Input() columns: any[] = [];
  @Input() getDataFn$: any;
  @Input() changeStatusFn$: any;
  @Input() progressUpdateFn$: any;
  @Input() deleteFn$: any;
  @Input() editPageUrl: string = '';
  @Input() viewPageUrl: string = '';
  @Input() filter: any;
  @Input() slugKey: any;
  environment = environment

  data: any[] = [];
  totalRecords = 0;
  isfirstCallDone = false;
  currentlyChangingStateId = 0;
  goingToEditId = 0;
  goingToViewId = 0;

  isTableLoading: boolean = false;

  defaultTableLength = DefaultTableLength;
  fileBasePath = environment.fileEndPoint;
  searchableColumn: any = {};
  permissions = {
    viewDetails: false,
    edit: false,
    delete: false,
    changeStatus: false,
    updateProgress: false
  }
  imageFallback =
    'data:image/png;base64,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';
  constructor(
    private _router: Router,
    private _alertService: AlertService,
    private _authService: AuthService,
    private _route: ActivatedRoute,
    private _core: CoreService,
  ) {
    this.columns = this.columns.filter(d => d.isHide == false);
  }

  ngOnInit(): void {
    // initialize table by the data of resolver
    if (this.initialData.isSuccess && (this.initialData.data && this.initialData.data.length)) {
      this.filter = { ...this.filter, ...this._core.getLocalStorage("filter") }
      this.data = this.initialData.data;
      this.totalRecords = this.initialData.totalRecords;
    }
    this.createSearchableColumns();
    this.updatePermissions()
  }

  updatePermissions() {
    let permissions = this._authService.getPermissions()
    let component = this._route.snapshot.data['component']
    let actions = permissions[component]

    this.permissions.changeStatus = actions.includes(ActionTypes.STATUS_CHANGE)
    this.permissions.delete = actions.includes(ActionTypes.DELETE)
    this.permissions.edit = actions.includes(ActionTypes.EDIT)
    this.permissions.updateProgress = actions.includes(ActionTypes.UPDATE_PROGRESS)
    this.permissions.viewDetails = actions.includes(ActionTypes.VIEW_DETAILS)
  }

  search(obj: any) {
    obj.isVisible = false;
    this.updateFilters();
    this.getData();
  }

  updateFilters() {
    Object.keys(this.searchableColumn).forEach((filter: string) => {
      if (this.searchableColumn[filter].type == 'text')
        (this.filter as any)[filter] =
          this.searchableColumn[filter].value || '';
      else if (this.searchableColumn[filter].type == 'singleSelect')
        (this.filter as any)[filter] = this.searchableColumn[filter].value || 0;
      else if (this.searchableColumn[filter].type == 'multiSelect')
        (this.filter as any)[filter] =
          this.searchableColumn[filter].value || [];
    });
  }

  reset(obj: any) {
    obj.value = null;
    this.search(obj);
  }

  createSearchableColumns() {
    if (this.columns.length) {
      this.columns.forEach((col) => {
        if (col.searchType) {
          this.searchableColumn[col.filterKey || col.columnKey] = {
            isVisible: false,
            value: null,
            type: col.searchType,
          };
        }
      });
    }
  }

  onQueryParamsChange(e: NzTableQueryParams) {
    if (this.filter) {

      this.filter.page = e.pageIndex;
      this.filter.pageSize = e.pageSize;
      this.filter.orderBy = '';
      e.sort.forEach((ele) => {
        if (ele.value) {
          this.filter.orderBy = ele.key;
          this.filter.orderByAsc = ele.value === 'ascend' ? 1 : 0;
        }
      });
    }

    this.updateFilters();
    this.getData();
  }

  onDelete(id: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.deleteFn$(id).subscribe((data: any) => {
      if (data.isSuccess) {
        this.getData();
        this._alertService.success(data.message);
      } else {
        deleteBtn.nzLoading = false;
      }
    });
  }

  getData() {
    if (!this.isfirstCallDone) {
      this.isfirstCallDone = true;
      return;
    }
    this.isTableLoading = true;
    this._core.isUpdateFilter = true;
    this.filter.orderBy = this.filter.orderBy || "ModifiedDate";
    this.getDataFn$(this.filter).subscribe(
      (result: ApiResponseModel<any[]>) => {
        this.isTableLoading = false;
        if (result.isSuccess) {
          this.data = result.data;
          this.totalRecords = result.totalRecords;
        } else {
          this.data = [];
          this.totalRecords = 0;
        }
        this._core.isUpdateFilter = false;
      }
    );
  }

  changeStatus(id: number) {
    this.currentlyChangingStateId = id;
    this.changeStatusFn$(id).subscribe(
      (result: ApiResponseModel<boolean>) => {
        this.currentlyChangingStateId = 0;
        if (result.isSuccess) {
          this.getData();
        }
      },
      () => {
        this.currentlyChangingStateId = 0;
      }
    );
  }

  onProgressUpdate(id: number, progressStatus: number) {
    this.progressUpdateFn$(id, progressStatus).subscribe(
      (result: ApiResponseModel<boolean>) => {
        if (result.isSuccess) {
          this._alertService.success(result.message);
          this.getData();
        } else {
          this._alertService.error(result.message);
        }
      }
    );
  }

  edit(event: any, id: number) {
    if (event.ctrlKey) {
      this.goingToEditId = 0;
      const url = this._router.serializeUrl(
        this._router.createUrlTree([this.editPageUrl, id])
      );
      window.open(url, '_blank');
    } else {
      this.goingToEditId = id;
      this._router.navigate([this.editPageUrl, id]);
    }
  }

  view(event: any, id: number) {
    if (event.ctrlKey) {
      this.goingToViewId = 0;
      const url = this._router.serializeUrl(
        this._router.createUrlTree([this.viewPageUrl, id])
      );
      window.open(url, '_blank');
    } else {
      this.goingToViewId = id;
      this._router.navigate([this.viewPageUrl, id]);
    }
  }
}
