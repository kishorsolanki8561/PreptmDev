import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { actionsDdl } from 'src/app/core/models/fixed-value';
import { Page } from 'src/app/core/models/master-models/page.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-page',
  templateUrl: './add-update-page.component.html',
  styleUrls: ['./add-update-page.component.scss']
})
export class AddUpdatePageComponent implements OnInit {
  ddls: Ddls;
  form: FormGroup;
  data: Page;
  formElements: formElement[] = []
  actionsDdl = actionsDdl
  breadcrumb:Breadcrumb[]=[]

  get pageComponentsControl() {
    return this.form.get('pageComponents') as FormArray
  }

  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData']
    this.ddls = initialData.ddls.data;
    this.data = initialData.pageData?.data;
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
      name: [null, Validators.required],
      menuId: [null],
      icon: [null],
      pageUrl:[null,Validators.required],
      pageComponents: this._fb.array([]),

    })

    //adding first url form
    this.addComponent()
  }

  addComponent = () => {
    let pageUrlCtrl = this._fb.group({
      id: [0],
      name: [null, Validators.required],
      pageComponentsAction: [null, Validators.required]
    })
    this.pageComponentsControl.push(pageUrlCtrl)
  }

  removeUrl(index: number) {
    this.pageComponentsControl.removeAt(index);
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Name',
        placeholder: 'Page Name',
        type: 'text'
      },
      {
        controlName: 'icon',
        label: 'Icon',
        placeholder: 'Google Icon for page',
        type: 'text'
      },
      {
        controlName: 'menuId',
        label: 'Menu',
        placeholder: 'Choose menu',
        type: 'select',
        selectConfig:{
          selectOptions: this.ddls['ddlMenu']
        }
      },
      {
        controlName: 'pageUrl',
        label: 'Page URL',
        placeholder: 'URL for listing page',
        type: 'text'
      },
      
    ]
  }

}
