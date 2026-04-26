import { NzUploadFile } from 'ng-zorro-antd/upload';
import { DefaultTableLength } from './fixed-value';
import { FormGroup } from '@angular/forms';

export class ApiResponseModel<T> {
  isSuccess: boolean;
  statusCode: number;
  message: string;
  data: T;
  totalRecords: number;
}

export class RawFiles {
  pendingToUpload: NzUploadFile[]
  pendingToDelete: string[]
  files: NzUploadFile[]
}


export class PaginationData {
  currentPage: number;
  pageSize: number;
}

// export class Ddl {
//     text: string;
//     value: number;
//     otherData: any
// }

export class DdlItem {
  text: string;
  value: number;
  otherData?: any;
}

export type Ddls = {
  [key: string]: DdlItem[];
};
export class Obj<T = any> {
  [key: string]: T;
}

export class tableHeader {
  constructor(
    public key: string,
    public text: string,
    public isSearchable: boolean = false,
    public filterDropdown: DdlItem | null = null,
    public isFilterMultiSelect: boolean = false
  ) { }
}

export class indexModel {
  page: number = 1;
  pageSize: number = DefaultTableLength;
  orderBy: string = 'ModifiedDate';
  orderByAsc: number = 0;
  isActive: number = -1;
}

export type columns = Array<column>;
export class column {
  columnKey: string;
  filterKey?: string;
  columnWidth?: string;
  columnAlign?: 'right' | 'left';
  columnText: string;
  sorting?: boolean;
  searchType?: 'text' | 'singleSelect' | 'multiSelect' | 'switch';
  type?: 'date' | 'text' | 'updateProgress' | 'image' | 'switch' | 'isenum' | 'copyIcon' | 'siteView';
  filterOptions?: DdlItem[]; //dropdown for filter
  statusDdl?: DdlItem[];
  isHide?: boolean;
}

export class formElement {
  controlName: any;
  type:
    'select'
    | 'switch'
    | 'text'
    | 'slug'
    | 'file'
    | 'date'
    | 'dateRange'
    | 'textarea'
    | 'htmlEditor'
    | 'radio'
    | 'tableForm'
  label: string;

  placeholder?: string | string[];
  class?: string;
  id?: string;
  otherApiCall?: any;
  slugFrom?: string;
  numberFormat?: NumberFormat;
  isOtherApi?: boolean;
  fileConfig?: {
    maxFileSize?: number; // in KB
    minFileSize?: number; // in KB
    accept?: string;
    isMultiple?: boolean;
    isThumbnail?: boolean;
    showCropper?: boolean;
    croppingRatio?: number;
  };
  selectConfig?: {

    ddlKey?: string;
    addComponent?: any;
    addComponentClassName?: string;

    isMultiple?: boolean;
    selectOptions?: DdlItem[];
    replaceValueWithKey?: boolean;
    childDdl?: {
      DdlMethodRef: Function,
      ChamgeValuef?: Function,
      childControlName: string
    },
    parentDdl?: {
      parentControlName: string
    }
  };
  dateConfig?: {
    minDate?: Date;
    maxDate?: Date;
  };
  radtioOptions?: radioOption[] = [];
  isDisabled?: boolean = false;
  translateTo?: string;
  tableFormConfig?: {
    childForm?: FormGroup;
    childFormElements?: formElement[];
  }
}

export interface Breadcrumb {
  name: string;
  path: string;
  type: string;
}
export interface radioOption {
  text: string;
  value: number;
}

export class EditorVal {
  json: any = {}
  html: any = ''
}

export interface NumberFormat {
  isNumber?: false;
  isDecimalNumber?: false;
  isCommaOrDashAllowed?: false;
  isDotAllowed?: false;
};