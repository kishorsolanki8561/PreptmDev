import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { jobDesignation } from 'src/app/core/models/master-models/JobDesignation.Model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-job-designation-master',
  templateUrl: './add-update-job-designation-master.component.html',
  styleUrls: ['./add-update-job-designation-master.component.scss'],
})
export class AddUpdateJobDesignationMasterComponent implements OnInit {
  form: FormGroup;
  jobDesignation: jobDesignation;
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
    this.jobDesignation = initialData?.jobDesignationData?.data;
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
      }, {
        controlName: 'nameHindi',
        label: 'Name(Hindi)',
        placeholder: 'Name(Hindi)',
        type: 'text',
      },
    ];
  };

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }

}
