import { indexModel } from '../core.model';

export class BlockTypeModel {
  id: number;
  name: string;
  slugUrl: string;
  forRecruitment: boolean;
}

export class BlockTypeViewModel {
  id: number;
  name: string;
  slugUrl: string;
  forRecruitment: boolean;
  modifiedByName: string;
  modifiedDate: string;
  isActive: boolean;
}

export class BlockTypeFilterModel extends indexModel {
  name: string = '';
  slugUrl: string = '';
  fromDate: string = '';
  toDate: string = '';
  constructor() {
    super();
    this.name = '';
  }
}
