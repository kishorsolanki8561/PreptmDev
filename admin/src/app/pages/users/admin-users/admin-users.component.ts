import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns, indexModel } from 'src/app/core/models/core.model';
import { AdminUserList, AdminUserListReq } from 'src/app/core/models/users.model';
import { UsersService } from 'src/app/core/services/users.service';

@Component({
  selector: 'app-admin-users',
  templateUrl: './admin-users.component.html',
  styleUrls: ['./admin-users.component.scss']
})
export class AdminUsersComponent implements OnInit {
  initialData: ApiResponseModel<AdminUserList[]>
  filter = new AdminUserListReq();
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    public userService: UsersService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].adminUsers
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
      sorting: true,
      searchType: 'text',
    },
    {
      columnKey: 'email',
      columnText: 'Email',
      sorting: true,
      searchType: 'text',
    },
    {
      columnKey: 'userTypeName',
      columnText: 'User Type',
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
