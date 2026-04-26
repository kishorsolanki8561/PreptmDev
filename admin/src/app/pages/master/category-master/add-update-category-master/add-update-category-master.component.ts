import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { category } from 'src/app/core/models/master-models/category-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-category-master',
  templateUrl: './add-update-category-master.component.html',
  styleUrls: ['./add-update-category-master.component.scss'],
})
export class AddUpdateCategoryMasterComponent implements OnInit {
  form: FormGroup;
  category: category;
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
    this.category = initialData?.qualification?.data;
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
  // onEdit() {
  //   this.form.enable();
  // }
}
