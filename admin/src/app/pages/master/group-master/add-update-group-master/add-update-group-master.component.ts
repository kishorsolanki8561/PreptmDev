import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { GroupMasterModel } from 'src/app/core/models/master-models/group-master-model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-group-master',
  templateUrl: './add-update-group-master.component.html',
  styleUrls: ['./add-update-group-master.component.scss'],
})
export class AddUpdateGroupMasterComponent implements OnInit {
  form: FormGroup;
  group: GroupMasterModel;
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
    this.group = initialData?.group?.data;
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
    });
  }
  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Name',
        placeholder: 'Name',
        type: 'text'
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
    ];
  }

  // onEdit() {
  //   this.form.enable();
  // }

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }
}
