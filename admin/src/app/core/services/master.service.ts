import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { EndPoints } from '../api';
import { EditorVal, Obj } from '../models/core.model';
import { FilesWithPrev, Url } from '../models/FormElementsModel';
import {
  AssetsMasterFilterModel,
  AssetsMasterModel,
  AssetsMasterViewModel,
} from '../models/master-models/assets-master-model';
import {
  BlockTypeFilterModel,
  BlockTypeModel,
  BlockTypeViewModel,
} from '../models/master-models/block-type-model';
import {
  category,
  categoryList,
  categoryListFilters,
} from '../models/master-models/category-model';
import {
  department,
  departmentList,
  departmentListFilters,
} from '../models/master-models/department.model';
import { GroupMasterFilterModel, GroupMasterModel, GroupMasterViewModel } from '../models/master-models/group-master-model';
import {
  jobDesignation,
  jobDesignationList,
  jobDesignationListFilters,
} from '../models/master-models/JobDesignation.Model';
import {
  menu,
  menuList,
  menuListFilters,
} from '../models/master-models/menu.model';
import { PageListFilter } from '../models/master-models/page.model';
import {
  qualification,
  qualificationList,
  qualificationListFilters,
} from '../models/master-models/Qualification.model';
import { SubCategoryFilterModel, SubCategoryViewModel } from '../models/master-models/sub-category-model';
import { BaseService } from './base.service';
import { LookupTypeFilterModel, LookupTypeViewListModel, LookupTypeViewModel } from '../models/master-models/LookupTypeModel';
import { LookupFilterModel, LookupViewListModel, LookupViewModel } from '../models/master-models/lookupmodel';
import { BannerFilterModel, BannerViewListModel, BannerViewModel } from '../models/master-models/banner-model';

@Injectable({
  providedIn: 'root',
})
export class MasterService {
  constructor(private _baseService: BaseService) { }
  //#region <menu>
  getMenuList = (payload: menuListFilters) => {
    return this._baseService.post<menuList[]>(
      EndPoints.menuMaster.getMenuMasterListUrl,
      payload
    );
  };
  getMenuById = (id: number) => {
    return this._baseService.get<menu[]>(
      EndPoints.menuMaster.getMenuMasterByIdUrl,
      { id }
    );
  };
  addUpdateMenu = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.menuMaster.addUpdateMenuMaster,
      payload,
      queryParams
    );
  };
  deleteMenu = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.menuMaster.deleteMenuMasterUrl,
      { id }
    );
  };
  changeStatusMenu = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.menuMaster.changeStatusMenuMasterUrl,
      { id }
    );
  };

  //#endregion

  //#region <page>
  getPageList = (payload: PageListFilter) => {
    return this._baseService.post<menuList[]>(
      EndPoints.pageMaster.getPageMasterListUrl,
      payload
    );
  };
  getPageById = (id: number) => {
    return this._baseService.get<menu[]>(
      EndPoints.pageMaster.getPageMasterByIdUrl,
      { id }
    );
  };
  addUpdatePage = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.pageMaster.addUpdatePageMaster,
      payload,
      queryParams
    );
  };
  deletePage = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.pageMaster.deletePageMasterUrl,
      { id }
    );
  };
  changeStatusPage = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.pageMaster.changeStatusPageMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <department>
  getDepartmentList = (payload: departmentListFilters) => {
    return this._baseService.post<departmentList[]>(
      EndPoints.departmentMaster.getDepartmentMasterListUrl,
      payload
    );
  };
  getDepartmentById = (id: number) => {
    return this._baseService
      .get<department>(EndPoints.departmentMaster.getDepartmentMasterByIdUrl, {
        id,
      })
      .pipe(
        map((result: any) => {

          let desc: EditorVal = { json: result.data.descriptionJson, html: result.data.description }
          result.data.description = desc;

          let descHi: EditorVal = { json: result.data.descriptionHindiJson, html: result.data.descriptionHindi }
          result.data.descriptionHindi = descHi;

          return result;
        })
      );
  };
  addUpdateDepartment = (payload: any, queryParams: Obj<number>) => {

    let desc = payload.description
    payload.description = desc?.html || ''
    payload.descriptionJson = Object.keys(desc?.json || {}).length ? desc?.json : '{}'

    let descHi = payload.descriptionHindi
    payload.descriptionHindi = descHi?.html || ''
    payload.descriptionHindiJson = Object.keys(descHi?.json || {}).length ? descHi?.json : '{}'

    return this._baseService.post<number>(
      EndPoints.departmentMaster.addUpdateDepartmentMaster,
      payload,
      queryParams
    );
  };
  deleteDepartment = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.departmentMaster.deleteDepartmentMasterUrl,
      { id }
    );
  };
  changeStatusDepartment = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.departmentMaster.changeStatusDepartmentMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <jobDesignation master>
  getJobDesignationList = (payload: jobDesignationListFilters) => {
    return this._baseService.post<jobDesignationList[]>(
      EndPoints.JobDesignation.getJobDesignationListUrl,
      payload
    );
  };
  getJobDesignationById = (id: number) => {
    return this._baseService.get<jobDesignation[]>(
      EndPoints.JobDesignation.getJobDesignationByIdUrl,
      { id }
    );
  };
  addUpdateJobDesignation = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.JobDesignation.addUpdateJobDesignationMaster,
      payload,
      queryParams
    );
  };
  deleteJobDesignation = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.JobDesignation.deleteJobDesignationUrl,
      { id }
    );
  };
  changeStatusJobDesignation = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.JobDesignation.changeStatusJobDesignationUrl,
      { id }
    );
  };
  //#endregion

  //#region <QualificationMaster>
  getQualificationMasterList = (payload: qualificationListFilters) => {
    return this._baseService.post<qualificationList[]>(
      EndPoints.QualificationMaster.getJQualificationMasterListUrl,
      payload
    );
  };

  getQualificationMasterById = (id: number) => {
    return this._baseService.get<qualification[]>(
      EndPoints.QualificationMaster.getQualificationMasterByIdUrl,
      { id }
    );
  };
  addUpdateQualificationMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.QualificationMaster.addUpdateQualificationMaster,
      payload,
      queryParams
    );
  };
  deleteQualificationMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.QualificationMaster.deleteQualificationMasterUrl,
      { id }
    );
  };
  changeStatusQualificationMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.QualificationMaster.changeQualificationMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <CategoryMaste>
  getCategoryMasterList = (payload: categoryListFilters) => {
    return this._baseService.post<categoryList[]>(
      EndPoints.CategoryMaster.getCategoryMasterListUrl,
      payload
    );
  };

  getCategoryMasterById = (id: number) => {
    return this._baseService.get<category>(
      EndPoints.CategoryMaster.getCategoryMasterByIdUrl,
      { id }
    )
  };
  addUpdateCategoryMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.CategoryMaster.addUpdateCategoryMaster,
      payload,
      queryParams
    );
  };
  deleteCategoryMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.CategoryMaster.deleteCategoryMasterUrl,
      { id }
    );
  };
  changeStatusCategoryMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.CategoryMaster.changeCategoryMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <AssetsMaster>
  getAssetsMasterList = (payload: AssetsMasterFilterModel) => {
    return this._baseService.post<AssetsMasterViewModel[]>(
      EndPoints.AssetsMaster.getAssetsMasterListUrl,
      payload
    );
  };

  getAssetsMasterById = (id: number) => {
    return this._baseService.get<AssetsMasterModel[]>(
      EndPoints.AssetsMaster.getAssetsMasterByIdUrl,
      { id }
    )
  };
  addUpdateAssetsMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.AssetsMaster.addAssetsCategoryMaster,
      payload,
      queryParams
    );
  };

  deleteAssetsMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.AssetsMaster.deleteAssetsMasterUrl,
      { id }
    );
  };
  changeStatusAssetsMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.AssetsMaster.changeAssetsMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <BlockTypeMaster>
  getBlockTypeMasterList = (payload: BlockTypeFilterModel) => {
    return this._baseService.post<BlockTypeViewModel[]>(
      EndPoints.BlockTypeMaster.getBlockTypeMasterListUrl,
      payload
    );
  };

  getBlockTypeMasterById = (id: number) => {
    return this._baseService.get<BlockTypeModel[]>(
      EndPoints.BlockTypeMaster.getBlockTypeMasterByIdUrl,
      { id }
    );
  };
  addUpdateBlockTypeMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.BlockTypeMaster.addUpdateBlockTypeMaster,
      payload,
      queryParams
    );
  };
  deleteBlockTypeMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BlockTypeMaster.deleteBlockTypeMasterUrl,
      { id }
    );
  };
  changeStatusBlockTypeMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BlockTypeMaster.changeBlockTypeMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <GroupMaster>
  getGroupMasterList = (payload: GroupMasterFilterModel) => {
    return this._baseService.post<GroupMasterViewModel[]>(
      EndPoints.GroupMaster.getGroupMasterListUrl,
      payload
    );
  };

  getGroupMasterById = (id: number) => {
    return this._baseService.get<GroupMasterModel[]>(
      EndPoints.GroupMaster.getGroupMasterByIdUrl,
      { id }
    );
  };
  addUpdateGroupMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.GroupMaster.addUpdateGroupMaster,
      payload,
      queryParams
    );
  };
  deleteGroupMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.GroupMaster.deleteGroupMasterUrl,
      { id }
    );
  };
  changeStatusGroupMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.GroupMaster.changeGroupMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <SubCategoryMaster>
  getSubCategoryMasterList = (payload: SubCategoryFilterModel) => {
    return this._baseService.post<SubCategoryViewModel[]>(
      EndPoints.SubCategoryMaster.getSubCategoryMasterListUrl,
      payload
    );
  };

  getSubCategoryMasterById = (id: number) => {
    return this._baseService.get<category>(
      EndPoints.SubCategoryMaster.getSubCategoryMasterByIdUrl,
      { id }
    )
  };
  addUpdateSubCategoryMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.SubCategoryMaster.addUpdateSubCategoryMaster,
      payload,
      queryParams
    );
  };
  deleteSubCategoryMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.SubCategoryMaster.deleteSubCategoryMasterUrl,
      { id }
    );
  };
  changeStatusSubCategoryMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.SubCategoryMaster.changeSubCategoryMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <LookupTypeMaster>
  getLookupTypeMasterList = (payload: LookupTypeFilterModel) => {
    return this._baseService.post<LookupTypeViewListModel[]>(
      EndPoints.LookupTypeMaster.getLookupTypeMasterListUrl,
      payload
    );
  };

  getLookupTypeMasterById = (id: number) => {
    return this._baseService.get<LookupTypeViewModel[]>(
      EndPoints.LookupTypeMaster.getLookupTypeMasterByIdUrl,
      { id }
    );
  };
  addUpdateLookupTypeMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.LookupTypeMaster.addUpdateLookupTypeMaster,
      payload,
      queryParams
    );
  };
  deleteLookupTypeMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.LookupTypeMaster.deleteLookupTypeMasterUrl,
      { id }
    );
  };
  changeStatusLookupTypeMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.LookupTypeMaster.changeLookupTypeMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <LookupMaster>
  getLookupMasterList = (payload: LookupFilterModel) => {
    return this._baseService.post<LookupViewListModel[]>(
      EndPoints.LookupMaster.getlookupMasterListUrl,
      payload
    );
  };

  getLookupMasterById = (id: number) => {
    return this._baseService.get<LookupViewModel[]>(
      EndPoints.LookupMaster.getlookupMasterByIdUrl,
      { id }
    );
  };
  addUpdateLookupMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.LookupMaster.addUpdatelookupMaster,
      payload,
      queryParams
    );
  };
  deleteLookupMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.LookupMaster.deletelookupMasterUrl,
      { id }
    );
  };
  changeStatusLookupMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.LookupMaster.changelookupMasterUrl,
      { id }
    );
  };
  //#endregion

  //#region <BannerMaster>
  getBannerMasterList = (payload: BannerFilterModel) => {
    return this._baseService.post<BannerViewListModel[]>(
      EndPoints.BannerMaster.getBannerMasterListUrl,
      payload
    );
  };

  getBannerMasterById = (id: number) => {
    return this._baseService.get<BannerViewModel[]>(
      EndPoints.BannerMaster.getBannerMasterByIdUrl,
      { id }
    )
  };

  addUpdateBannerMaster = (payload: any, queryParams: Obj<number>) => {
    return this._baseService.post<number>(
      EndPoints.BannerMaster.addUpdateBannerMaster,
      payload,
      queryParams
    );
  };

  deleteBannerMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BannerMaster.deleteBannerMasterUrl,
      { id }
    );
  };

  changeStatusBannerMaster = (id: number) => {
    return this._baseService.get<boolean>(
      EndPoints.BannerMaster.changeBannerMasterUrl,
      { id }
    );
  };

  //#endregion
}
