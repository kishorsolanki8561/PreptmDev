import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, formElement } from 'src/app/core/models/core.model';
import { qualification } from 'src/app/core/models/master-models/Qualification.model';
import { MasterService } from 'src/app/core/services/master.service';

@Component({
  selector: 'app-add-update-qualification-master',
  templateUrl: './add-update-qualification-master.component.html',
  styleUrls: ['./add-update-qualification-master.component.scss'],
})
export class AddUpdateQualificationMasterComponent implements OnInit {
  form: FormGroup;
  qualification: qualification;
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
    this.qualification = initialData?.qualification?.data;
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
    ];
  }

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }
}
