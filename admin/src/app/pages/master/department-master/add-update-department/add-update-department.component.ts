import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { department } from 'src/app/core/models/master-models/department.model';
import { MasterService } from 'src/app/core/services/master.service';
import { maxCharEachLine, maxLine } from 'src/app/core/validators/form.validator';

@Component({
  selector: 'app-add-update-department',
  templateUrl: './add-update-department.component.html',
  styleUrls: ['./add-update-department.component.scss']
})
export class AddUpdateDepartmentComponent implements OnInit {
  ddls: Ddls
  form: FormGroup
  departments: department;
  formElements: formElement[] = []
  breadcrumb: Breadcrumb[] = []

  @Input() dialogMode = false;
  @Output('dialogClosed') dialogClosed = new EventEmitter<boolean>();

  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public masterService: MasterService
  ) {
    let initialData = this._route.snapshot.data['initialData']
    this.ddls = initialData.ddls.data;
    this.departments = initialData.departmentData?.data;
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
      url: [null],
      slugUrl: [{ value: null, disabled: true }, Validators.required],
      stateId: [null, [Validators.required]],
      shortName: [null],
      logo: [null],
      address: [null],
      addressHindi: [null],
      mapUrl: [null],
      email: [null, [Validators.email]],
      phoneNumber: [null],
      description: [null],
      descriptionHindi: [null],
      shortDescription: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      shortDescriptionHindi: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      faceBookLink: [null],
      twitterLink: [null],
      wikipediaEnglishUrl: [null],
      wikipediaHindiUrl: [null]
    })
  }


  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'name',
        label: 'Department Name',
        placeholder: 'New department name',
        type: 'text',
        translateTo: 'nameHindi'
      },
      {
        controlName: 'nameHindi',
        label: 'Department Name(Hindi)',
        placeholder: 'New Department Name(Hindi)',
        type: 'text'
      },
      {
        controlName: 'shortName',
        label: 'Short Name',
        placeholder: 'Short form of department',
        type: 'text'
      },
      {
        controlName: 'url',
        label: 'Department Website',
        placeholder: 'https://www.xyz.com',
        type: 'text'
      },
      {
        controlName: 'slugUrl',
        label: 'Slug',
        placeholder: 'home-guard-bharti-2025',
        type: 'slug',
        slugFrom: 'name'
      },
      {
        controlName: 'stateId',
        label: 'State',
        placeholder: 'Choose department state',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddls['ddlState']
        }
      },
      {
        controlName: 'address',
        label: 'Address',
        placeholder: 'Address',
        type: 'textarea',
        translateTo: 'addressHindi'
      },
      {
        controlName: 'addressHindi',
        label: 'Address(Hindi)',
        placeholder: 'Address(Hindi)',
        type: 'textarea',

      },
      {
        controlName: 'mapUrl',
        label: 'Google map Url',
        placeholder: 'Map embading source url',
        type: 'text',
      },
      {
        controlName: 'email',
        label: 'Email',
        placeholder: 'abc@xyz.com',
        type: 'text',
      },
      {
        controlName: 'phoneNumber',
        label: 'Contact Number',
        placeholder: '1234567890,1234567890',
        type: 'text',
      },
      {
        controlName: 'faceBookLink',
        label: 'FaceBookLink',
        placeholder: 'abc@xyz.com',
        type: 'text',
      },
      {
        controlName: 'twitterLink',
        label: 'TwitterLink',
        placeholder: 'abc@xyz.com',
        type: 'text',
      },

      {
        controlName: 'wikipediaEnglishUrl',
        label: 'Wikipedia Url',
        placeholder: 'Wikipedia Url',
        type: 'text',
      },

      {
        controlName: 'wikipediaHindiUrl',
        label: 'Wikipedia Url (Hindi)',
        placeholder: 'Wikipedia Url (Hindi)',
        type: 'text',
      },
      {
        controlName: 'shortDescription',
        label: 'Short Description',
        placeholder: 'Short description',
        type: 'textarea',
        class: 'col-span-2',
        translateTo: 'shortDescriptionHindi'
      },
      {
        controlName: 'shortDescriptionHindi',
        label: 'Short Description(Hindi)',
        placeholder: 'Short description(Hindi)',
        type: 'textarea',
        class: 'col-span-2',
      },
      {
        controlName: 'logo',
        label: 'Department Logo',
        type: 'file',
        fileConfig: {
          isMultiple: false,
          accept: 'image/*',
          showCropper: false,
        }
      },
      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'Add full description of this Department',
        class: 'col-span-4',
        id: 'department_master_description',
        type: 'htmlEditor',
      },
      {
        controlName: 'descriptionHindi',
        label: 'Description(Hindi)',
        placeholder: 'Add full description(Hindi) of this Department',
        class: 'col-span-4',
        id: 'department_master_descriptionHindi',
        type: 'htmlEditor',
      }
    ]
  }

  close(data: boolean) {
    this.dialogClosed.emit(data)
  }

}
