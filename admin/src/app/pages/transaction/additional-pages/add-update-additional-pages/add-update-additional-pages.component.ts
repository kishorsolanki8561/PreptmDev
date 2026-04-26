import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { AdditionalPagesTypeDdl, ExamModeDdl } from 'src/app/core/models/fixed-value';
import { AdditionalPagesModel } from 'src/app/core/models/master-models/transaction/additional-pages-model';
import { AdditionalPagesService } from 'src/app/core/services/additional-pages.service';

@Component({
  selector: 'app-add-update-additional-pages',
  templateUrl: './add-update-additional-pages.component.html',
  styleUrls: ['./add-update-additional-pages.component.scss']
})
export class AddUpdateAdditionalPagesComponent implements OnInit {
  form: FormGroup;
  additionalData: AdditionalPagesModel = new AdditionalPagesModel();
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public _additionalPagesService: AdditionalPagesService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.additionalData = initialData?.additionalData?.data;
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
      pageType: [null, Validators.required],
      content: [null, Validators.required],
      contentHindi: [null],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'pageType',
        label: 'Page Type',
        placeholder: 'Choose Page Type',
        type: 'select',
        selectConfig: {
          selectOptions: AdditionalPagesTypeDdl,
        },
        class: 'col-span-3',
      },
      {
        controlName: 'content',
        label: 'Content',
        placeholder: 'content',
        type: 'htmlEditor',
        class: 'col-span-12',
      },
      {
        controlName: 'contentHindi',
        label: 'Content(Hindi)',
        placeholder: 'Content(Hindi)',
        type: 'htmlEditor',
        class: 'col-span-12',
      },
    ];
  }

}
