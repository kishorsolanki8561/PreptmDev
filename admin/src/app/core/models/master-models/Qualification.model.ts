import { indexModel } from '../core.model';

export class qualificationListFilters extends indexModel {
  title: string = '';
  fromDate: string = '';
  toDate: string = '';
}
export class qualificationList {
  id: number;
  title: string;
  modifiedByName: string;
  modifiedDate: string;
  isActive: boolean;
  isDelete: boolean;
}

export class qualification {
  id: number;
  title: string;
}
