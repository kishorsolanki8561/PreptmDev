import { indexModel } from '../core.model';

export class jobDesignationListFilters extends indexModel {
  name: string = '';
  nameHindi: string = '';
  fromDate: string = '';
  toDate: string = '';
}
export class jobDesignationList {
  id: number;
  name: string;
  nameHindi: string;
  modifiedByName: string;
  modifiedDate: string;
  isActive: boolean;
  isDelete: boolean;
}

export class jobDesignation {
  id: number;
  name: string;
}
