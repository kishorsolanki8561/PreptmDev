import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { menu } from 'src/app/core/models/master-models/menu.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-menu',
  templateUrl: './add-update-menu.component.html',
  styleUrls: ['./add-update-menu.component.scss']
})
export class AddUpdateMenuComponent implements OnInit {
  ddls: Ddls
  form: FormGroup
  menu: menu;
  formElements: formElement[] = []
  breadcrumb:Breadcrumb[]=[]

  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData']
    this.ddls = initialData.ddls.data;
    this.menu = initialData.menuData?.data;
    this.initiateFrom();
    this.initiateFormElements();
  }

  ngOnInit(): void {
    this._route.data.subscribe((data)=>{
      this.breadcrumb = data['breadcrumb']
    })
  }

  initiateFrom() {
    this.form = this._fb.group({
      menuName: [null, Validators.required],
      displayName: [null, Validators.required],
      hashChild: [false],
      parentId: [null],
      position: [null, Validators.required],
      iconClass: [null],
      userTypeCodes: [null]
    })
  }


  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'menuName',
        label: 'Menu Name',
        placeholder: 'Pta nhi kaha kaam aana h',
        type: 'text'
      },
      {
        controlName: 'displayName',
        label: 'Display',
        placeholder: 'This will be displayed',
        type: 'text'
      },
      {
        controlName: 'hashChild',
        label: 'Has Child',
        type: 'switch'
      },
      {
        controlName: 'parentId',
        label: 'Parent Menu',
        placeholder: 'Who is parent of this menu',
        type: 'select',
        selectConfig:{
          selectOptions: this.ddls['ddlMenu']
        }
      },
      {
        controlName: 'position',
        label: 'Position',
        placeholder: 'Lower value will show first',
        type: 'text'
      },
      {
        controlName: 'iconClass',
        label: 'Icon',
        placeholder: 'Ant Design Icon',
        type: 'text'
      },
      {
        controlName: 'userTypeCodes',
        label: 'User Role',
        placeholder: 'clear krna h abi',
        type: 'select',
        selectConfig:{
          selectOptions: this.ddls['ddlUserType'],
          isMultiple:true
        }
      },
    ]
  }


}
