import { PAGE_SIZE } from "../fixed-values";

export class ApiResponseModel<T> {
  isSuccess: boolean;
  statusCode: number;
  message: string;
  data: T;
  otherData: any;
  totalRecords: number
}
export class Obj<T = any> {
  [key: string]: T
}

export class PaginationModel {
  itemsPerPage = PAGE_SIZE;
  currentPage = 1;
  totalItems = 0;
}

export class ddlItem {
  text: string;
  value: number;
  otherData: {
    CategoryNameHindi: string,
    icon: string,
    slugUrl: string,
    subCategoryCount: number,
  }
}

export class ddl {
  ddlCategory: ddlItem[];
  ddlDepartment: ddlItem[];
  ddlJobDesignation: ddlItem[];
  ddlQualification: ddlItem[];
  ddlGroup: ddlItem[];
  articleType: ddlItem[];
}

export class ddlLookup {
  [key: string]: ddlItem[];
}

export interface Breadcrumb {
  text: string;
  path?: string;
}

export class ShareContent {
  title: string;
  totalPost: string;
  startDate: string;
  date: string;
  FeeLastDate: string;
  lastDate: string;
  extendedDate: string;
  admitCardDate: string;
  link: string;

}

export class MetaData {
  keywords?: string;
  description?: string;
}



