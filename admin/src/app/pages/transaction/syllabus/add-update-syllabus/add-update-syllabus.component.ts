import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { SyllabusDetails } from 'src/app/core/models/syllabus.model';
import { SyllabusService } from 'src/app/core/services/syllabus.service';
import { minLengthArr } from 'src/app/core/validators/form.validator';
import { AddUpdateCategoryMasterComponent } from 'src/app/pages/master/category-master/add-update-category-master/add-update-category-master.component';
import { AddUpdateDepartmentComponent } from 'src/app/pages/master/department-master/add-update-department/add-update-department.component';
import { AddUpdateGroupMasterComponent } from 'src/app/pages/master/group-master/add-update-group-master/add-update-group-master.component';
import { AddUpdateQualificationMasterComponent } from 'src/app/pages/master/qualification-master/add-update-qualification-master/add-update-qualification-master.component';

@Component({
  selector: 'app-add-update-syllabus',
  templateUrl: './add-update-syllabus.component.html',
  styleUrls: ['./add-update-syllabus.component.scss']
})
export class AddUpdateSyllabusComponent implements OnInit {

  form: FormGroup;
  syllabusForm: FormGroup;
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  ddls: Ddls;
  lookupData: Ddls;
  syllabus: SyllabusDetails = new SyllabusDetails()

  constructor(
    public _syllabusService: SyllabusService,
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.lookupData = initialData.lookupData.data;
    this.syllabus = initialData?.syllabus?.data || new SyllabusDetails();
    this.initiateFrom();
    this.initiateFormElements();
  }

  ngOnInit(): void {
  }

  initiateFrom() {
    this.form = this._fb.group({
      id: [null],
      title: [null, Validators.required],
      titleHindi: [null],
      departmentId: [null],
      stateId: [null],
      categoryId: [null],
      syllabusTags: [[]],
      slugUrl: [{ value: null, disabled: !!this.syllabus.publishedDate }, Validators.required],
      qualificationId: [null],
      description: [null],
      descriptionHindi: [null],
      keywords: [null],
      keywordsHindi: [null],
      syllabus_Subjects: [null, minLengthArr(1)],
    });
    this.syllabusForm = this._fb.group({
      id: [null],
      subjectName: [null, Validators.required],
      subjectNameHindi: [null],
      yearId: [null, Validators.required],
      path: [null, Validators.required]
    })
  }


  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Title',
        placeholder: 'Title',
        type: 'text',
        class: 'col-span-2',
        translateTo: 'titleHindi'
      },
      {
        controlName: 'titleHindi',
        label: 'Title(Hindi)',
        placeholder: 'Title(Hindi)',
        type: 'text',
        class: 'col-span-2',
      },
      {
        controlName: 'departmentId',
        label: 'Department',
        placeholder: 'Select Department',
        type: 'select',
        selectConfig: {
          ddlKey: 'ddlDepartment',
          addComponent: AddUpdateDepartmentComponent,
          addComponentClassName: "AddUpdateDepartmentComponent",
          selectOptions: this.ddls['ddlDepartment'],
        }
      },
      {
        controlName: 'stateId',
        label: 'State',
        placeholder: 'Select State',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddls['ddlState'],
        }
      },
      {
        controlName: 'categoryId',
        label: 'Category',
        placeholder: 'Category',
        type: 'select',
        class: 'col-span-2',
        selectConfig: {
          ddlKey: 'ddlCategory',
          addComponent: AddUpdateCategoryMasterComponent,
          addComponentClassName: "AddUpdateCategoryMasterComponent",
          isMultiple: false,
          selectOptions: this.ddls['ddlCategory'],
        }
      },
      {
        controlName: 'syllabusTags',
        label: 'Tags',
        placeholder: 'Used for showing related data',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddls['ddlGroup'],
          ddlKey: 'ddlGroup',
          addComponent: AddUpdateGroupMasterComponent,
          addComponentClassName: "AddUpdateGroupMasterComponent",
          isMultiple: true
        }
      },
      {
        controlName: 'slugUrl',
        label: 'Url Slug',
        placeholder: 'Slug Url',
        type: 'slug',
        slugFrom: 'title',
      },
      {
        controlName: 'qualificationId',
        label: 'Qualification',
        placeholder: 'Choose Job qualification',
        type: 'select',
        selectConfig: {
          isMultiple: false,
          selectOptions: this.ddls['ddlQualification'],
          addComponent: AddUpdateQualificationMasterComponent,
          addComponentClassName: "AddUpdateQualificationMasterComponent",
          ddlKey: 'ddlQualification',
        }
      },
      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'Add full description of this syllabus',
        class: 'col-span-4',
        type: 'htmlEditor',
      },
      {
        controlName: 'descriptionHindi',
        label: 'Description(Hindi)',
        placeholder: 'Add full description of this syllabus',
        class: 'col-span-4',
        type: 'htmlEditor',
      },
      {
        controlName: 'keywords',
        label: 'keywords',
        placeholder: 'keywords',
        type: 'textarea',
        class: 'col-span-2',
        translateTo: 'keywordsHindi'
      },
      {
        controlName: 'keywordsHindi',
        label: 'Keywords(Hindi)',
        placeholder: 'Keywords(Hindi)',
        type: 'textarea',
        class: 'col-span-2',
      },
      {
        controlName: 'syllabus_Subjects',
        label: 'Syllabus',
        type: 'tableForm',
        tableFormConfig: {
          childForm: this.syllabusForm,
          childFormElements: [
            {
              controlName: 'subjectName',
              label: 'Subject Name',
              placeholder: 'Subject Name',
              type: 'text',
              class: 'col-span-2',
              translateTo: 'subjectNameHindi',
            },
            {
              controlName: 'subjectNameHindi',
              label: 'Subject Name(Hindi)',
              placeholder: 'Subject Name(Hindi)',
              type: 'text',
              class: 'col-span-2',
            },
            {
              controlName: 'yearId',
              label: 'Year',
              placeholder: 'Select Year',
              type: 'select',
              selectConfig: {
                selectOptions: this.lookupData['year'],
              },
            },
            {
              controlName: 'path',
              label: 'Path',
              placeholder: 'Path',
              type: 'file',
              class: 'col-span-2',
            },
          ],
        }
      },

    ];
  }

}
