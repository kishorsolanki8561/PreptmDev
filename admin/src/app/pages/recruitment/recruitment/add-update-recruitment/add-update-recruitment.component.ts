import { AfterViewChecked, AfterViewInit, Component, EventEmitter, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { Breadcrumb, DdlItem, Ddls, EditorVal, formElement, NumberFormat, radioOption } from 'src/app/core/models/core.model';
import { ContentTypeLocal, ExamModeDdl, Lookup, Message } from 'src/app/core/models/fixed-value';
import { FaqLookup, HowToApplyAndQuickLinkLookup, Recruitment } from 'src/app/core/models/recruitment.model';
import { CoreService } from 'src/app/core/services/core.service';
import { RecruitmentService } from 'src/app/core/services/recruitment.service';
import { maxCharEachLine, maxLine } from 'src/app/core/validators/form.validator';
import { AddUpdateCategoryMasterComponent } from 'src/app/pages/master/category-master/add-update-category-master/add-update-category-master.component';
import { AddUpdateDepartmentComponent } from 'src/app/pages/master/department-master/add-update-department/add-update-department.component';
import { AddUpdateGroupMasterComponent } from 'src/app/pages/master/group-master/add-update-group-master/add-update-group-master.component';
import { AddUpdateJobDesignationMasterComponent } from 'src/app/pages/master/job-designation-master/add-update-job-designation-master/add-update-job-designation-master.component';
import { AddUpdateQualificationMasterComponent } from 'src/app/pages/master/qualification-master/add-update-qualification-master/add-update-qualification-master.component';
import { AddUpdateSubCategoryComponent } from 'src/app/pages/master/sub-category-master/add-update-sub-category/add-update-sub-category.component';

@Component({
  selector: 'app-add-update-recruitment',
  templateUrl: './add-update-recruitment.component.html',
  styleUrls: ['./add-update-recruitment.component.scss'],
})
export class AddUpdateRecruitmentComponent implements OnInit, AfterViewInit {
  ddls: Ddls;
  ddlLookups: Ddls;
  iconList: DdlItem[] = [];
  form: FormGroup;
  howToOtherLinksForm: FormGroup;
  OtherLinksForm: FormGroup;
  faqForm: FormGroup;
  indexd: number = 0;
  recruitment: Recruitment = new Recruitment();
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  navigationUrlOnSuccess: string;
  message = Message;
  isUpdateFaq: boolean = false;
  isUpdateHow2Apply: boolean = false;
  isUpdateOtherLinks: boolean = false;
  lookupEnum = Lookup;

  get faqLookup() {
    return this.form.get('faqLookup') as FormArray
  }

  get howToApplyAndQuickLinkLookup() {
    return this.form.get('howToApplyAndQuickLinkLookup') as FormArray
  }
  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public recruitmentService: RecruitmentService,
    private _coreService: CoreService
  ) {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.ddlLookups = initialData?.lookupsData?.data ?? undefined;
    this.recruitment = initialData.recruitmentData?.data ?? new Recruitment();
    this.initiateFrom();
    this.initiateFormElements();
    this.navigationUrlOnSuccess = recruitmentService.getRedirectMetadata(this._route.snapshot).viewPageUrl
    this.setOutSideFormData();
  }

  ngAfterViewInit(): void {
    if (this.form.disabled) {
      this.isUpdateFaq = this.isUpdateOtherLinks = this.isUpdateHow2Apply = true;
    }
  }

  ngOnInit(): void {

  }

  //#region initiate From Groups
  initiateFrom() {
    this.form = this._fb.group({
      title: [null, Validators.required],
      titleHindi: [null],
      departmentId: [null, Validators.required],
      salary: [null],
      description: [null],
      descriptionHindi: [null],
      minAge: [null],
      maxAge: [null],
      startDate: [null],
      lastDate: [null],
      extendedDate: [null],
      feePaymentLastDate: [null],
      correctionLastDate: [null],
      admitCardDate: [null],
      examMode: [null, Validators.required],
      applyLink: [null],
      officialLink: [null],
      notificationLink: [null],
      totalPost: [null],
      shortDesription: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      shortDesriptionHindi: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      slugUrl: [{ value: null, disabled: this.recruitment.publishedDate ? true : false }, Validators.required,],
      jobDesignations: [[]],
      qualifications: [[]],
      attachments: [null],
      thumbnail: [null],
      categoryId: [null],
      subCategoryId: [null],
      keywords: [null],
      keywordsHindi: [null],
      stateId: [null, Validators.required],
      blockTypeCode: [null, Validators.required],
      thumbnailCaption: [null],
      socialMediaUrl: [null],
      isCompleted: [null],
      isPrivate: [false],
      shouldReminder: [null],
      reminderDescription: [null],
      upcomingCalendarCode: [null],
      howToApplyAndQuickLinkLookup: this._fb.array([]),
      faqLookup: this._fb.array([]),
      tags: [[]],
      // radio:[1]
    });
    this.HowToOtherLinksSet();
    this.faqSet();
    this.updateBlockTypeCode();
    this.form.controls['upcomingCalendarCode']?.valueChanges.subscribe((res: any) => {
      if (res) {
        this.form.controls['startDate']?.addValidators([Validators.required])
        this.form.controls['startDate']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['shouldReminder']?.addValidators([Validators.required])
        this.form.controls['shouldReminder']?.updateValueAndValidity({ emitEvent: false })
        this.form.controls['reminderDescription']?.addValidators([Validators.required])
        this.form.controls['reminderDescription']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.form.controls['startDate']?.removeValidators([Validators.required])
        this.form.controls['startDate']?.updateValueAndValidity({ emitEvent: false })
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

  // faqLookup: 

  //#endregion

  //#region howToApply
  addHowToApplyData(how2Apply: any) {
    if (!this.howToOtherLinksForm.valid) {
      this.howToOtherLinksForm.markAllAsTouched();
      return;
    }
    this.isUpdateHow2Apply = false;
    how2Apply.isQuickLink = false;
    if (how2Apply.id) {
      if (this.recruitment.id)
        how2Apply.isUpdate = true;
      var index = this.recruitment.howToApplyAndQuickLinkLookup.findIndex(s => s.id == how2Apply.id);
      this.recruitment.howToApplyAndQuickLinkLookup[index] = how2Apply;
    }
    else {
      how2Apply.isUpdate = false;
      this.recruitment.howToApplyAndQuickLinkLookup.push(how2Apply);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.recruitment.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.recruitment.howToApplyAndQuickLinkLookup);
    this.isUpdateHow2Apply = false;
    this.howToOtherLinksForm.reset();
    this.howToOtherLinksForm.get("id")?.setValue(0);
  }

  updateHowToApply(how2Apply: any, index: number) {
    how2Apply.isQuickLink = false;
    if (!this.howToOtherLinksForm.valid) {
      this.howToOtherLinksForm.markAllAsTouched();
      return
    }
    if (how2Apply.id == 0) {
      this.recruitment.howToApplyAndQuickLinkLookup.splice(index, 1);
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
    this.recruitment.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetHowToApply(how2Apply: any) {
    if (how2Apply.Id > 0 && (how2Apply.title || how2Apply.titleHindi || how2Apply.description || how2Apply.descriptionHindi || how2Apply.linkUrl)) {
      this.recruitment.howToApplyAndQuickLinkLookup.push(how2Apply);
    }
    this.isUpdateHow2Apply = false;
    this.howToOtherLinksForm.reset();
    this.howToOtherLinksForm.get("id")?.setValue(0);
  }
  //#endregion

  //#region otherLinks

  addOtherLinksData(otherLinks: any) {
    otherLinks = { ...otherLinks }
    otherLinks.isQuickLink = true;
    if (!this.OtherLinksForm.valid) {
      this.OtherLinksForm.markAllAsTouched();
      return
    }
    this.isUpdateOtherLinks = false;
    if (otherLinks.id) {
      if (this.recruitment.id)
        otherLinks.isUpdate = true;
      var index = this.recruitment.howToApplyAndQuickLinkLookup.findIndex(s => s.id == otherLinks.id);
      this.recruitment.howToApplyAndQuickLinkLookup[index] = otherLinks;
    }
    else {
      otherLinks.isUpdate = false;
      this.recruitment.howToApplyAndQuickLinkLookup.push(otherLinks);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.recruitment.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.recruitment.howToApplyAndQuickLinkLookup);
    this.isUpdateOtherLinks = false;
    this.OtherLinksForm.reset();
    this.OtherLinksForm.get("id")?.setValue(0);
  }

  updateOtherLinks(otherLinks: any, index: number) {
    otherLinks.isQuickLink = true;
    if (!this.OtherLinksForm.valid) {
      this.OtherLinksForm.markAllAsTouched();
      return
    }
    if (otherLinks.id == 0) {
      this.recruitment.howToApplyAndQuickLinkLookup.splice(index, 1);
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
    this.recruitment.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetOtherLinks(otherLinks: any) {
    if (otherLinks.Id > 0 && (otherLinks.title || otherLinks.titleHindi || otherLinks.description || otherLinks.descriptionHindi || otherLinks.linkUrl || otherLinks.iconClass)) {
      this.recruitment.howToApplyAndQuickLinkLookup.push(otherLinks);
    }
    this.isUpdateOtherLinks = false;
    this.OtherLinksForm.reset();
    this.howToOtherLinksForm.get("id")?.setValue(0);
  }

  //#endregion

  //#region FAQ
  addFaqData(faq: any) {
    if (!this.faqLookup.valid) {
      this.faqLookup.markAllAsTouched();
      return
    }
    this.isUpdateFaq = false;
    if (faq.id) {
      if (this.recruitment.id)
        faq.isUpdate = true;
      var index = this.recruitment.faqLookup.findIndex(s => s.id == faq.id);
      this.recruitment.faqLookup[index] = faq;
    }
    else {
      faq.isUpdate = false;
      this.recruitment.faqLookup.push(faq);
    }
    this.faqLookup.clear();
    for (let index = 0; index < this.recruitment.faqLookup.length; index++) {
      this.faqSet(true);
    }
    this.faqLookup.patchValue(this.recruitment.faqLookup);
    this.isUpdateFaq = false;
    this.faqForm.reset();
    this.faqForm.get("id")?.setValue(0);
  }

  updateFaq(faq: any, index: number) {
    if (!this.faqLookup.valid) {
      this.faqLookup.markAllAsTouched();
      return
    }
    if (faq.id == 0) {
      this.recruitment.faqLookup.splice(index, 1);
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
    this.recruitment.faqLookup.splice(index, 1);
    this.faqLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetFaq(faq: any) {
    if (faq.Id > 0 && (faq.que || faq.ans || faq.queHindi || faq.ansHindi)) {
      this.recruitment.faqLookup.push(faq);
    }
    this.isUpdateFaq = false;
    this.faqForm.reset();
  }
  //#endregion

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Title',
        placeholder: 'Title',
        type: 'text',
        class: 'col-span-2',
        otherApiCall: this.recruitmentService.CheckRecruitmentTitle,
        isOtherApi: true,
        translateTo: "titleHindi"
      },

      {
        controlName: 'titleHindi',
        label: 'Title(Hindi)',
        placeholder: 'Title(Hindi)',
        type: 'text',
        class: 'col-span-2',

      },
      {
        controlName: 'tags',
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
      // {
      //   controlName: 'blockTypeCode',
      //   label: 'Block Type',
      //   placeholder: 'Select Block Type',
      //   type: 'select',
      //   isDisabled: true,
      //   selectConfig: {
      //     selectOptions: this.ddls['ddlBlockTypeByForRecruitment'],
      //   }
      // },

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
        controlName: 'salary',
        label: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? 'Fee' : 'Salary',
        placeholder: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? 'Fee' : 'Salary of this payscale',
        type: 'text',
      },

      {
        controlName: 'minAge',
        label: 'Minimum Age',
        placeholder: 'Minimum Age',
        type: 'text',
      },
      {
        controlName: 'maxAge',
        label: 'Maxmum Age',
        placeholder: 'Maxmum Age',
        type: 'text',
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
        controlName: 'startDate',
        label: 'Start Date',
        placeholder: "Start Date",
        type: 'date',
      },
      {
        controlName: 'lastDate',
        label: 'End Date',
        placeholder: "End Date",
        type: 'date',
      },
      {
        controlName: 'extendedDate',
        label: 'Extended Date',
        placeholder: 'Recruitment Extended date',
        type: 'date',
      },
      {
        controlName: 'feePaymentLastDate',
        label: 'Payment Last Date',
        placeholder: 'Last payment Date',
        type: 'date',
      },
      {
        controlName: 'correctionLastDate',
        label: 'Correction Last Date',
        placeholder: 'Last Correction Date',
        type: 'date',
      },
      {
        controlName: 'admitCardDate',
        label: 'Admit card date',
        placeholder: 'Admit card date',
        type: 'date',
      },
      {
        controlName: 'examMode',
        label: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? "Admission Type " : 'Exam Mode',
        placeholder: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? 'Choose Admission Type' : 'Choose exam mode',
        type: 'select',
        selectConfig: {
          selectOptions: ExamModeDdl,
        }
      },
      {
        controlName: 'applyLink',
        label: 'Apply Link',
        placeholder: 'https://www.xyz.com',
        type: 'text',
      },
      {
        controlName: 'officialLink',
        label: 'Official Link',
        placeholder: 'https://www.xyz.com',
        type: 'text',
      },
      {
        controlName: 'totalPost',
        label: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? 'Total No. of Seats' : 'Total Post',
        placeholder: this.recruitmentService.getRedirectMetadata(this._route.snapshot).blockTypeCode == ContentTypeLocal.ADMISSION ? 'Total no. of Seats' : 'Total Number of Post',
        type: 'text',
        numberFormat: { isNumber: true, isDecimalNumber: false, isCommaOrDashAllowed: false, isDotAllowed: false } as any
      },
      // {
      //   controlName: 'radio',
      //   label: 'Slug Options',
      //   type: 'radio',
      //   radtioOptions: [{text: 'Auto-Generate',value:1},{text: 'Custom',value:2}]
      // },
      {
        controlName: 'slugUrl',
        label: 'Url Slug',
        placeholder: 'Slug Url',
        type: 'slug',
        slugFrom: 'title',
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
          parentDdl: {
            parentControlName: "categoryId"
          },
          selectOptions: [],
          addComponent: AddUpdateSubCategoryComponent,
          addComponentClassName: "AddUpdateSubCategoryComponent",
        }
      },
      {
        controlName: 'jobDesignations',
        label: 'Job Designation',
        placeholder: 'Choose Job designation',
        type: 'select',
        selectConfig: {
          isMultiple: true,
          selectOptions: this.ddls['ddlJobDesignation'],
          addComponent: AddUpdateJobDesignationMasterComponent,
          addComponentClassName: "AddUpdateJobDesignationMasterComponent",
          ddlKey: 'ddlJobDesignation',
        }
      },
      {
        controlName: 'qualifications',
        label: 'Qualification',
        placeholder: 'Choose Job qualification',
        type: 'select',
        selectConfig: {
          isMultiple: true,
          selectOptions: this.ddls['ddlQualification'],
          addComponent: AddUpdateQualificationMasterComponent,
          addComponentClassName: "AddUpdateQualificationMasterComponent",
          ddlKey: 'ddlQualification',
        }
      },

      {
        controlName: 'shouldReminder',
        label: 'Should Reminder',
        placeholder: 'Should Reminder',
        type: 'date',
      },
      {
        controlName: 'shortDesription',
        label: 'Short Description',
        placeholder: 'Short description',
        type: 'textarea',
        class: 'col-span-2',
        translateTo: 'shortDesriptionHindi'
      },
      {
        controlName: 'shortDesriptionHindi',
        label: 'Short Description(Hindi)',
        placeholder: 'Short description(Hindi)',
        type: 'textarea',
        class: 'col-span-2',
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
        controlName: 'socialMediaUrl',
        label: 'Social Media Link',
        placeholder: 'Social Media Url',
        type: 'text',
        class: 'col-span-2',

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
        controlName: 'isPrivate',
        label: 'Is Private Job',
        placeholder: 'Is Private Job',
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
      // {
      //   controlName: 'howTo',
      //   label: 'How to',
      //   placeholder: 'How to apply',
      //   class: 'col-span-4',
      //   type: 'htmlEditor',
      // },

      {
        controlName: 'thumbnailCaption',
        label: 'Thumbnail Credit',
        placeholder: 'Thumbnail Credit',
        type: 'text',
        class: 'col-span-4',

      },
      {
        controlName: 'thumbnail',
        label: 'Upload Thumbnail',
        type: 'file',
        fileConfig: {
          accept: 'image/*',
          isMultiple: false,
          isThumbnail: true,
          showCropper: false,
        },
      },
      {
        controlName: 'attachments',
        label: 'Upload Images',
        type: 'file',
        class: 'col-span-2',
        fileConfig: {
          accept: 'image/*',
          isMultiple: true,
        },
      },
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
    ];
  }

  updateBlockTypeCode() {
    let data: any = this.recruitmentService.getRedirectMetadata(this._route.snapshot)
    this.form.get('blockTypeCode')?.setValue(data.blockTypeCode)
  }

  setOutSideFormData() {
    if (this.recruitment && this.recruitment.id) {
      this.howToApplyAndQuickLinkLookup.clear();
      this.recruitment.howToApplyAndQuickLinkLookup.forEach((element, index) => {
        this.howToApplyAndQuickLinkLookup.push(this._fb.group(this.howToOtherLinksForm.value));
        this.howToApplyAndQuickLinkLookup.controls[index].patchValue(element);
      });
      this.faqLookup.clear();
      this.recruitment.faqLookup.forEach((element, index) => {
        this.faqLookup.push(this._fb.group(this.faqForm.value));
        this.faqLookup.controls[index].patchValue(element);
      });
    }
  }

  translate(fromControl?: FormControl, toControl?: FormControl) {
    this._coreService.translateFormCtrl(fromControl, toControl)
  }
}
