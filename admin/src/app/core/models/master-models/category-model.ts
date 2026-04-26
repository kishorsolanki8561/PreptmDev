import { indexModel } from '../core.model';
import { FilesWithPrev } from '../FormElementsModel';

export class categoryListFilters extends indexModel {
  name: string = '';
  fromDate: string = '';
  toDate: string = '';
  slugUrl: string = '';
  icon: string = '';
}
export class categoryList {
  id: number;
  name: string;
  modifiedByName: string;
  modifiedDate: string;
  isActive: boolean;
  isDelete: boolean;
}

export class category {
  iconImage:FilesWithPrev
  id: number;
  name: string;
  slugUrl: string;
  icon: string;
}
