import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { Role } from 'src/app/core/models/rolemaster.model';
import { UsersService } from 'src/app/core/services/users.service';

@Component({
  selector: 'app-add-update-role',
  templateUrl: './add-update-role.component.html',
  styleUrls: ['./add-update-role.component.scss']
})
export class AddUpdateRoleComponent implements OnInit {
  form: FormGroup;
  UserType: Role;
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public _user: UsersService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.UserType = initialData?.userData?.data;
    this.initiateFrom();
    this.initiateFormElements();
  }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb']
    })
  }
  initiateFrom() {
    this.form = this._fb.group({
      typeName: [null, Validators.required],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'typeName',
        label: 'Type Name',
        placeholder: 'Type Name',
        type: 'text',
      }
    ];
  }

  // onEdit() {
  //   this.form.enable();
  // }
}
