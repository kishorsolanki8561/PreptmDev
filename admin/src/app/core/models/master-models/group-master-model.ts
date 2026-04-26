import { indexModel } from '../core.model';

export class GroupMasterModel {
  id: number;
  name: string;
  nameHindi:string;
  slugUrl: string;
}

export class GroupMasterViewModel {
  id: number;
  name: string;
  slugUrl: string;
  modifiedByName: string;
  modifiedDate: string;
  nameHindi:string;
  isActive: boolean;
}

export class GroupMasterFilterModel extends indexModel {
  name: string = '';
  slugUrl: string = '';
  fromDate: string = '';
  toDate: string = '';
  constructor() {
    super();
    this.name = '';
  }
}
