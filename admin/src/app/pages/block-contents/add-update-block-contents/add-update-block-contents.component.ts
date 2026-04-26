import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BlockContentsModel, FaqLookup, HowToApplyAndQuickLinkLookup } from 'src/app/core/models/block-contents-model';
import { Breadcrumb, Ddls, EditorVal, formElement } from 'src/app/core/models/core.model';
import { BlockContentsService } from 'src/app/core/services/block-contents.service';
import { CoreService } from 'src/app/core/services/core.service';
import { AddUpdateBlockTypeComponent } from '../../master/block-type-master/add-update-block-type/add-update-block-type.component';
import { AddUpdateDepartmentComponent } from '../../master/department-master/add-update-department/add-update-department.component';
import { AddUpdateCategoryMasterComponent } from '../../master/category-master/add-update-category-master/add-update-category-master.component';
import { AddUpdateSubCategoryComponent } from '../../master/sub-category-master/add-update-sub-category/add-update-sub-category.component';
import { AddUpdateGroupMasterComponent } from '../../master/group-master/add-update-group-master/add-update-group-master.component';
import { ExamModeDdl, Lookup } from 'src/app/core/models/fixed-value';
import { AddUpdateLookupComponent } from '../../master/lookup/add-update-lookup/add-update-lookup.component';
import { DdlsList } from 'src/app/core/api';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { maxCharEachLine, maxLine } from 'src/app/core/validators/form.validator';

@Component({
  selector: 'app-add-update-block-contents',
  templateUrl: './add-update-block-contents.component.html',
  styleUrls: ['./add-update-block-contents.component.scss'],
})
export class AddUpdateBlockContentsComponent implements OnInit, AfterViewInit {
  ddls: Ddls;
  ddlLookups: Ddls;
  form: FormGroup;
  howToOtherLinksForm: FormGroup;
  OtherLinksForm: FormGroup;
  faqForm: FormGroup;
  blockContents: BlockContentsModel = new BlockContentsModel();
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  isUpdateFaq: boolean = false;
  isUpdateHow2Apply: boolean = false;
  isUpdateOtherLinks: boolean = false;
  lookupEnum = Lookup;


  constructor(
    private _route: ActivatedRoute,
    public blockContentsService: BlockContentsService,
    private _fb: FormBuilder,
    private _coreService: CoreService
  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.blockContents = initialData.blockContentData?.data ?? new BlockContentsModel();
    this.ddlLookups = initialData.lookupsData.data;
    this.initiateFrom();
    this.initiateFormElements();
    this.setOutSideFormData();
  }



  get faqLookup() {
    return this.form.get('faqLookup') as FormArray
  }

  get howToApplyAndQuickLinkLookup() {
    return this.form.get('howToApplyAndQuickLinkLookup') as FormArray
  }

  ngOnInit(): void {

    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }
  ngAfterViewInit(): void {
    if (this.form.disabled) {
      this.isUpdateFaq = this.isUpdateOtherLinks = this.isUpdateHow2Apply = true;
    }
  }

  initiateFrom() {
    this.form = this._fb.group({
      title: [null, Validators.required],
      titleHindi: [null],
      blockTypeId: [null, Validators.required],
      recruitmentId: [null],
      departmentId: [null, Validators.required],
      categoryId: [null],
      subCategoryId: [null],
      blockContentTags: [[]],
      slugUrl: [{ value: null, disabled: this.blockContents.publishedDate ? true : false }, Validators.required],
      url: [null],
      keywords: [null],
      keywordsHindi: [null],
      description: [null],
      descriptionHindi: [null],
      date: [null],
      stateId: [null, Validators.required],
      documents: [null],
      summary: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      summaryHindi: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      notificationLink: [null],
      lastDate: [null],
      extendedDate: [null],
      feePaymentLastDate: [null],
      correctionLastDate: [null],
      urlLabelId: [null],
      examMode: [null],
      isCompleted: [null],
      shouldReminder: [null],
      reminderDescription: [null],
      upcomingCalendarCode: [null],
      thumbnail: [null],
      socialMediaUrl: [null],
      thumbnailCredit: [null],
      howToApplyAndQuickLinkLookup: this._fb.array([]),
      faqLookup: this._fb.array([]),
    });
    this.HowToOtherLinksSet();
    this.faqSet();

    this.form.controls['upcomingCalendarCode']?.valueChanges.subscribe((res: any) => {
      if (res) {
        this.form.controls['date']?.addValidators([Validators.required])
        this.form.controls['date']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['shouldReminder']?.addValidators([Validators.required])
        this.form.controls['shouldReminder']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['reminderDescription']?.addValidators([Validators.required])
        this.form.controls['reminderDescription']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.form.controls['date']?.removeValidators([Validators.required])
        this.form.controls['date']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['shouldReminder']?.setValue(null)
        this.form.controls['shouldReminder']?.removeValidators([Validators.required])
        this.form.controls['shouldReminder']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['reminderDescription']?.removeValidators([Validators.required])
        this.form.controls['reminderDescription']?.setValue(null)
        this.form.controls['reminderDescription']?.updateValueAndValidity({ emitEvent: false })
      }
    })
  }

  HowToOtherLinksSet = (isdefault: boolean = false) => {
    this.howToOtherLinksForm = this._fb.group({
      id: [0],
      title: [''],
      titleHindi: [''],
      description: [''],
      descriptionHindi: [''],
      linkUrl: [''],
      iconClass: [''],
      isQuickLink: [true],
      isUpdate: [false]
    })
    //#region HowToApply Validators
    this.howToOtherLinksForm.controls['title']?.valueChanges.subscribe((res: any) => {
      if (!this.howToOtherLinksForm.controls['title']?.value && !this.howToOtherLinksForm.controls['titleHindi']?.value) {
        this.howToOtherLinksForm.controls['title']?.removeValidators([Validators.required])
        this.howToOtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.howToOtherLinksForm.controls['titleHindi']?.removeValidators([Validators.required])
        this.howToOtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.howToOtherLinksForm.controls['title']?.addValidators([Validators.required])
        this.howToOtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.howToOtherLinksForm.controls['titleHindi']?.addValidators([Validators.required])
        this.howToOtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.howToOtherLinksForm.controls['titleHindi']?.valueChanges.subscribe((res: any) => {
      if (!this.howToOtherLinksForm.controls['title']?.value && !this.howToOtherLinksForm.controls['titleHindi']?.value) {
        this.howToOtherLinksForm.controls['title']?.removeValidators([Validators.required])
        this.howToOtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.howToOtherLinksForm.controls['titleHindi']?.removeValidators([Validators.required])
        this.howToOtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })

      }
      else {
        this.howToOtherLinksForm.controls['title']?.addValidators([Validators.required])
        this.howToOtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.howToOtherLinksForm.controls['titleHindi']?.addValidators([Validators.required])
        this.howToOtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })

      }
    })
    //#endregion

    this.OtherLinksForm = this._fb.group({
      id: [0],
      title: [''],
      titleHindi: [''],
      description: [''],
      descriptionHindi: [''],
      linkUrl: [''],
      iconClass: [''],
      isQuickLink: [true],
      isUpdate: [false]
    })

    //#region OtherLinks Validators
    this.OtherLinksForm.controls['title']?.valueChanges.subscribe((res: any) => {
      if (!this.OtherLinksForm.controls['title']?.value && !this.OtherLinksForm.controls['titleHindi']?.value) {
        this.OtherLinksForm.controls['title']?.removeValidators([Validators.required])
        this.OtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.OtherLinksForm.controls['titleHindi']?.removeValidators([Validators.required])
        this.OtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.OtherLinksForm.controls['title']?.addValidators([Validators.required])
        this.OtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.OtherLinksForm.controls['titleHindi']?.addValidators([Validators.required])
        this.OtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.OtherLinksForm.controls['titleHindi']?.valueChanges.subscribe((res: any) => {
      if (!this.OtherLinksForm.controls['title']?.value && !this.OtherLinksForm.controls['titleHindi']?.value) {
        this.OtherLinksForm.controls['title']?.removeValidators([Validators.required])
        this.OtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.OtherLinksForm.controls['titleHindi']?.removeValidators([Validators.required])
        this.OtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })

      }
      else {
        this.OtherLinksForm.controls['title']?.addValidators([Validators.required])
        this.OtherLinksForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.OtherLinksForm.controls['titleHindi']?.addValidators([Validators.required])
        this.OtherLinksForm.controls['titleHindi']?.updateValueAndValidity({ emitEvent: false })

      }
    })
    //#endregion

    if (isdefault)
      this.howToApplyAndQuickLinkLookup.push(this._fb.group(this.howToOtherLinksForm.value));
  }

  faqSet = (isdefault: boolean = false) => {
    this.faqForm = this._fb.group({
      id: [0],
      que: [''],
      ans: [''],
      queHindi: [''],
      ansHindi: [''],
      isUpdate: [false]
    })
    //#region FAQ Validators
    this.faqForm.controls['que']?.valueChanges.subscribe((res: any) => {
      if (!this.faqForm.controls['que']?.value && !this.faqForm.controls['ans']?.value) {
        this.faqForm.controls['que']?.removeValidators([Validators.required])
        this.faqForm.controls['que']?.updateValueAndValidity({ emitEvent: false })
        this.faqForm.controls['ans']?.removeValidators([Validators.required])
        this.faqForm.controls['ans']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.faqForm.controls['que']?.addValidators([Validators.required])
        this.faqForm.controls['que']?.updateValueAndValidity({ emitEvent: false })
        this.faqForm.controls['ans']?.addValidators([Validators.required])
        this.faqForm.controls['ans']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.faqForm.controls['ans']?.valueChanges.subscribe((res: any) => {
      if (!this.faqForm.controls['que']?.value && !this.faqForm.controls['ans']?.value) {
        this.faqForm.controls['que']?.removeValidators([Validators.required])
        this.faqForm.controls['que']?.updateValueAndValidity({ emitEvent: false })
        this.faqForm.controls['ans']?.removeValidators([Validators.required])
        this.faqForm.controls['ans']?.updateValueAndValidity({ emitEvent: false })

      }
      else {
        this.faqForm.controls['que']?.addValidators([Validators.required])
        this.faqForm.controls['que']?.updateValueAndValidity({ emitEvent: false })
        this.faqForm.controls['ans']?.addValidators([Validators.required])
        this.faqForm.controls['ans']?.updateValueAndValidity({ emitEvent: false })

      }
    })
    //#endregion

    if (isdefault)
      this.faqLookup.push(this._fb.group(this.faqForm.value));
  }

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Title',
        placeholder: 'Block Title',
        type: 'text',
        class: 'col-span-2',
        otherApiCall: this.blockContentsService.CheckTitle,
        isOtherApi: true,
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
        controlName: 'blockTypeId',
        label: 'Block Type',
        placeholder: 'Select Block Type',
        type: 'select',
        selectConfig: {
          ddlKey: 'ddlBlockType',
          selectOptions: this.ddls['ddlBlockType'],
          addComponent: AddUpdateBlockTypeComponent,
          addComponentClassName: "AddUpdateBlockTypeComponent"
        }
      },
      {
        controlName: 'recruitmentId',
        label: 'Recruitment',
        placeholder: 'Select Recruitment',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddls['ddlRecruitment'],
        }
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
        placeholder: 'Select Category',
        type: 'select',
        selectConfig: {
          ddlKey: 'ddlCategory',
          addComponent: AddUpdateCategoryMasterComponent,
          addComponentClassName: "AddUpdateCategoryMasterComponent",
          selectOptions: this.ddls['ddlCategory'],
          childDdl: {
            DdlMethodRef: this._coreService.getSubCategoryDdlById,
            childControlName: 'subCategoryId'
          }
        }
      },
      {
        controlName: 'subCategoryId',
        label: 'Sub Category',
        placeholder: 'Select Sub Category',
        type: 'select',
        selectConfig: {
          addComponent: AddUpdateSubCategoryComponent,
          addComponentClassName: "AddUpdateSubCategoryComponent",
          selectOptions: []
        }
      },
      {
        controlName: 'blockContentTags',
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
        label: 'SlugUrl',
        placeholder: 'Slug Url',
        type: 'slug',
        slugFrom: 'title',
        isDisabled: this.blockContents.publishedDate ? true : false
      },
      {
        controlName: 'upcomingCalendarCode',
        label: 'Upcoming Calendar',
        placeholder: 'Upcoming Calendar',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddlLookups[Lookup.UpcomingCalendarGroup.toString()],
          ddlKey: Lookup.UpcomingCalendarGroup.toString(),
        }
      },
      {
        controlName: 'date',
        label: 'Start Date',
        type: 'date',
      },
      {
        controlName: 'lastDate',
        label: 'Last Date',
        type: 'date',
      },
      {
        controlName: 'extendedDate',
        label: 'Extended Date',
        type: 'date',
      },
      {
        controlName: 'correctionLastDate',
        label: 'Correction Last Date',
        type: 'date',
      },
      {
        controlName: 'feePaymentLastDate',
        label: 'Fee Payment Last Date',
        type: 'date',
      },
      {
        controlName: 'examMode',
        label: 'Exam Mode',
        placeholder: 'Choose exam mode',
        type: 'select',
        selectConfig: {
          selectOptions: ExamModeDdl,
        }
      },
      // {
      //   controlName: 'urlLabelId',
      //   label: 'Url Label',
      //   placeholder: 'Url Label',
      //   type: 'select',
      //   selectConfig: {
      //     selectOptions: this.ddlLookups[Lookup.BlockContentUrlLabel.toString()],
      //     // addComponent: AddUpdateLookupComponent,
      //     // addComponentClassName: "AddUpdateLookupComponent",
      //     ddlKey: Lookup.BlockContentUrlLabel.toString(),
      //     // childDdl: {
      //     //   DdlMethodRef: this._coreService.fnGetDdls,
      //     //   childControlName: 'lookupTypeId'
      //     // }
      //   }
      // },

      {
        controlName: 'summary',
        label: 'Summary',
        placeholder: 'Summary',
        type: 'textarea',
        class: 'col-span-2',
        translateTo: 'summaryHindi'
      },
      {
        controlName: 'summaryHindi',
        label: 'Summary(Hindi)',
        placeholder: 'Summary(Hindi)',
        type: 'textarea',
        class: 'col-span-2',
      },
      {
        controlName: 'keywords',
        label: 'Keywords',
        placeholder: 'Keywords',
        type: 'text',
        class: 'col-span-2',
        translateTo: 'keywordsHindi'

      },
      {
        controlName: 'keywordsHindi',
        label: 'Keywords(Hindi)',
        placeholder: 'Keywords(Hindi)',
        type: 'text',
        class: 'col-span-2',

      },
      // {
      //   controlName: 'url',
      //   label: 'Url',
      //   placeholder: 'https://www.xyz.com',
      //   type: 'text',
      // },
      {
        controlName: 'socialMediaUrl',
        label: 'Social Media Link',
        placeholder: 'Social Media Url',
        type: 'text',
        class: 'col-span-2',

      },
      {
        controlName: 'shouldReminder',
        label: 'Should Reminder',
        placeholder: 'Should Reminder',
        type: 'date',
      },
      {
        controlName: 'reminderDescription',
        label: 'Reminder Description',
        placeholder: 'Reminder Description',
        type: 'textarea',
        class: 'col-span-2',
      },
      {
        controlName: 'isCompleted',
        label: 'Completed Post',
        placeholder: 'Completed Post',
        type: 'switch',
      },
      {
        controlName: 'description',
        label: 'Description',
        placeholder: 'Add full description of this recruitment',
        class: 'col-span-4',
        type: 'htmlEditor',
      },
      {
        controlName: 'descriptionHindi',
        label: 'Description(Hindi)',
        placeholder: 'Add full description of this recruitment',
        class: 'col-span-4',
        type: 'htmlEditor',
      },
      {
        controlName: 'thumbnailCredit',
        label: 'Thumbnail Credit',
        placeholder: 'Thumbnail Credit',
        type: 'text',
        class: 'col-span-2',

      },
      {
        controlName: 'thumbnail',
        label: 'Thumbnail',
        type: 'file',
        fileConfig: {
          accept: 'image/*',
          isMultiple: false,
          isThumbnail: true,
          showCropper: false,
        },
      },
      {
        controlName: 'documents',
        label: 'Upload Images',
        type: 'file',
        class: 'col-span-2',
        fileConfig: {
          accept: 'image/*',
          isMultiple: true,
        },
      }
      ,
      {
        controlName: 'notificationLink',
        label: 'Upload Notification',
        type: 'file',
        selectConfig: {
          isMultiple: false,
        },
        fileConfig: {
          accept: 'application/pdf',
        },
      },
      // {
      //   controlName: 'howTo',
      //   label: 'How to',
      //   placeholder: 'How to apply',
      //   class: 'col-span-4',
      //   type: 'htmlEditor',
      // },
    ];
  }


  //#region howToApply
  addHowToApplyData(how2Apply: any) {
    how2Apply.isQuickLink = false;
    this.isUpdateHow2Apply = false;
    if (!this.howToOtherLinksForm.valid) {
      this.howToOtherLinksForm.markAllAsTouched();
      return;
    }
    if (how2Apply.id) {
      if (this.blockContents.id)
        how2Apply.isUpdate = true;
      var index = this.blockContents.howToApplyAndQuickLinkLookup.findIndex(s => s.id == how2Apply.id);
      this.blockContents.howToApplyAndQuickLinkLookup[index] = how2Apply;
    }
    else {
      how2Apply.isUpdate = false;
      this.blockContents.howToApplyAndQuickLinkLookup.push(how2Apply);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.blockContents.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.blockContents.howToApplyAndQuickLinkLookup);
    this.isUpdateHow2Apply = false;
    this.howToOtherLinksForm.reset();
    this.howToOtherLinksForm.get("id")?.setValue(0);
  }

  updateHowToApply(how2Apply: any, index: number) {
    how2Apply.isQuickLink = false;
    if (!this.howToOtherLinksForm.valid) {
      this.howToOtherLinksForm.markAllAsTouched();
      return;
    }
    if (how2Apply.id == 0) {
      this.blockContents.howToApplyAndQuickLinkLookup.splice(index, 1);
      this.howToApplyAndQuickLinkLookup.removeAt(index);
    }
    else {
      this.isUpdateHow2Apply = true;
      how2Apply['isUpdate'] = false;
    }
    this.howToOtherLinksForm.get('description')?.setValue(new EditorVal())
    this.howToOtherLinksForm.get('descriptionHindi')?.setValue(new EditorVal())
    this.howToOtherLinksForm.patchValue(how2Apply);
  }

  deleteHowToApply(index: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.blockContents.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetHowToApply(how2Apply: any) {
    if (how2Apply.Id > 0 && (how2Apply.title || how2Apply.titleHindi || how2Apply.description || how2Apply.descriptionHindi || how2Apply.linkUrl)) {
      this.blockContents.howToApplyAndQuickLinkLookup.push(how2Apply);
    }
    this.isUpdateHow2Apply = false;
    this.howToOtherLinksForm.reset();
    this.howToOtherLinksForm.get("id")?.setValue(0);
  }

  //#endregion

  //#region otherLinks
  addOtherLinksData(otherLinks: any) {
    otherLinks.isQuickLink = true;
    if (!this.OtherLinksForm.valid) {
      this.OtherLinksForm.markAllAsTouched();
      return;
    }
    this.isUpdateOtherLinks = false;
    if (otherLinks.id) {
      if (this.blockContents.id)
        otherLinks.isUpdate = true;
      var index = this.blockContents.howToApplyAndQuickLinkLookup.findIndex(s => s.id == otherLinks.id);
      this.blockContents.howToApplyAndQuickLinkLookup[index] = otherLinks;
    }
    else {
      otherLinks.isUpdate = false;
      this.blockContents.howToApplyAndQuickLinkLookup.push(otherLinks);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.blockContents.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.blockContents.howToApplyAndQuickLinkLookup);

    this.isUpdateOtherLinks = false;
    this.OtherLinksForm.reset();
    this.OtherLinksForm.get("id")?.setValue(0);
  }

  updateOtherLinks(otherLinks: any, index: number) {
    otherLinks.isQuickLink = true;
    if (!this.OtherLinksForm.valid) {
      this.OtherLinksForm.markAllAsTouched();
      return;
    }
    if (otherLinks.id == 0) {
      this.blockContents.howToApplyAndQuickLinkLookup.splice(index, 1);
      this.howToApplyAndQuickLinkLookup.removeAt(index);
    }
    else {
      this.isUpdateOtherLinks = true;
      otherLinks['isUpdate'] = false;
    }
    this.OtherLinksForm.patchValue(otherLinks);
  }

  deleteOtherLinks(index: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.blockContents.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetOtherLinks(otherLinks: any) {
    if (otherLinks.Id > 0 && (otherLinks.title || otherLinks.titleHindi || otherLinks.description || otherLinks.descriptionHindi || otherLinks.linkUrl || otherLinks.iconClass)) {
      this.blockContents.howToApplyAndQuickLinkLookup.push(otherLinks);
    }
    this.isUpdateOtherLinks = false;
    this.OtherLinksForm.reset();
    this.OtherLinksForm.get("id")?.setValue(0);
  }
  //#endregion

  //#region FAQ

  addFaqData(faq: any) {
    if (!this.faqLookup.valid) {
      this.faqLookup.markAllAsTouched();
      return;
    }
    this.isUpdateFaq = false;
    if (faq.id) {
      if (this.blockContents.id)
        faq.isUpdate = true;
      var index = this.blockContents.faqLookup.findIndex(s => s.id == faq.id);
      this.blockContents.faqLookup[index] = faq;
    }
    else {
      faq.isUpdate = false;
      this.blockContents.faqLookup.push(faq);
    }
    this.faqLookup.clear();
    for (let index = 0; index < this.blockContents.faqLookup.length; index++) {
      this.faqSet(true);
    }
    this.faqLookup.patchValue(this.blockContents.faqLookup);
    this.isUpdateFaq = false;
    this.faqForm.reset();
    this.faqForm.get("id")?.setValue(0);
  }

  updateFaq(faq: any, index: number) {
    if (!this.faqLookup.valid) {
      this.faqLookup.markAllAsTouched();
      return;
    }
    if (faq.id == 0) {
      this.blockContents.faqLookup.splice(index, 1);
      this.faqLookup.removeAt(index);

    }
    else {
      this.isUpdateFaq = true;
      faq['isUpdate'] = false;
    }
    this.faqForm.patchValue(faq);
  }

  deleteFaq(index: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.blockContents.faqLookup.splice(index, 1);
    this.faqLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetFaq(faq: any) {
    if (faq.Id > 0 && (faq.que || faq.ans || faq.queHindi || faq.ansHindi)) {
      this.blockContents.faqLookup.push(faq);
    }
    this.isUpdateFaq = false;
    this.faqForm.reset();
    this.faqForm.get("id")?.setValue(0);
  }
  //#endregion

  setOutSideFormData() {
    if (this.blockContents && this.blockContents.id && this.blockContents.howToApplyAndQuickLinkLookup && this.blockContents.howToApplyAndQuickLinkLookup.length) {
      this.howToApplyAndQuickLinkLookup.clear();
      this.blockContents.howToApplyAndQuickLinkLookup.forEach((element, index) => {
        this.howToApplyAndQuickLinkLookup.push(this._fb.group(this.howToOtherLinksForm.value));
        this.howToApplyAndQuickLinkLookup.controls[index].patchValue(element);
      });
    }
    if (this.blockContents && this.blockContents.id && this.blockContents.faqLookup && this.blockContents.faqLookup.length) {
      this.faqLookup.clear();
      this.blockContents.faqLookup.forEach((element, index) => {
        this.faqLookup.push(this._fb.group(this.faqForm.value));
        this.faqLookup.controls[index].patchValue(element);
      });
    }
  }

  translate(fromControl?: FormControl, toControl?: FormControl) {
    this._coreService.translateFormCtrl(fromControl, toControl)
  }
}
