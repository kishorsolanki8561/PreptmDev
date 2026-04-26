import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { AssetsMasterModel } from 'src/app/core/models/master-models/assets-master-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-assets-master',
  templateUrl: './add-update-assets-master.component.html',
  styleUrls: ['./add-update-assets-master.component.scss'],
})
export class AddUpdateAssetsMasterComponent implements OnInit {
  form: FormGroup;
  assets: AssetsMasterModel = new AssetsMasterModel();
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  ddls: Ddls;
  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.assets = initialData?.assetsData?.data;
    this.ddls = initialData?.ddls?.data;
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
      title: [null, Validators.required],
      path: [null, Validators.required],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Title',
        placeholder: 'title',
        type: 'text',
      },
      {
        controlName: 'path',
        label: 'Attachment',
        type: 'file',
        fileConfig: {
          isMultiple: false,
          showCropper:false,
        },
      },
    ];
  }

}
