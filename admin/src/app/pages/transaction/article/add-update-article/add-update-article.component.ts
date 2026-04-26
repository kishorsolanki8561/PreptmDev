import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { ArticleResponseDTO } from 'src/app/core/models/article.model';
import { Breadcrumb, Ddls, formElement } from 'src/app/core/models/core.model';
import { Lookup } from 'src/app/core/models/fixed-value';
import { ArticleService } from 'src/app/core/services/article.service';
import { CoreService } from 'src/app/core/services/core.service';
import { maxCharEachLine, maxLine } from 'src/app/core/validators/form.validator';
import { AddUpdateGroupMasterComponent } from 'src/app/pages/master/group-master/add-update-group-master/add-update-group-master.component';

@Component({
  selector: 'app-add-update-article',
  templateUrl: './add-update-article.component.html',
  styleUrls: ['./add-update-article.component.scss']
})
export class AddUpdateArticleComponent implements OnInit, AfterViewInit {
  form: FormGroup;
  formElements: formElement[] = [];
  breadcrumb: Breadcrumb[] = [];
  _article: ArticleResponseDTO = new ArticleResponseDTO();
  ddls: Ddls;
  ddlLookups: Ddls;
  faqForm: FormGroup;
  isUpdateFaq: boolean = false;
  get faqLookup() {
    return this.form.get('articleFaqsDTOs') as FormArray
  }

  constructor(
    private _route: ActivatedRoute,
    public _articleServervice: ArticleService,
    private _fb: FormBuilder,
    private _coreService: CoreService

  ) {
    let initialData = this._route.snapshot.data['initialData'];
    this.ddlLookups = initialData.Lookup?.data;
    this._article = initialData.article?.data ?? new ArticleResponseDTO();
    this.ddls = initialData.ddls?.data;
    this.initiateFrom();
    this.initiateFormElements();
    this.setOutSideFormData();
  }

  initiateFrom() {
    this.form = this._fb.group({
      id: [null],
      title: [null, Validators.required],
      titleHindi: [null],
      articleType: [null, Validators.required],
      slugUrl: [{ value: '', disabled: this._article.publisherDate ? true : false }, Validators.required],
      keywords: [null],
      keywordHindi: [null],
      description: [null],
      descriptionHindi: [null],
      summary: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      summaryHindi: [null, [maxLine(4), maxCharEachLine(200), Validators.required]],
      thumbnail: [null],
      thumbnailCredit: [null],
      articleFaqsDTOs: this._fb.array([]),
      articleTagsDTOs: [[]],
    });
    this.faqSet();
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
        placeholder: 'Title',
        type: 'text',
        class: 'col-span-2',
        otherApiCall: this._articleServervice.CheckTitle,
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
        controlName: 'articleType',
        label: 'Article Type',
        placeholder: 'Article Type',
        type: 'select',
        selectConfig: {
          selectOptions: this.ddlLookups['article-type'],
          ddlKey: 'article-type',
        }
      },
      {
        controlName: 'articleTagsDTOs',
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
        class: 'col-span-2',
        isDisabled: this._article.publisherDate ? true : false
      },
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
        translateTo: 'keywordHindi'
      },
      {
        controlName: 'keywordHindi',
        label: 'Keywords(Hindi)',
        placeholder: 'Keywords(Hindi)',
        type: 'text',
        class: 'col-span-2',

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
          showCropper: false
        },
      },
    ];
  }

  //#region FAQ

  addFaqData(faq: any) {
    if (!this.faqLookup.valid) {
      this.faqLookup.markAllAsTouched();
      return;
    }
    this.isUpdateFaq = false;
    if (faq.id) {
      if (this._article.id)
        faq.isUpdate = true;
      var index = this._article.articleFaqsDTOs.findIndex(s => s.id == faq.id);
      this._article.articleFaqsDTOs[index] = faq;
    }
    else {
      faq.isUpdate = false;
      this._article.articleFaqsDTOs.push(faq);
    }
    this.faqLookup.clear();
    for (let index = 0; index < this._article.articleFaqsDTOs.length; index++) {
      this.faqSet(true);
    }
    this.faqLookup.patchValue(this._article.articleFaqsDTOs);
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
      this._article.articleFaqsDTOs.splice(index, 1);
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
    this._article.articleFaqsDTOs.splice(index, 1);
    this.faqLookup.removeAt(index);
    deleteBtn.nzLoading = false;
  }

  resetFaq(faq: any) {
    if (faq.Id > 0 && (faq.que || faq.ans || faq.queHindi || faq.ansHindi)) {
      this._article.articleFaqsDTOs.push(faq);
    }
    this.isUpdateFaq = false;
    this.faqForm.reset();
    this.faqForm.get("id")?.setValue(0);
  }
  //#endregion

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }

  ngAfterViewInit(): void {
    if (this.form.disabled) {
      this.isUpdateFaq = true;
    }
  }

  setOutSideFormData() {
    if (this._article && this._article.id && this._article.articleFaqsDTOs) {
      this.faqLookup.clear();
      this._article.articleFaqsDTOs.forEach((element, index) => {
        this.faqLookup.push(this._fb.group(this.faqForm.value));
        this.faqLookup.controls[index].patchValue(element);
      });
    }
  }
  translate(fromControl?: FormControl, toControl?: FormControl) {
    this._coreService.translateFormCtrl(fromControl, toControl)
  }
}
