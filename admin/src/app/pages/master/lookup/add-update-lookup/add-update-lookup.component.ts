import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { LookupModel } from 'src/app/core/models/master-models/lookupmodel';
import { MasterService } from 'src/app/core/services/master.service';
import { AddUpdateLookupTypeComponent } from '../../lookup-type/add-update-lookup-type/add-update-lookup-type.component';
import { DdlsList } from 'src/app/core/api';

@Component({
  selector: 'app-add-update-lookup',
  templateUrl: './add-update-lookup.component.html',
  styleUrls: ['./add-update-lookup.component.scss']
})
export class AddUpdateLookupComponent implements OnInit {
  form: FormGroup;
  ddls: Ddls;

  lookup: LookupModel;
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
    this.lookup = initialData?.Lookup?.data;
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
      lookupTypeId: [null, Validators.required],
      titleHindi: [null],
      description: [null],
      descriptionHindi: [null],
      slug: [{ value: null, disabled: true }],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'lookupTypeId',
        label: 'lookup Type',
        placeholder: 'Select lookup Type',
        type: 'select',
        selectConfig: {
          ddlKey: DdlsList.masters.lookupType,
          selectOptions: this.ddls[DdlsList.masters.lookupType],
          addComponent: AddUpdateLookupTypeComponent,
          addComponentClassName: "AddUpdateLookupTypeComponen",
        }
      },
      {
        controlName: 'title',
        label: 'Lookup',
        placeholder: 'Lookup',
        type: 'text',
        translateTo:'titleHindi'
      },
      {
        controlName: 'titleHindi',
        label: 'Lookup(Hindi)',
        placeholder: 'Lookup(Hindi)',
        type: 'text',
      },
      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'Description',
        type: 'text',
        translateTo:'descriptionHindi'
      },
      {
        controlName: 'descriptionHindi',
        label: 'Description(Hindi)',
        placeholder: 'Description(Hindi)',
        type: 'text',
      },

      {
        controlName: 'slug',
        label: 'Slug',
        placeholder: 'Slug',
        type: 'slug',
        slugFrom:'title'
      },

    ];
  }


  close(data: boolean) {
    this.dialogClosed.emit(data)
  }
}
