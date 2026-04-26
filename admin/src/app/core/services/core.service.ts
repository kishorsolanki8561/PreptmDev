import { Injectable } from '@angular/core';
import { Location } from '@angular/common';
import { BaseService } from './base.service';
import { EndPoints } from '../api';
import { ApiResponseModel, DdlItem, Obj, RawFiles } from '../models/core.model';
import { ActivatedRoute, ActivatedRouteSnapshot, Params, Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { forkJoin, Observable, of } from 'rxjs';
import { FormControl } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})

export class CoreService {
  public isUpdateFilter: boolean = false;
  public enableTranslate: boolean = this.getLocalStorage('enableTranslator') || false;
  constructor(
    private _location: Location,
    private _baseService: BaseService,
    private _route: Router,
    private _http: HttpClient
  ) {
  }

  NumberOnly(
    event: any,
    isDotAllowed: boolean = false,
    isCommaOrDash: boolean = false
  ): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (isDotAllowed) {
      if (charCode == 46) {
        //Check if the text already contains the . character
        if (event.target.value.indexOf('.') === -1) {
          return true;
        } else {
          return false;
        }
      }
    }
    if (isCommaOrDash) {
      if (charCode == 44 || charCode == 45) {
        return true;
      }
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  checkDecimalNumberOnly(event: any): boolean {
    var charCode = event.which ? event.which : event.keyCode;
    if (charCode == 46) {
      //Check if the text already contains the . character
      if (event.target.value.indexOf('.') === -1) {
        return true;
      } else {
        return false;
      }
    } else {
      if (
        event.target.value.split('.').length > 1 &&
        event.target.value.split('.')[1].length > 1
      ) {
        return false;
      } else if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    }
    return true;
  }

  AlphaNumericOnly(e: any) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[A-Za-z0-9]+$/;
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (!isValid) {
      //alert("Only Alphabets and Numbers are allowed.");
    }
    return isValid;
  }

  AlphabetOnly(e: any) {
    var keyCode = e.keyCode || e.which;
    var regex = /^[a-zA-Z& ]*$/;
    var isValid = regex.test(String.fromCharCode(keyCode));
    if (!isValid) {
      //alert("Only Alphabets and Numbers are allowed.");
    }
    return isValid;
  }

  goBack() {
    this._location.back();
  }

  fileToBase64(file: File) {
    let fileReader = new FileReader();
    let base64;
    fileReader.onload = function (fileLoadedEvent) {
      base64 = fileLoadedEvent.target?.result;
    };
    // Convert data to base64
    fileReader.readAsDataURL(file);
    return base64;
  }



  setLocalStorage(key: string, value: any) {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getLocalStorage(key: string) {
    return JSON.parse(localStorage.getItem(key) + '');
  }

  getDdls(ddls: string) {
    return this._baseService.get<Obj<DdlItem[]>>(EndPoints.ddl.getAllDdl, { keys: ddls })
  }


  fnGetDdls = (ddls: string) => {
    return this._baseService.get<Obj<DdlItem[]>>(EndPoints.ddl.getAllDdl, { keys: ddls })
  }

  uploadFiles = (files: any[], isThumbnail: boolean = false) => {
    let formdata = new FormData()
    files.forEach((file) => {
      formdata.append('file', file)
    })
    return this._baseService.post<string[]>(`${EndPoints.file.uploadFile}?isThumbnail=${isThumbnail}`, formdata)
  }
  translate = (text: string) => {
    return this._baseService.getExternal<any>(`http://localhost:3000/api/v1/en/hi/${text}`)
  }

  deleteFiles = (filePaths: string[]) => {
    return this._baseService.post<string[]>(`${EndPoints.file.deleteFile}`, filePaths)
  }



  GetDDLLookupDataByLookupTypeIdAndLookupType(SlugUrl: string = '', LookupType: string = '', LookupTypeId: string = '') {
    return this._baseService.get<Obj<DdlItem[]>>(EndPoints.ddl.GetDDLLookupDataByLookupTypeIdAndLookupTypeUrl, { SlugUrl: SlugUrl, LookupType: LookupType, LookupTypeId: LookupTypeId })
  }
  getRecordIndex(index: number, pageSize: number, curPage: number) {
    return (curPage - 1) * pageSize + (index + 1);
  }

  groupListByKey(rawList: any[], key: any) {
    let newList: any = {}
    rawList.forEach((item) => {
      if (newList[item[key]]) {
        newList[item[key]].push(item)
      } else {
        newList[item[key]] = []
        newList[item[key]].push(item)
      }
    })
    return Object.values(newList)
  }

  getSubCategoryDdlById = (id: number) => {
    return this._baseService.get(EndPoints.SubCategoryMaster.getSubCategoryByCategoryIdurl, { cateCode: id })
  }

  convertToSlug(slug: string): string {
    //replace all special characters | symbols with a space
    slug = slug
      .replace(/[`~!@#$%^&*()_\-+=\[\]{};:'"\\|\/,.<>?\s]/g, ' ')
      .toLowerCase();

    // trim spaces at start and end of string
    slug = slug.replace(/^\s+|\s+$/gm, '-');

    // replace space with dash/hyphen
    slug = slug.replace(/\s+/g, '-');

    return slug;
  }

  downloadFile(base64: any, fileName: string = '') {
    const src = `${base64}`;
    const link = document.createElement("a")
    link.href = src
    link.target = "_blank";
    link.download = fileName
    link.click()

    link.remove()
  }

  b64toBlob = (b64Data: string, contentType = '') => {
    contentType = contentType || '';
    const sliceSize = 1024;
    const byteCharacters = window.atob("bXlQYXNzd29yZCE=");
    // const byteCharacters = Buffer.from(b64Data).toString('binary');
    const bytesLength = byteCharacters.length;
    const slicesCount = Math.ceil(bytesLength / sliceSize);
    const byteArrays = new Array(slicesCount);

    for (let sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
      const begin = sliceIndex * sliceSize;
      const end = Math.min(begin + sliceSize, bytesLength);

      const bytes = new Array(end - begin);
      for (let offset = begin, i = 0; offset < end; ++i, ++offset) {
        bytes[i] = byteCharacters[offset].charCodeAt(0);
      }
      byteArrays[sliceIndex] = new Uint8Array(bytes);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    const blobUrl = URL.createObjectURL(blob);
    return blob;
  }

  setfilter(filter: any, url: string): any {
    let oldUrl = this.getLocalStorage("pageUrl");
    this.setLocalStorage("pageUrl", url ?? '')

    if (this.isUpdateFilter)
      this.setLocalStorage("filter", filter);
    else if (!this.isUpdateFilter && url != oldUrl)
      this.setLocalStorage("filter", filter);

    // only use blocktpye 
    else if ((this.getLocalStorage("filter")['blockTypeCode'] && filter['blockTypeCode']) && this.getLocalStorage("filter")['blockTypeCode'] != filter['blockTypeCode']) {
      localStorage.removeItem('pageUrl');
      localStorage.removeItem('filter');
    }

    return url != oldUrl ? (filter) : (this.getLocalStorage("filter") || filter);
  }
  copyTextToClipboard(text: string) {
    navigator.clipboard.writeText(text)
  }

  uploadRawFiles(files: RawFiles, updateValCB?: Function, isMultiple = false, isThumbnail = false) {
    // //upload files
    let fakeResponse = new ApiResponseModel<[]>();
    fakeResponse.data = []
    fakeResponse.isSuccess = true
    fakeResponse.message = ''
    fakeResponse.statusCode = 200
    fakeResponse.totalRecords = 0

    let fileUploadObs = files.pendingToUpload?.length ? this.uploadFiles(files.pendingToUpload, isThumbnail) : of(fakeResponse)


    let fileDeleteObs = files.pendingToDelete?.length ? this.deleteFiles(files.pendingToDelete) : of(fakeResponse)

    let alreadyUploadedFiles = files.files.filter((file) => {
      return Object.prototype.toString.call(file) == '[object Object]'
    }).map((file) => {
      return file.url?.replace(environment.fileEndPoint, '')
    })


    return new Observable((subscriber) => {
      forkJoin({
        upload: fileUploadObs,
        delete: fileDeleteObs
      }).subscribe((val) => {
        if (files.pendingToUpload.length) {
          //uploaded files
          if (val.upload.isSuccess) {
            let result = [...alreadyUploadedFiles, ...val.upload.data]
            let processedResult = isMultiple ? result : (result[0] || '')
            if (updateValCB) {
              updateValCB(processedResult)
            }
            subscriber.next(processedResult)
            subscriber.complete();
          } else {
            subscriber.error(val.upload.message)
            alert(val.upload.message)
          }
        } else if (files.pendingToDelete.length) {
          // deleted files
          if (val.delete.isSuccess) {
            let result = [...alreadyUploadedFiles]
            let processedResult = isMultiple ? result : (result[0] || '')
            if (updateValCB) {
              updateValCB(processedResult)
            }
            subscriber.next(processedResult)
            subscriber.complete();
          } else {
            subscriber.error(val.delete.message)
            alert(val.delete.message)
          }
        } else {
          // already available files
          let result = [...alreadyUploadedFiles]
          let processedResult = isMultiple ? result : (result[0] || '')
          if (updateValCB) {
            updateValCB(processedResult)
          }
          subscriber.next(processedResult)
          subscriber.complete();
        }
      }, (err) => {
        subscriber.error(err)
      })
    })
  }
  changeTranslatorStatus(status: boolean) {
    this.enableTranslate = status;
    this.setLocalStorage('enableTranslator', status)
  }
  translatorStaus() {
    return this.enableTranslate;
  }

  translateFormCtrl(fromControl?: FormControl, toControl?: FormControl) {
    if (this.enableTranslate) {
      if (fromControl?.value) {
        this.translate(fromControl.value).subscribe((val) => {
          toControl?.setValue(val.translation)
        })
      } else {
        toControl?.setValue(null)
      }
    }
  }

}

