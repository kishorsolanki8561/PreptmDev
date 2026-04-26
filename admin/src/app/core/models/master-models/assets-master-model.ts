import { indexModel } from '../core.model';
export class AssetsMasterModel {
  id: number;
  path: string | null;
  directoryName: string;
  title: string;
}

export class AssetsMasterViewModel {
  id: number;
  title: string;
  directoryName: string;
  modifiedByName: string;
  modifiedDate: string;
  isActive: boolean;
}

export class AssetsMasterFilterModel extends indexModel {
  title: string = '';
  directoryName: string = '';
  fromDate: string = '';
  toDate: string = '';
}
