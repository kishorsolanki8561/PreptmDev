import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiResponseModel, Breadcrumb, columns, indexModel } from 'src/app/core/models/core.model';
import { RoleList } from 'src/app/core/models/rolemaster.model';
import { UsersService } from 'src/app/core/services/users.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {
  initialData: ApiResponseModel<RoleList[]>;
  breadcrumb: Breadcrumb[] = [];
  filter: indexModel = new  indexModel();
  constructor(
    private _route: ActivatedRoute,
    public _usersService: UsersService
  ) {
    this.initialData = this._route.snapshot.data['initialData'].UsersType;
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  columns: columns = [
    {
      columnKey: 'typeName',
      columnText: 'Type Name',
      searchType: 'text',
    }
  ];
}
