import { AfterViewInit, Component, ComponentFactoryResolver, ElementRef, EventEmitter, Input, OnInit, Output, QueryList, ViewChild, ViewChildren, ViewContainerRef } from '@angular/core';
import { FormArray, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiResponseModel, formElement } from '../../models/core.model';
import { ActionTypes, MaxFileSize, Message, MinFileSize } from '../../models/fixed-value';
import { AlertService } from '../../services/alert.service';
import { CoreService } from '../../services/core.service';
import { AuthService } from '../../services/auth.service';
import { debounceTime, forkJoin, of, switchMap } from 'rxjs';
import { UploaderComponent } from '../form-controls/uploader/uploader.component';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit, AfterViewInit {
  isLoading = false;
  id: number;
  MaxFileSize = MaxFileSize;
  MinFileSize = MinFileSize;
  viewOnly = false;
  @ViewChild('dialogContent', { read: ViewContainerRef }) dialogContent: any
  @ViewChildren('fileUploadRef', { read: UploaderComponent }) filesUploadRef: QueryList<UploaderComponent>

  shouldShowModel = false;
  dialogTitle: string = ''
  searchDataModel: any[] = [];

  dialogFormElement: formElement;

  @Input() formElements: formElement[] = [];
  @Input() form: FormGroup;
  @Input() inlineForm: boolean = false;
  @Input() addUpdateFn$: any;
  // @Input() addFormArrayFn: any;
  @Input() formArrayConfig: { arrayName: string, addFormArrayFn: any }[] = [];
  // @Input() arrayName: string = ''
  @Input() initialData: any;
  @Input() isitialPayload: boolean = false;
  @Input() navigationUrlOnSuccess: string = '';
  @Input() isMultiselect: boolean = false;

  @Input() dialogMode: boolean = false;
  @Output('dialogClosed') dialogClosed = new EventEmitter<boolean>();

  permissions = this._authService.getPermissions();
  positivePermissions: string[] = []

  constructor(
    private _route: ActivatedRoute,
    private _alertService: AlertService,
    private _router: Router,
    private _coreService: CoreService,
    private _authService: AuthService
  ) {
    this.viewOnly = this._route.snapshot.data['pageAction'] == ActionTypes.VIEW_DETAILS;
  }
  ngAfterViewInit(): void {
    let element = [...this.formElements.filter(s => s.isOtherApi == true)];
    for (let index = 0; index < element.length; index++) {
      const formControl = element[index];
      if (formControl.type === 'text' && formControl.isOtherApi) {
        let control = this.form.get(formControl.controlName) as FormControl;
        if (control) {
          control.valueChanges.pipe(
            debounceTime(500),
            switchMap((query: any) => {
              if (query) {
                {
                  formControl.otherApiCall(query).subscribe((res: ApiResponseModel<any>) => {
                    if (res.isSuccess) {
                      this.searchDataModel = res.data;
                    }
                  })
                }
              }
              else {
                this.searchDataModel = [];
              };

              return of(query);
            })
          ).subscribe();
        }
      }

    }
  }

  applyPermissions() {
    this.positivePermissions = []
    this.formElements.forEach((element) => {
      if (element.selectConfig?.addComponent) {
        if (this.permissions[element.selectConfig.addComponentClassName as string]?.includes(ActionTypes.ADD)) {
          this.positivePermissions.push(element.controlName)
        }
      }
    })
  }





  ngOnInit(): void {
    this.applyPermissions();
    this.id = this.dialogMode ? 0 : (this._route.snapshot.params['id'] | 0);
    if (this.viewOnly) {
      this.readOnlyMode();
    }

    if (this.initialData) {
      this.patchFormValue(this.initialData);
      if (this.viewOnly)
        this.form.disable();
    }

  }

  patchFormValue(data: any) {
    this.formArrayConfig.forEach(element => {
      if (element.arrayName && data[element.arrayName]) {
        //clear already added form elements if any
        (this.form.get(element.arrayName) as FormArray).clear();

        // creating form groups for formArray
        data[element.arrayName].forEach((obj: any) => {
          if (typeof obj === 'object') {
            element.addFormArrayFn();
          }
        });
        // add value if there is no value in form array
        if (!data[element.arrayName].length) {
          element.addFormArrayFn();

        }
      }
    })



    this.form.patchValue(data);
  }

  readOnlyMode() {
    this.form.disable();
  }

  onReset() {
    if (this.initialData)
      this.patchFormValue(this.initialData)
    else
      this.form.reset();
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this._alertService.error(Message.formError.invalidForm)
      return;
    }
    this.form.disable();
    this.isLoading = true;
    let payload = this.isitialPayload ? this.initialData : { ...this.form.getRawValue() }
    if (!this.inlineForm) {
      payload['id'] = this.id || 0;
    }

    let fileUploadRefObs: any = []
    this.filesUploadRef.forEach((fileUpload) => {
      fileUploadRefObs.push(fileUpload.submit())
    })

    let allFileUploadRef$ = forkJoin(fileUploadRefObs)

    if (fileUploadRefObs && fileUploadRefObs.length) {
      allFileUploadRef$.subscribe((val) => {

        this.form.disable();
        this.isLoading = true;
        let payload = { ...this.form.getRawValue() }
        if (!this.inlineForm) {
          payload['id'] = this.id || 0;
        }
        this.addupdate(payload);
      }, (err) => {
        this.isLoading = false;
        this.form.enable()
      })
    }
    else {
      this.addupdate(payload);
    }


  }

  addupdate(payload: any) {

    if (this.inlineForm) {
      payload['id'] =  payload['id'] || 0
      this.addUpdateFn$(payload)
      return
    }

    this.addUpdateFn$(payload, { id: this.id }).subscribe((res: ApiResponseModel<any>) => {

      this.isLoading = false;
      if (res.isSuccess) {
        if (this.dialogMode) {
          //emit true
          this.dialogClosed.emit(true)
          this._alertService.success(res.message)
          return;
        }
        this._alertService.success(res.message)
        this._router.navigate([this.navigationUrlOnSuccess])
      }
      this.form.enable()
    }, () => {
      this.isLoading = false;
      this.form.enable()
    })
  }
  getChildDdl(value: any, selectConfig: any, isOutsiteCalled: boolean = false) {
    if (selectConfig?.childDdl) {
      let childDdlName = selectConfig?.childDdl?.childControlName
      const index = this.formElements.findIndex(item => item.controlName === childDdlName)

      if (!value) {
        this.form.get(selectConfig?.childDdl.childControlName)?.setValue(null);
        (this.formElements[index] as any).selectConfig.selectOptions = [];
        return
      }
      if (!isOutsiteCalled) {
        // this will not called when parentDdl value changed programatically (outsite)
        this.form.get(selectConfig?.childDdl.childControlName)?.setValue(null)
      }
      selectConfig?.childDdl.DdlMethodRef(value).subscribe((response: any) => {
        if (response.isSuccess) {
          if (index != -1) {
            (this.formElements[index] as any).selectConfig.selectOptions = response.data
          }
        } else {
          if (index != -1) {
            (this.formElements[index] as any).selectConfig.selectOptions = []
          }
        }
      })
    }
  }

  generateSlug(slugFromName?: string, slugToName?: string) {
    if (slugFromName && slugToName) {
      let fromCtrl = this.form.get(slugFromName)
      let toCtrl = this.form.get(slugToName)
      if (fromCtrl && toCtrl) {
        if (fromCtrl?.value) {
          toCtrl?.setValue(this._coreService.convertToSlug(fromCtrl?.value))
        } else {
          alert(`please provide value in "${slugFromName}"`)
        }
      } else {
        console.error("controls not found");
      }
    } else {
      console.error("please pass 'slugFromName' & 'slugToName'")
    }




    // if (isConvertToSlug && colConvertToSlug) {
    //   if(!this.form.get(colConvertToSlug)?.value){
    //     let slug = this._coreService.convertToSlug(formControl.value)
    //     this.form.get(colConvertToSlug)?.setValue(slug);
    //   }
    //   // this.form.get(colConvertToSlug)?.enable()
    //   return;
    // }
    // if (elementType === 'slug') {
    //   let slug = this._coreService.convertToSlug(formControl.value)
    //   formControl.setValue(slug);
    // }
  }

  focusOut(curFormContorl: any, curFormElement: formElement) {
    if (this._coreService.enableTranslate && curFormElement.translateTo) {
      if (curFormContorl.value) {
        this._coreService.translate(curFormContorl.value).subscribe((val) => {
          this.form.get(curFormElement.translateTo || '')?.setValue(val.translation)
        })
      } else {
        // remove hindi value
        this.form.get(curFormElement.translateTo || '')?.setValue(null)
      }
    }

  }

  openDialog(formElement: formElement) {
    this.dialogFormElement = formElement
    this.dialogContent.clear();
    let compRef = this.dialogContent.createComponent(formElement.selectConfig?.addComponent);
    compRef.instance.dialogMode = true;
    compRef.instance.dialogClosed.subscribe((isCreated: boolean) => {
      this.closeDialog(isCreated);
    })
    this.dialogTitle = formElement.label;
    this.shouldShowModel = true;
  }

  closeDialog(isCreated: boolean) {
    if (isCreated) {

      if (this.dialogFormElement.selectConfig?.parentDdl?.parentControlName) {

        // finding parent control
        let parentCtrlIndex = this.formElements.findIndex((element) => element.type === 'select' && element.controlName === this.dialogFormElement.selectConfig?.parentDdl?.parentControlName)
        if (parentCtrlIndex == -1) {
          console.error("parent control not found")
        } else {
          this.formElements[parentCtrlIndex].selectConfig?.childDdl?.DdlMethodRef(this.form.get(this.formElements[parentCtrlIndex].controlName)?.value).subscribe((res: any) => {
            if (res.isSuccess) {
              // auto select newally added option
              let difference = res.data.filter((x: any) => !(this.dialogFormElement.selectConfig as any).selectOptions.map((x: any) => x.value).includes(x.value));

              this.form.get(this.dialogFormElement.controlName)?.setValue(difference[0].value);

              // update dropdown
              (this.dialogFormElement.selectConfig as any).selectOptions = res.data
            }
          })
        }
      } else {

        //refresh dropdown
        this._coreService.getDdls(this.dialogFormElement.selectConfig?.ddlKey as string).subscribe((res) => {
          if (res.isSuccess) {
            // auto select newally added option
            let difference = res.data[this.dialogFormElement.selectConfig?.ddlKey as string]
              .filter(x => !(this.dialogFormElement.selectConfig as any).selectOptions.map((x: any) => x.value).includes(x.value));

            if (this.dialogFormElement.selectConfig?.isMultiple) {

              this.form.get(this.dialogFormElement.controlName)?.setValue(
                [difference[0].value, ...(this.form.get(this.dialogFormElement.controlName)?.value || [])]
              )
            } else {
              this.form.get(this.dialogFormElement.controlName)?.setValue(difference[0].value);
            }

            // update dropdown
            (this.dialogFormElement.selectConfig as any).selectOptions = res.data[this.dialogFormElement.selectConfig?.ddlKey as string]
          }
        });

      }



    }
    // this.shouldShowModel = false;
  }

}
