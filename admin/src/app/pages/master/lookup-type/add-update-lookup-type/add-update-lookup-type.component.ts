import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { LookupTypeModel } from 'src/app/core/models/master-models/LookupTypeModel';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-lookup-type',
  templateUrl: './add-update-lookup-type.component.html',
  styleUrls: ['./add-update-lookup-type.component.scss']
})
export class AddUpdateLookupTypeComponent implements OnInit {
  form: FormGroup;
  lookupType: LookupTypeModel;
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
    this.lookupType = initialData?.LookupType?.data;
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
      slug: [{ value: null, disabled: true }, Validators.required],
      titleHindi: [null],
      description: [null],
      descriptionHindi: [null],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Lookup Type',
        placeholder: 'Lookup Type',
        type: 'text',
      },
      {
        controlName: 'titleHindi',
        label: 'Lookup Type(Hindi)',
        placeholder: 'Lookup Type(Hindi)',
        type: 'text',
      },
      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'description',
        type: 'text',
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
      }
    ];
  }

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }
}
