import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { BlockTypeModel } from 'src/app/core/models/master-models/block-type-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-block-type',
  templateUrl: './add-update-block-type.component.html',
  styleUrls: ['./add-update-block-type.component.scss'],
})
export class AddUpdateBlockTypeComponent implements OnInit {
  form: FormGroup;
  blockType: BlockTypeModel;
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
    this.blockType = initialData?.blockType?.data;
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
      slugUrl: [{ value: null, disabled: true }, Validators.required],
      description: [null],
      descriptionHindi: [null],
      forRecruitment: [false],
    });
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Name',
        placeholder: 'Name',
        type: 'text',
        class: 'col-span-2',
        translateTo:'nameHindi'
      },
      {
        controlName: 'nameHindi',
        label: 'Name Hindi',
        placeholder: 'Name Hindi',
        type: 'text',
        class: 'col-span-2',
      },

      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'Description',
        type: 'textarea',
        class: 'col-span-2',
        translateTo:'descriptionHindi'

      },
      {
        controlName: 'descriptionHindi',
        label: 'Description Hindi',
        placeholder: 'Description Hindi',
        type: 'textarea',
        class: 'col-span-2',

      },
      {
        controlName: 'slugUrl',
        label: 'Slug',
        placeholder: 'Slug',
        type: 'slug',
        slugFrom:'name'
      },
      {
        controlName: 'forRecruitment',
        label: 'Is Use For Other Posts',
        placeholder: 'Is Use For Other Posts',
        class: 'col-12',
        type: 'switch',
      },
    ];
  }

  // onEdit() {
  //   this.form.enable();
  // }

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }
}
