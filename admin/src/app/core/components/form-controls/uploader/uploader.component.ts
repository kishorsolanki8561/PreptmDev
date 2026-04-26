import { Component, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { NzUploadFile } from 'ng-zorro-antd/upload';
import { Observable } from 'rxjs';
import { ApiResponseModel, RawFiles } from 'src/app/core/models/core.model';
import { FileFormat, Message } from 'src/app/core/models/fixed-value';
import { FilesWithPrev, Url } from 'src/app/core/models/FormElementsModel';
import { AlertService } from 'src/app/core/services/alert.service';
import { CoreService } from 'src/app/core/services/core.service';
import { environment } from 'src/environments/environment';
// 1.
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { DomSanitizer } from '@angular/platform-browser';
// 

const getBase64 = (file: File): Promise<string | ArrayBuffer | null> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
  });

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => UploaderComponent),
      multi: true,
    },
  ],
})
export class UploaderComponent implements OnInit {
  fileType: string[] = [];
  Validators = Validators;

  previewImg: string | undefined = '';
  previewVisible = false;

  pendingToUpload: NzUploadFile[] = [];
  pendingToDelete: string[] = [];

  fileList: NzUploadFile[] = [];
  isFormControl: boolean = true;
  public changed: (value: any) => void;
  public touched: () => void;
  public isDisabled: boolean = false;
  private rawFiles: RawFiles;
  isShowCropper: boolean = false
  @Output() valueChange: EventEmitter<string | (string | undefined)[]> = new EventEmitter();

  @Output() getRawFiles: EventEmitter<RawFiles> = new EventEmitter();

  @Input() multiple: boolean | undefined = false;
  @Input() maxFileSize!: number; //in KB
  @Input() minFileSize!: number; //in KB
  @Input() value: (string | undefined | Url)[] = []
  @Input() formControlName!: string;
  @Input() label!: string;
  @Input() IsDisabled: boolean = false;;
  @Input() form!: FormGroup | AbstractControl;
  @Input() isThumbnail = false
  @Input() showCropper = false
  @Input() croppingRatio = 1.91 / 1

  @Input() set accept(types: string) {

    this._accept = types;
    const typesArr = types.split(',');
    this.fileType = [];
    typesArr.forEach((type: string) => {
      switch (type) {
        case 'image/*':
          this.fileType = [...this.fileType, ...FileFormat.imageFormat];
          break;
        case 'application/pdf':
          this.fileType = [...this.fileType, ...FileFormat.pdfFormat];
          break;
        case 'video/*':
          this.fileType = [...this.fileType, ...FileFormat.videoFormat];
          break;
      }
    });
  }
  _accept: string = '';
  get accept() {
    return this._accept;
  }
  get formField() {
    return this.form?.get(this.formControlName) as FormControl;
  }

  //  //2.
  imageFile: any = '';
  croppedImage: ImageCroppedEvent;
  // //

  constructor(
    private _alertService: AlertService,
    private _coreService: CoreService,
    private sanitizer: DomSanitizer
  ) { }




  ngOnInit(): void {
    this.isDisabled = this.IsDisabled;
    if (!this.form && !this.formControlName) this.isFormControl = false;
    this.updateFileList();
  }


  emitRawFiles() {
    this.rawFiles = {
      pendingToUpload: [...this.pendingToUpload],
      pendingToDelete: [...this.pendingToDelete],
      files: [...this.fileList]
    }
    this.getRawFiles.emit(this.rawFiles)
  }


  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event;
  }


  cropImage() {
    getBase64(this.croppedImage.blob as any).then((url: any) => {
      if (this.croppedImage.blob) {
        let file: any = new File([this.croppedImage.blob], this.imageFile.name);
        file['url'] = url
        file['uid'] = this.fileList.length;
        // this.croppedImage.url = url
        // this.croppedImage.uid = this.fileList.length;
        this.fileList = this.fileList.concat(file as any);
        this.isShowCropper = false
        this.pendingToUpload.push(file as any)
        this.emitRawFiles()
      }
    })
  }

  beforeUpload = (file: any): boolean => {
    let fileExt: any = (file.name.split('.') as any).at(-1);
    if (this.isFileValid(file)) {

      if (file.type.includes('image') && this.showCropper) {
        // image is uploaded
        this.imageFile = file;
        //open cropper dialog
        this.isShowCropper = true
        return false
      }

      getBase64(file).then((url: any) => {
        file.url = url
        this.fileList = this.fileList.concat(file);

        if (this.pendingToUpload.length) {
          let files = this.pendingToUpload.map(file => file.url)
          this.updateVal(this.multiple ? files : (files[0] || ''))
        }

      })

      this.pendingToUpload.push(file)
      this.emitRawFiles()
    }

    return false;
  };

  handlePreview = async (file: NzUploadFile): Promise<void> => {
    if (!file.url && !file['preview']) {
      file['preview'] = await getBase64(file.originFileObj!);
    }

    this.previewImg = (file.url || file['preview']);
    // handle image

    //open pdf in browser popup
    if (this.previewImg?.includes('application/pdf') || this.previewImg?.includes('.pdf')) {
      let w = window.open('about:blank', '_blank', 'popup=true');
      setTimeout(() => { //FireFox seems to require a setTimeout for this to work.
        if (w && this.previewImg) {
          w.document.body.appendChild(w.document.createElement('iframe')).src = this.previewImg;
          w.document.getElementsByTagName("iframe")[0].style.width = '100%';
          w.document.getElementsByTagName("iframe")[0].style.height = '100%';
          w.document.getElementsByTagName("body")[0].style.margin = '0px';
        }
      }, 0);
      return
    }

    // show image preview
    if (this.previewImg?.includes('image/') ||
      this.previewImg?.includes('.png') ||
      this.previewImg?.includes('.jpg') ||
      this.previewImg?.includes('.jpeg') ||
      this.previewImg?.includes('.gif') ||
      this.previewImg?.includes('.webp')
    ) {
      this.previewVisible = true;
      return
    }

    // download file
    this._coreService.downloadFile(this.previewImg)

  };



  isFileValid = (file: File): boolean => {
    let fileSize = file.size / 1024;
    let fileExt: any = (file.name.split('.') as any).at(-1);
    //check max file size
    if (this.maxFileSize && this.maxFileSize < fileSize) {
      this._alertService.error(
        `Size of ${file.name} must be less then ${this.maxFileSize}KB`,
      );
      return false;
    }
    //check min file size
    if (this.minFileSize && this.minFileSize > fileSize) {
      this._alertService.error(
        `Size of ${file.name} must be grater then ${this.minFileSize}KB`,
      );
      return false;
    }
    //check file extension
    if (this.fileType.length && !this.fileType.includes(fileExt)) {
      this._alertService.error(
        `Extension of ${file.name} must be ${this.fileType.join(', ')}`,
      );
      return false;
    }
    //check file already exist
    const fileIndex = this.fileList.findIndex((fileObj: any) => fileObj.name === file.name);
    if (fileIndex >= 0) {
      this._alertService.error(`File ${file.name} already exist.`);
      return false;
    }
    return true;
  }

  // updateValue() {
  //   
  //   this.touched();
  //   this.value = new FilesWithPrev();
  //   this.fileList.forEach((file: any) => {
  //     let fileType = Object.prototype.toString.call(file) // getting file type (binary file or link)
  //     if (fileType == '[object File]') {
  //       this.value.files.push(file)
  //     } else if (fileType == '[object Object]') {
  //       this.value.urls.push({ id: file.uid, path: file.url.split(environment.fileEndPoint)[1] })
  //     }
  //   })
  //   this.valueChange.emit(this.value);
  //   if (this.isFormControl)
  //     this.changed(this.value);
  // }

  submit() {
    this.touched();
    this.rawFiles.pendingToUpload.forEach((item) => {
      delete item.url
    })
    return this._coreService.uploadRawFiles(this.rawFiles, (val: any) => this.updateVal(val), this.multiple, this.isThumbnail)

  }

  updateVal(val: string | (string | undefined)[]) {
    this.valueChange.emit(val);
    if (this.isFormControl)
      this.changed(val);
  }

  //executes angular change detection,
  //telling angular about value is changed,
  //so angular will reflect in formGroup
  public registerOnChange(fn: any): void {
    this.changed = fn;
  }

  // when control blured - for validation error
  //telling angular about touched,
  //so angular will reflect in formGroup
  public registerOnTouched(fn: any): void {
    this.touched = fn;
  }

  // angular says that value is changed from outside
  public setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  // angular says that value is changed from outside
  public writeValue(value: string[]): void {
    if (!value) {
      this.value = []
    } else {
      this.value = value;
    }
    this.updateFileList();
  }

  onFileDelete = (file: NzUploadFile) => {
    return new Observable<boolean>((observer) => {
      this._alertService.Question(Message.confirm.delete).then((isConfirmed) => {
        if (isConfirmed && file.url) {

          let i = this.pendingToUpload.findIndex((item) => item.uid == file.uid)
          if (i > -1) {
            this.pendingToUpload.splice(i, 1)
          } else if (!file.url.includes('http')) {
            this.pendingToDelete.push(file.url.replace(environment.fileEndPoint, ''))
          }
          observer.next(true);
          observer.complete();
          this.emitRawFiles()
        } else {
          observer.next(false);
          observer.complete();
        }

      })

    })
  }

  updateFileList() {
    this.fileList = [];
    if (this.value && this.value.length) {
      if (typeof this.value == 'string') {
        this.value = [this.value]
      }
      this.value.forEach((url: any, index) => {
        if (url) {
          let isUrlString = typeof url == 'string'
          let fileObj = { uid: isUrlString ? index.toString() : url.id.toString(), name: isUrlString ? '0' : url.id.toString(), url: (((isUrlString ? url : url.path).includes('base64') || url.includes('http')) ? '' : environment.fileEndPoint) + (isUrlString ? url : url.path) }
          this.fileList.push(fileObj)
        }
      })
    }
    this.emitRawFiles()
  }

  public hasRequiredField = (abstractControl: AbstractControl): boolean => {
    if (abstractControl.validator) {
      const validator = abstractControl.validator({} as AbstractControl);
      if (validator && validator['FileRequired']) {
        return true;
      }
    }
    return false;
  };

  touchedFlag = false;
  isTouched() {
    if (!this.touchedFlag) {
      this.touchedFlag = true;
    } else if (this.isFormControl) {
      this.touched();
    }
  }

  // popOver
  urlVisible: boolean = false;


  onSeturl(url: string) {
    let fileExt: any = (url.split('.') as any).at(-1);
    //check file extension
    if (this.fileType.length && !this.fileType.includes(fileExt)) {
      this._alertService.error(
        `File Extension must be ${this.fileType.join(', ')}`,
      );
      return;
    }

    let filePath: any = {}
    filePath['url'] = url.trim()
    filePath['uid'] = this.fileList.length.toString();
    filePath['isImageUrl'] = true;

    this.fileList = this.fileList.concat(filePath);

    this.urlVisible = false
    this.emitRawFiles()
  }



}

