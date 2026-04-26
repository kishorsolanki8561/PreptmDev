import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  Resolve,
  RouterStateSnapshot,
} from '@angular/router';
import { forkJoin, map, Observable, of } from 'rxjs';
import { DdlsList } from 'src/app/core/api';
import { ApiResponseModel } from 'src/app/core/models/core.model';
import { AssetsMasterFilterModel } from 'src/app/core/models/master-models/assets-master-model';
import { BannerFilterModel } from 'src/app/core/models/master-models/banner-model';
import { BlockTypeFilterModel } from 'src/app/core/models/master-models/block-type-model';
import { categoryListFilters } from 'src/app/core/models/master-models/category-model';
import { departmentListFilters } from 'src/app/core/models/master-models/department.model';
import { GroupMasterFilterModel } from 'src/app/core/models/master-models/group-master-model';
import { jobDesignationListFilters } from 'src/app/core/models/master-models/JobDesignation.Model';
import { LookupFilterModel } from 'src/app/core/models/master-models/lookupmodel';
import { LookupTypeFilterModel } from 'src/app/core/models/master-models/LookupTypeModel';
import { menuListFilters } from 'src/app/core/models/master-models/menu.model';
import { PageListFilter } from 'src/app/core/models/master-models/page.model';
import { qualificationListFilters } from 'src/app/core/models/master-models/Qualification.model';
import { SubCategoryFilterModel } from 'src/app/core/models/master-models/sub-category-model';
import { CoreService } from 'src/app/core/services/core.service';
import { MasterService } from 'src/app/core/services/master.service';

//#region <menu>
@Injectable()
export class MenuMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getMenuList(new menuListFilters()),
    ]).pipe(
      map((data) => {
        return {
          menus: data[0],
        };
      })
    );
  }
}
@Injectable()
export class MenuMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.menuMasterAddUpdateDdls),
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getMenuById(Number(route.params['id']))
      );
    }
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          ddls: data[0],
          menuData: data[1] ? data[1] : undefined,
        };
      })
    );
  }
}

//#endregion

//#region <page>
@Injectable()
export class PageMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getPageList(new PageListFilter()),
    ]).pipe(
      map((data) => {
        return {
          pages: data[0],
        };
      })
    );
  }
}
@Injectable()
export class PageMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.pageMasterAddUpdateDdls),
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getPageById(Number(route.params['id']))
      );
    }
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          ddls: data[0] ? data[0] : undefined,
          pageData: data[1] ? data[1] : undefined,
        };
      })
    );
  }
}

//#endregion

//#region <department>
@Injectable()
export class DepartmentMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getDepartmentList(new departmentListFilters()),
    ]).pipe(
      map((data) => {
        return {
          departments: data[0],
        };
      })
    );
  }
}
@Injectable()
export class DepartmentMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.departmentMasterAddUpdateDdls),
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getDepartmentById(Number(route.params['id']))
      );
    }
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          ddls: data[0] ? data[0] : undefined,
          departmentData: data[1] ? data[1] : undefined,
        };
      })
    );
  }
}

//#endregion

//#region <JobDesignationMaster>
@Injectable()
export class JobDesignationMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getJobDesignationList(
        new jobDesignationListFilters()
      ),
    ]).pipe(
      map((data) => {
        return {
          jobDesignation: data[0],
        };
      })
    );
  }
}
@Injectable()
export class JobDesignationMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getJobDesignationById(Number(route.params['id']))
      );
    }
    return forkJoin(dataGetters).pipe(
      map((data) => {
        return {
          jobDesignationData: data[0] ? data[0] : undefined,
        };
      })
    );
  }
}

//#endregion

//#region <JobDesignationMaster>
@Injectable()
export class QualificationMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getQualificationMasterList(
        new qualificationListFilters()
      ),
    ]).pipe(
      map((data) => {
        return {
          qualification: data[0],
        };
      })
    );
  }
}
@Injectable()
export class QualificationMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getQualificationMasterById(
          Number(route.params['id'])
        )
      );
    }

    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            qualification: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}

//#endregion

//#region <CategoryMaster>
@Injectable()
export class CategoryMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getCategoryMasterList(new categoryListFilters()),
    ]).pipe(
      map((data) => {
        return {
          qualification: data[0],
        };
      })
    );
  }
}
@Injectable()
export class CategoryMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getCategoryMasterById(Number(route.params['id']))
      );
    }

    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            qualification: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}

//#endregion

//#region <AssetsMaster>
@Injectable()
export class AssetsMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getAssetsMasterList(new AssetsMasterFilterModel()),
    ]).pipe(
      map((data) => {
        return {
          assets: data[0],
        };
      })
    );
  }
}
@Injectable()
export class AssetsMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _masterService: MasterService,
    private _coreService: CoreService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.assetsAddUpdate)
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getAssetsMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            ddls: data[0] ? data[0] : undefined,
            assetsData: data[1] ? data[1] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}

//#endregion

//#region <BlockTypeMaster>
@Injectable()
export class BlockTypeMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getBlockTypeMasterList(new BlockTypeFilterModel()),
    ]).pipe(
      map((data) => {
        return {
          blockType: data[0],
        };
      })
    );
  }
}
@Injectable()
export class BlockTypeMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getBlockTypeMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            blockType: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}
//#endregion

//#region <GroupMaster>
@Injectable()
export class GroupMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getGroupMasterList(new GroupMasterFilterModel()),
    ]).pipe(
      map((data) => {
        return {
          group: data[0],
        };
      })
    );
  }
}
@Injectable()
export class GroupMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getGroupMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            group: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}
//#endregion

//#region <SubCategoryMaster>
@Injectable()
export class SubCategoryMasterResolver implements Resolve<any> {
  constructor(private _masterService: MasterService,
    private _coreService: CoreService,) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.SubCategory)
    ];
    dataGetters.push(
      this._masterService.getSubCategoryMasterList(new SubCategoryFilterModel()),
    );
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            ddls: data[0] ? data[0] : undefined,
            subcategory: data[1] ? data[1] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}
@Injectable()
export class SubCategoryMasterAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.SubCategory)
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getSubCategoryMasterById(Number(route.params['id']))
      );
    }

    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            ddls: data[0] ? data[0] : undefined,
            subcategory: data[1] ? data[1] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}
//#endregion

//#region <LookupTypeMaster>
@Injectable()
export class LookupTypeResolver implements Resolve<any> {
  constructor(private _masterService: MasterService) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getLookupTypeMasterList(new LookupTypeFilterModel()),
    ]).pipe(
      map((data) => {
        return {
          LookupType: data[0],
        };
      })
    );
  }
}
@Injectable()
export class LookupTypeAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getLookupTypeMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            LookupType: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }
  }
}
//#endregion

//#region <LookupMaster>
@Injectable()
export class LookupResolver implements Resolve<any> {
  constructor(private _masterService: MasterService
    , private _coreService: CoreService,
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._coreService.getDdls(DdlsList.masters.lookupType),
      this._masterService.getLookupMasterList(new LookupFilterModel())
    ]).pipe(
      map((data) => {
        return {
          ddls: data[0] ? data[0] : undefined,
          Lookup: data[1] ? data[1] : undefined,
        };
      })
    );
  }
}
@Injectable()
export class LookupAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
      this._coreService.getDdls(DdlsList.masters.lookupType)
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getLookupMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            ddls: data[0] ? data[0] : undefined,
            Lookup: data[1] ? data[1] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }



  }
}
//#endregion

//#region <BannerMaster>
@Injectable()
export class BannerResolver implements Resolve<any> {
  constructor(private _masterService: MasterService
    , private _coreService: CoreService,
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return forkJoin([
      this._masterService.getBannerMasterList(new BannerFilterModel())
    ]).pipe(
      map((data) => {
        return {
          Banner: data[0] ? data[0] : undefined,
        };
      })
    );
  }
}
@Injectable()
export class BannerAddUpdateResolver implements Resolve<any> {
  constructor(
    private _coreService: CoreService,
    private _masterService: MasterService
  ) { }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let dataGetters: Observable<ApiResponseModel<any>>[] = [
    ];
    if (route.params['id']) {
      dataGetters.push(
        this._masterService.getBannerMasterById(Number(route.params['id']))
      );
    }
    if (dataGetters.length > 0) {
      return forkJoin(dataGetters).pipe(
        map((data) => {
          return {
            Banner: data[0] ? data[0] : undefined,
          };
        })
      );
    } else {
      return of(true);
    }



  }
}
//#endregion
