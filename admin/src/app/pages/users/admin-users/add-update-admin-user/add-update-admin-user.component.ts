import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EndPoints } from 'src/app/core/api';
import { Breadcrumb, DdlItem, Ddls, formElement } from 'src/app/core/models/core.model';
import { Message } from 'src/app/core/models/fixed-value';
import { AdminUser } from 'src/app/core/models/users.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { BaseService } from 'src/app/core/services/base.service';
import { UsersService } from 'src/app/core/services/users.service';

@Component({
  selector: 'app-add-update-admin-user',
  templateUrl: './add-update-admin-user.component.html',
  styleUrls: ['./add-update-admin-user.component.scss']
})
export class AddUpdateAdminUserComponent implements OnInit {
  ddls: Ddls
  form: FormGroup
  userData: AdminUser;
  formElements: formElement[] = []
  breadcrumb: Breadcrumb[] = [];
  constructor(
    public usersService: UsersService,
    private _route: ActivatedRoute,
    private _fb: FormBuilder
  ) {
    let initialData = this._route.snapshot.data['initialData']
    this.ddls = initialData.ddls.data;
    this.userData = initialData.userData?.data;
    this.initiateFrom();
    this.initiateFormElements();
  }


  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }

  initiateFrom() {
    this.form = this._fb.group({
      name: [null, Validators.required],
      email: [null, Validators.email],
      password: [null, Validators.required],
      userTypeCode: [null, Validators.required],
    })
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Name',
        placeholder: 'User Name',
        type: 'text'
      },
      {
        controlName: 'email',
        label: 'Email',
        placeholder: 'User\'s Email',
        type: 'text'
      },
      {
        controlName: 'password',
        label: 'Password',
        placeholder: 'User\'s Password',
        type: 'text'
      },
      {
        controlName: 'userTypeCode',
        label: 'User Role',
        placeholder: 'Select User\'s Role',
        type: 'select',
        selectConfig:{
          selectOptions: this.ddls['ddlUserType']
        }
      },
    ]
  }
}
