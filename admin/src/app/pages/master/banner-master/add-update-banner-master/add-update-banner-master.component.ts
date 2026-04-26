import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { BannerModel } from 'src/app/core/models/master-models/banner-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-banner-master',
  templateUrl: './add-update-banner-master.component.html',
  styleUrls: ['./add-update-banner-master.component.scss']
})
export class AddUpdateBannerMasterComponent implements OnInit {
  form: FormGroup;
  banner: BannerModel;
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
    this.banner = initialData?.Banner?.data;
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
      title: [null, Validators.required],
      titleHindi: [null],
      url: [null],
      isAdvt: [false],
      displayOrder: [null, Validators.required],
      attachmentUrl: [null, Validators.required],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Title',
        placeholder: 'Title',
        type: 'text',
        translateTo:'titleHindi'
      },
      {
        controlName: 'titleHindi',
        label: 'Title(Hindi)',
        placeholder: 'Title(Hindi)',
        type: 'text',
      },
      {
        controlName: 'url',
        label: 'Url',
        placeholder: 'Url',
        type: 'text',
      },
      // {
      //   controlName: 'isAdvt',
      //   label: 'Is Advt',
      //   placeholder: 'is Advt',
      //   type: 'switch',
      // },
      {
        controlName: 'displayOrder',
        label: 'Display Order',
        placeholder: 'Display Order',
        type: 'text',
      },
      {
        controlName: 'attachmentUrl',
        label: 'icon',
        type: 'file',
        fileConfig: {
          isMultiple: false,
          accept: 'image/*',
          showCropper:false,
        }
      },
    ];
  }

}
