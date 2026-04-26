import { AfterViewInit, Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { forkJoin, observable, Observable } from 'rxjs';
import { Breadcrumb, Ddls, EditorVal, formElement, RawFiles } from 'src/app/core/models/core.model';
import { AttachmentTypeDdl, ExamModeDdl, LevelType, Lookup, fileType } from 'src/app/core/models/fixed-value';
import { FaqLookup, SchemeAttchamentLookupModel, SchemeContentDetailsLookup, SchemeHowToApplyAndQuickLinkLookup, SchemeRequestModel } from 'src/app/core/models/scheme-model';
import { CoreService } from 'src/app/core/services/core.service';
import { SchemeService } from 'src/app/core/services/scheme.service';
import { maxCharEachLine, maxLine } from 'src/app/core/validators/form.validator';
import { AddUpdateDepartmentComponent } from 'src/app/pages/master/department-master/add-update-department/add-update-department.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-update-scheme',
  templateUrl: './add-update-scheme.component.html',
  styleUrls: ['./add-update-scheme.component.scss']
})
export class AddUpdateSchemeComponent implements OnInit, AfterViewInit {
  ddls: Ddls;
  ddlLookups: Ddls;
  form: FormGroup;
  howToOtherLinksForm: FormGroup;
  OtherLinksForm: FormGroup;
  faqForm: FormGroup;
  contentDetailsForm: FormGroup;
  schemeAttchamentForm: FormGroup;
  scheme: SchemeRequestModel;
  formElements: formElement[] = [];
  fileElements: formElement = new formElement();
  breadcrumb: Breadcrumb[] = [];
  navigationUrlOnSuccess: string;
  attachmentTypeDdl = AttachmentTypeDdl;
  isUpdateFaq: boolean = false;
  isUpdateHow2Apply: boolean = false;
  isUpdateOtherLinks: boolean = false;
  isAttachment: boolean = false;
  isContactDetail: boolean = false;
  fileBasePath = environment.fileEndPoint;
  imageFallback = environment.imageFallback;
  lookupEnum = Lookup;
  attachmentFiles: RawFiles[] = []
  attachemntFormRawFile: RawFiles | null

  get faqLookup() {
    return this.form.get('faqLookups') as FormArray
  }

  get howToApplyAndQuickLinkLookup() {
    return this.form.get('howToApplyAndQuickLinkLookup') as FormArray
  }

  get attachmentLookup() {
    return this.form.get('schemeAttachmentLookups') as FormArray
  }

  get contactDetailLookup() {
    return this.form.get('contactDetail') as FormArray
  }

  constructor(
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    public _schemeService: SchemeService,
    public _coreService: CoreService
  ) {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
    let initialData = this._route.snapshot.data['initialData'];
    this.ddls = initialData.ddls.data;
    this.ddlLookups = initialData.lookupsData.data;
    this.scheme = initialData.scheme?.data ?? new SchemeRequestModel();

    this.attachmentFiles = this.scheme.schemeAttachmentLookups.map((item) => ({ files: item.path ? [{ url: item.path } as any] : [], pendingToDelete: [], pendingToUpload: [] }))
    this.initiateFrom();
    this.initiateFormElements();
    this.setOutSideFormData();
  }

  ngAfterViewInit(): void {
    if (this.form.disabled) {
      this.isAttachment = this.isContactDetail = this.isUpdateFaq = this.isUpdateOtherLinks = this.isUpdateHow2Apply = true;
    }
  }

  ngOnInit(): void {
  }

  initiateFrom() {
    this.form = this._fb.group({
      title: [null, Validators.required],
      titleHindi: [null],
      departmentId: [null, Validators.required],
      stateId: [null, Validators.required],
      minAge: [null],
      maxAge: [null],
      startDate: [null],
      endDate: [null],
      documentIds: [[]],
      extendedDate: [null],
      correctionLastDate: [null],
      postponeDate: [null],
      levelType: [null, Validators.required],
      mode: [null, Validators.required],
      applyLink: [null],
      officelLink: [null],
      shortDescription: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      shortDescriptionHindi: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      keywords: [null],
      keywordsHindi: [null],
      description: [null],
      descriptionHindi: [null],
      eligibilityIds: [],
      slug: [{ value: null, disabled: this.scheme.publishedDate ? true : false }, Validators.required],
      fee: [null],
      thumbnail: [null],
      isCompleted: [null],
      shouldReminder: [null],
      reminderDescription: [null],
      upcomingCalendarCode: [null],
      thumbnailCredit: [null],
      socialMediaUrl: [null],
      howToApplyAndQuickLinkLookup: this._fb.array([]),
      faqLookups: this._fb.array([]),
      schemeAttachmentLookups: this._fb.array([]),
      contactDetail: this._fb.array([]),
    });
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
    this.HowToOtherLinksSet();
    this.faqSet();
    this.ContactDetailSet();
    this.SchemeAttachmentSet()

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

  ContactDetailSet = (isdefault: boolean = false) => {
    this.contentDetailsForm = this._fb.group({
      id: [0],
      departmentId: [null],
      nodalOfficerName: [null],
      nodalOfficerNameHindi: [null],
      phoneNo: [null],
      email: [null],
      isUpdate: [false]
    })
    //#region Contact Detail Validators
    this.contentDetailsForm.controls['departmentId']?.valueChanges.subscribe((res: any) => {
      if (!res) {
        this.contentDetailsForm.controls['departmentId']?.removeValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.contentDetailsForm.controls['departmentId']?.setValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.contentDetailsForm.controls['nodalOfficerName']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.contentDetailsForm.controls['departmentId']?.value) {
        this.contentDetailsForm.controls['departmentId']?.removeValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.contentDetailsForm.controls['departmentId']?.setValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.contentDetailsForm.controls['nodalOfficerNameHindi']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.contentDetailsForm.controls['departmentId']?.value) {
        this.contentDetailsForm.controls['departmentId']?.removeValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.contentDetailsForm.controls['departmentId']?.setValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.contentDetailsForm.controls['phoneNo']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.contentDetailsForm.controls['departmentId']?.value) {
        this.contentDetailsForm.controls['departmentId']?.removeValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.contentDetailsForm.controls['departmentId']?.setValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.contentDetailsForm.controls['email']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.contentDetailsForm.controls['departmentId']?.value) {
        this.contentDetailsForm.controls['departmentId']?.removeValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.contentDetailsForm.controls['departmentId']?.setValidators([Validators.required])
        this.contentDetailsForm.controls['departmentId']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    //#endregion
    if (isdefault)
      this.contactDetailLookup.push(this._fb.group(this.contentDetailsForm.value));
  }

  SchemeAttachmentSet = (isdefault: boolean = false) => {
    this.schemeAttchamentForm = this._fb.group({
      id: [0],
      title: [null],
      titleHindi: [null],
      description: [null],
      descriptionHindi: [null],
      type: [null],
      path: [null],
      isUpdate: [false]
    })
    //#region Attachment Validators
    this.schemeAttchamentForm.controls['title']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.schemeAttchamentForm.controls['type']?.value) {
        this.schemeAttchamentForm.controls['title']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.schemeAttchamentForm.controls['title']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
    })

    this.schemeAttchamentForm.controls['type']?.valueChanges.subscribe((res: any) => {
      if (!res && !this.schemeAttchamentForm.controls['title']?.value) {
        this.schemeAttchamentForm.controls['type']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['title']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.schemeAttchamentForm.controls['type']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['title']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.schemeAttchamentForm.controls['titleHindi']?.valueChanges.subscribe((res: any) => {
      if (!res && !(this.schemeAttchamentForm.controls['title']?.value || this.schemeAttchamentForm.controls['type']?.value)) {
        this.schemeAttchamentForm.controls['title']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.schemeAttchamentForm.controls['title']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
    })

    this.schemeAttchamentForm.controls['description']?.valueChanges.subscribe((res: any) => {
      if (!res && !(this.schemeAttchamentForm.controls['title']?.value || this.schemeAttchamentForm.controls['type']?.value)) {
        this.schemeAttchamentForm.controls['title']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.schemeAttchamentForm.controls['title']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    this.schemeAttchamentForm.controls['descriptionHindi']?.valueChanges.subscribe((res: any) => {
      if (!res && !(this.schemeAttchamentForm.controls['title']?.value || this.schemeAttchamentForm.controls['type']?.value)) {
        this.schemeAttchamentForm.controls['title']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.removeValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
      else {
        this.schemeAttchamentForm.controls['title']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['title']?.updateValueAndValidity({ emitEvent: false })
        this.schemeAttchamentForm.controls['type']?.setValidators([Validators.required])
        this.schemeAttchamentForm.controls['type']?.updateValueAndValidity({ emitEvent: false })
      }
    })
    //#endregion
    if (isdefault)
      this.attachmentLookup.push(this._fb.group(this.schemeAttchamentForm.value));
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

  //#region Contact
  addContactData(contact: any) {
    if (!this.contentDetailsForm.valid) {
      this.contentDetailsForm.markAllAsTouched()
      return
    }
    this.isContactDetail = false;
    if (contact.id) {
      if (this.scheme.id)
        contact.isUpdate = true;
      var index = this.scheme.contactDetail.findIndex(s => s.id == contact.id);
      this.scheme.contactDetail[index] = contact;
    }
    else {
      contact.isUpdate = false;
      this.scheme.contactDetail.push(contact);
    }
    this.contactDetailLookup.clear();
    for (let index = 0; index < this.scheme.contactDetail.length; index++) {
      this.ContactDetailSet(true);
    }
    this.contactDetailLookup.patchValue(this.scheme.contactDetail);
    this.isContactDetail = false;
    this.contentDetailsForm.reset();
    this.contentDetailsForm.get("id")?.setValue(0);
  }

  updateContact(contact: any, index: number) {
    if (!this.contentDetailsForm.valid) {
      this.contentDetailsForm.markAllAsTouched()
      return
    }
    if (contact.id == 0) {
      this.scheme.contactDetail.splice(index, 1);
      this.contactDetailLookup.removeAt(index);
    }
    else {
      this.isContactDetail = true;
      contact['isUpdate'] = false;
    }
    this.contentDetailsForm.patchValue(contact);
  }

  deleteContact(index: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.scheme.contactDetail.splice(index, 1);
    this.contactDetailLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetContact(contact: any) {
    if (contact.Id > 0 && (contact.departmentId || contact.nodalOfficerName || contact.nodalOfficerNameHindi || contact.phoneNo || contact.email)) {
      this.scheme.contactDetail.push(contact);
    }
    this.isContactDetail = false;
    this.contentDetailsForm.reset();
    this.contentDetailsForm.get("id")?.setValue(0);
  }
  //#endregion

  //#region Attachment
  addAttachmentData(attach: any) {
    debugger
    if (!this.schemeAttchamentForm.valid) {
      this.schemeAttchamentForm.markAllAsTouched()
      return
    }
    this.isAttachment = false;
    if (attach.id) {
      //udpate
      if (this.scheme.id)
        attach.isUpdate = true;
      var index = this.scheme.schemeAttachmentLookups.findIndex(s => s.id == attach.id);
      this.scheme.schemeAttachmentLookups[index] = attach;
      if (this.attachemntFormRawFile) {
        this.attachmentFiles[index] = this.attachemntFormRawFile
      }
    }
    else {
      // add
      attach.isUpdate = false;
      if (this.attachemntFormRawFile)
        this.attachmentFiles.push(this.attachemntFormRawFile)
      this.scheme.schemeAttachmentLookups.push(attach);
    }
    this.attachmentLookup.clear();
    for (let index = 0; index < this.scheme.schemeAttachmentLookups.length; index++) {
      this.SchemeAttachmentSet(true);
    }

    this.attachmentLookup.patchValue(this.scheme.schemeAttachmentLookups);
    this.isAttachment = false;
    this.schemeAttchamentForm.reset();
    this.schemeAttchamentForm.get("id")?.setValue(0);
    this.attachemntFormRawFile = null
  }

  updateAttachment(attach: any, index: number) {
    if (!this.schemeAttchamentForm.valid) {
      this.schemeAttchamentForm.markAllAsTouched()
      return
    }
    if (attach.id == 0) {
      this.scheme.schemeAttachmentLookups.splice(index, 1);
      this.attachmentLookup.removeAt(index);

    }
    else {
      this.isAttachment = true;
      attach['isUpdate'] = false;
    }
    this.schemeAttchamentForm.patchValue(attach);
    this.schemeAttchamentForm.get('path')?.setValue(this.attachmentFiles[index]?.files[0]?.url || this.attachmentFiles[index]?.pendingToUpload[0]?.url || '')

    if (attach.id == 0) {
      this.attachmentFiles.splice(index, 1);
    }
  }

  deleteAttachment(index: number, deleteBtn: NzButtonComponent) {
    deleteBtn.nzLoading = true;
    this.scheme.schemeAttachmentLookups.splice(index, 1);
    this.attachmentFiles.splice(index, 1);
    this.attachmentLookup.removeAt(index);

    deleteBtn.nzLoading = false;
  }

  resetAttachment(attach: any) {
    if (attach.Id > 0 && (attach.title || attach.titleHindi || attach.description || attach.descriptionHindi || attach.type || attach.attchPath || attach.IndexNumber)) {
      this.scheme.schemeAttachmentLookups.push(attach);
    }
    this.isAttachment = false;
    this.schemeAttchamentForm.reset();
    this.schemeAttchamentForm.get("id")?.setValue(0);
  }
  //#endregion

  //#region howToApply
  addHowToApplyData(how2Apply: any) {
    how2Apply.isQuickLink = false;
    this.isUpdateHow2Apply = false;
    if (!this.howToOtherLinksForm.valid) {
      this.howToOtherLinksForm.markAllAsTouched();
      return;
    }
    if (how2Apply.id) {
      if (this.scheme.id)
        how2Apply.isUpdate = true;
      var index = this.scheme.howToApplyAndQuickLinkLookup.findIndex(s => s.id == how2Apply.id);
      this.scheme.howToApplyAndQuickLinkLookup[index] = how2Apply;
    }
    else {
      how2Apply.isUpdate = false;
      this.scheme.howToApplyAndQuickLinkLookup.push(how2Apply);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.scheme.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.scheme.howToApplyAndQuickLinkLookup);
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
      this.scheme.howToApplyAndQuickLinkLookup.splice(index, 1);
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
    this.scheme.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetHowToApply(how2Apply: any) {
    if (how2Apply.Id > 0 && (how2Apply.title || how2Apply.titleHindi || how2Apply.description || how2Apply.descriptionHindi || how2Apply.linkUrl)) {
      this.scheme.howToApplyAndQuickLinkLookup.push(how2Apply);
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
      if (this.scheme.id)
        otherLinks.isUpdate = true;
      var index = this.scheme.howToApplyAndQuickLinkLookup.findIndex(s => s.id == otherLinks.id);
      this.scheme.howToApplyAndQuickLinkLookup[index] = otherLinks;
    }
    else {
      otherLinks.isUpdate = false;
      this.scheme.howToApplyAndQuickLinkLookup.push(otherLinks);
    }
    this.howToApplyAndQuickLinkLookup.clear();
    for (let index = 0; index < this.scheme.howToApplyAndQuickLinkLookup.length; index++) {
      this.HowToOtherLinksSet(true);
    }
    this.howToApplyAndQuickLinkLookup.patchValue(this.scheme.howToApplyAndQuickLinkLookup);

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
      this.scheme.howToApplyAndQuickLinkLookup.splice(index, 1);
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
    this.scheme.howToApplyAndQuickLinkLookup.splice(index, 1);
    this.howToApplyAndQuickLinkLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetOtherLinks(otherLinks: any) {
    if (otherLinks.Id > 0 && (otherLinks.title || otherLinks.titleHindi || otherLinks.description || otherLinks.descriptionHindi || otherLinks.linkUrl || otherLinks.iconClass)) {
      this.scheme.howToApplyAndQuickLinkLookup.push(otherLinks);
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
      if (this.scheme.id)
        faq.isUpdate = true;
      var index = this.scheme.faqLookups.findIndex(s => s.id == faq.id);
      this.scheme.faqLookups[index] = faq;
    }
    else {
      faq.isUpdate = false;
      this.scheme.faqLookups.push(faq);
    }
    this.faqLookup.clear();
    for (let index = 0; index < this.scheme.faqLookups.length; index++) {
      this.faqSet(true);
    }
    this.faqLookup.patchValue(this.scheme.faqLookups);
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
      this.scheme.faqLookups.splice(index, 1);
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
    this.scheme.faqLookups.splice(index, 1);
    this.faqLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetFaq(faq: any) {
    if (faq.Id > 0 && (faq.que || faq.ans || faq.queHindi || faq.ansHindi)) {
      this.scheme.faqLookups.push(faq);
    }
    this.isUpdateFaq = false;
    this.faqForm.reset();
    this.faqForm.get("id")?.setValue(0);
  }
  //#endregion

  initiateFormElements() {
    this.formElements = [
      {
        controlName: 'title',
        label: 'Scheme',
        placeholder: 'Scheme',
        type: 'text',
        class: 'col-span-2',
        otherApiCall: this._schemeService.CheckTitle,
        isOtherApi: true,
        translateTo: 'titleHindi'
      },
      {
        controlName: 'titleHindi',
        label: 'Scheme(Hindi)',
        placeholder: 'Scheme(Hindi)',
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
        controlName: 'levelType',
        label: 'Level',
        placeholder: 'Choose Level',
        type: 'select',
        selectConfig: {
          selectOptions: LevelType,
        }
      },
      {
        controlName: 'mode',
        label: 'Mode',
        placeholder: 'Choose Mode',
        type: 'select',
        selectConfig: {
          selectOptions: ExamModeDdl,
        }
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
        placeholder: 'Start Date',
        type: 'date',
      },
      {
        controlName: 'endDate',
        label: 'End Date',
        placeholder: "End Date",
        type: 'date',
      },
      {
        controlName: 'extendedDate',
        label: 'Extended Date',
        placeholder: 'Extended Extended date',
        type: 'date',
      },
      {
        controlName: 'correctionLastDate',
        label: 'Correction Last Date',
        placeholder: 'Last Correction Date',
        type: 'date',
      },
      {
        controlName: 'postponeDate',
        label: 'Postpone Date',
        placeholder: 'Postpone Date',
        type: 'date',
      },

      {
        controlName: 'minAge',
        label: 'Minimum age',
        placeholder: 'Minimum Age',
        type: 'text',
      },
      {
        controlName: 'maxAge',
        label: 'Maxmum age',
        placeholder: 'Maxmum Age',
        type: 'text',
      },
      {
        controlName: 'applyLink',
        label: 'Apply Link',
        placeholder: 'https://www.xyz.com',
        type: 'text',
      },
      {
        controlName: 'officelLink',
        label: 'Website Link',
        placeholder: 'https://www.xyz.com',
        type: 'text',
      },
      {
        controlName: 'slug',
        label: 'Url slug',
        placeholder: 'Slug Url',
        type: 'slug',
        slugFrom: 'title'
      },
      {
        controlName: 'fee',
        label: 'Fee',
        placeholder: 'Fee',
        type: 'text',
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
        label: 'keywords(Hindi)',
        placeholder: 'keywords(Hindi)',
        type: 'textarea',
        class: 'col-span-2',
      },
      {
        controlName: 'shouldReminder',
        label: 'Should Reminder',
        placeholder: 'Should Reminder',
        type: 'date',
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
        controlName: 'shortDescription',
        label: 'Short Description',
        placeholder: 'Short description',
        type: 'textarea',
        class: 'col-span-4',
        translateTo: 'shortDescriptionHindi'
      },
      {
        controlName: 'shortDescriptionHindi',
        label: 'Short Description(Hindi)',
        placeholder: 'Short description(Hindi)',
        type: 'textarea',
        class: 'col-span-4',
      },
      {
        controlName: 'documentIds',
        label: 'Documents',
        placeholder: 'Choose Documents',
        type: 'select',
        selectConfig: {
          isMultiple: true,
          selectOptions: this.ddlLookups[Lookup.GovtDocument.toString()],
          // addComponent: AddUpdateJobDesignationMasterComponent,
          // addComponentClassName: "AddUpdateJobDesignationMasterComponent",
          ddlKey: Lookup.GovtDocument.toString(),
        }
      },
      {
        controlName: 'eligibilityIds',
        label: 'Eligibility',
        placeholder: 'Choose Eligibility',
        type: 'select',
        selectConfig: {
          isMultiple: true,
          selectOptions: this.ddlLookups[Lookup.SchemeEligibility.toString()],
          // addComponent: AddUpdateJobDesignationMasterComponent,
          // addComponentClassName: "AddUpdateJobDesignationMasterComponent",
          ddlKey: Lookup.SchemeEligibility.toString(),
        }
      },
      {
        controlName: 'socialMediaUrl',
        label: 'Social Media Link',
        placeholder: 'Social Media Url',
        type: 'text',
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
        class: 'col-span-4',

      },
      {
        controlName: 'thumbnail',
        label: 'Upload thumbnail',
        type: 'file',
        fileConfig: {
          accept: 'image/*',
          isMultiple: false,
          isThumbnail: true,
          showCropper: false,
        },
      },

      // {
      //   controlName: 'howTo',
      //   label: 'How to',
      //   placeholder: 'How to apply',
      //   class: 'col-span-4',
      //   type: 'htmlEditor',
      // },
      // {
      //   controlName: 'docs',
      //   label: 'Upload Images',
      //   type: 'file',
      //   class: 'col-span-2',
      //   fileConfig: {
      //     accept: 'image/*',
      //     isMultiple: true,
      //   },
      // },

      // {
      //   controlName: 'notificationFile',
      //   label: 'Upload Notification',
      //   type: 'file',
      //   selectConfig: {
      //     isMultiple: false,
      //   },
      //   fileConfig: {
      //     accept: 'application/pdf',
      //   },
      // },
    ];
  }

  setFileType(typeCode: number): String {
    var typeName = this.attachmentTypeDdl.find(x => x.value == typeCode)?.text;
    if (typeName) {
      switch (typeName) {
        case 'Image':
          return fileType.IMAGE
          break;
        case 'Video':
          return fileType.VIDEO
          break;
        case 'PDF':
          return fileType.APPLICATIONPDF
          break;
        default:
          return fileType.ALL
          break;
      }
    }
    return fileType.ALL
  }



  setOutSideFormData() {
    if (this.scheme && this.scheme.id) {
      this.howToApplyAndQuickLinkLookup.clear();
      this.scheme.howToApplyAndQuickLinkLookup.forEach((element, index) => {
        this.howToApplyAndQuickLinkLookup.push(this._fb.group(this.howToOtherLinksForm.value));
        this.howToApplyAndQuickLinkLookup.controls[index].patchValue(element);
      });
      this.faqLookup.clear();
      this.scheme.faqLookups.forEach((element, index) => {
        this.faqLookup.push(this._fb.group(this.faqForm.value));
        this.faqLookup.controls[index].patchValue(element);
      });

      this.attachmentLookup.clear();
      this.scheme.schemeAttachmentLookups.forEach((element, index) => {
        this.attachmentLookup.push(this._fb.group(this.schemeAttchamentForm.value));
        this.attachmentLookup.controls[index].patchValue(element);
      });

      this.contactDetailLookup.clear();
      this.scheme.contactDetail.forEach((element, index) => {
        this.contactDetailLookup.push(this._fb.group(this.contentDetailsForm.value));
        this.contactDetailLookup.controls[index].patchValue(element);
      });
    }
  }

  getRawFiles(e: RawFiles) {
    this.attachemntFormRawFile = e
  }

  onAddUpdate = (payload: any, otherData: any) => {
    //file upload
    let uploadedFilesResultObs: any[] = []
    this.attachmentFiles.forEach(rawFile => {
      uploadedFilesResultObs.push(this._coreService.uploadRawFiles(rawFile))
    });

    return new Observable((subscriber) => {
      if (uploadedFilesResultObs && uploadedFilesResultObs.length)
        forkJoin(uploadedFilesResultObs).subscribe((uploadedFilesRes) => {
          // update path in payload
          uploadedFilesRes.forEach((path: string, index: number) => {
            payload.schemeAttachmentLookups[index].path = path
          })

          //continue add update functionality
          this._schemeService.addUpdate(payload, otherData).subscribe(resp => {
            subscriber.next(resp)
          }, err => subscriber.error(err))
        })
      else
        this._schemeService.addUpdate(payload, otherData).subscribe(resp => {
          subscriber.next(resp)
        }, err => subscriber.error(err))
    })


  }

  test() {
    debugger
  }

  translate(fromControl?: FormControl, toControl?: FormControl) {
    this._coreService.translateFormCtrl(fromControl, toControl)
  }

}
