import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, DdlItem } from 'src/app/core/models/core.model';
import { actionsDdl } from 'src/app/core/models/fixed-value';
import { CoreService } from 'src/app/core/services/core.service';
import { UsersService } from 'src/app/core/services/users.service';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.scss']
})
export class PermissionComponent implements OnInit {

  constructor(private _usersService: UsersService,
    private _route: ActivatedRoute,
    private _coreService: CoreService
  ) { }
  UserTypes: DdlItem[] = [];
  pagePermissionList: any //PagePermission[] = []
  userTypeCode: number = 0
  breadcrumb: Breadcrumb[] = [];
  ngOnInit() {
    let initialData = this._route.snapshot.data['initialData'];
    this.UserTypes = initialData.ddls.data.ddlUserType;
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb']
    })
  }

  pagePermissionListByUserTypeCode(userTypeCode: number) {
    this.userTypeCode = userTypeCode;
    this._usersService.PagePermissionListByUserTypeCode(userTypeCode).subscribe((res) => {
      if (res.isSuccess) {
        this.pagePermissionList = this._coreService.groupListByKey(res.data, 'pageComponentName')
      }
    })
  }
  changeStatus(permissionId: number) {
    this._usersService.PageMasterPermissionModifiedById([permissionId], this.userTypeCode).subscribe((res) => {
      if (res.isSuccess) {
        this.pagePermissionListByUserTypeCode(this.userTypeCode);
      }
    })
  }

  getActionNameById(id: number) {
    return actionsDdl.find(x => x.value === id)
  }
}
