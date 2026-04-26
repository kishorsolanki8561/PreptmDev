import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { SubCategoryModel } from 'src/app/core/models/master-models/sub-category-model';
import { MasterService } from 'src/app/core/services/master.service';
import { AddUpdateCategoryMasterComponent } from '../../category-master/add-update-category-master/add-update-category-master.component';

@Component({
  selector: 'app-add-update-sub-category',
  templateUrl: './add-update-sub-category.component.html',
  styleUrls: ['./add-update-sub-category.component.scss']
})
export class AddUpdateSubCategoryComponent implements OnInit {
  form: FormGroup;
  ddls: Ddls;

  subCategory: SubCategoryModel;
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];

  @Input() dialogMode = false;
  @Output('dialogClosed') dialogClosed = new EventEmitter<boolean>();

  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.subCategory = initialData?.subcategory?.data;
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
      name: [null, Validators.required],
      nameHindi: [null],
      categoryId: [null, Validators.required],
      // icon: [null, Validators.required],
      icon: [null],
      slugUrl: [{ value: null, disabled: true }],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Name',
        placeholder: 'Name',
        type: 'text',
        translateTo:'nameHindi'
      },
      {
        controlName: 'nameHindi',
        label: 'Name(Hindi)',
        placeholder: 'Name(Hindi)',
        type: 'text',
      },
      {
        controlName: 'categoryId',
        label: 'Category',
        placeholder: 'Select Category',
        type: 'select',
        selectConfig: {
          ddlKey: 'ddlCategory',
          selectOptions: this.ddls['ddlCategory'],
          addComponent: AddUpdateCategoryMasterComponent,
          addComponentClassName: "AddUpdateCategoryMasterComponent",
        }
      },
      {
        controlName: 'slugUrl',
        label: 'Slug',
        placeholder: 'Slug',
        type: 'slug',
        slugFrom:'name'
      },
      {
        controlName: 'icon',
        label: 'icon',
        type: 'file',
        fileConfig: {
          isMultiple: false,
          accept: 'image/*'
        }
      },
    ];
  }


  close(data: boolean) {
    this.dialogClosed.emit(data)
  }

}
