import { Routes } from '@angular/router';
import { ActionTypes } from 'src/app/core/models/fixed-value';
import { UserTypeAddResolver, UserTypeResolver } from '../users/admin-users.resolver';
import { AddUpdateRoleComponent } from '../users/role/add-update-role/add-update-role.component';
import { RoleComponent } from '../users/role/role.component';
import { AddUpdateAssetsMasterComponent } from './assets-master/add-update-assets-master/add-update-assets-master.component';
import { AssetsMasterComponent } from './assets-master/assets-master.component';
import { AddUpdateBlockTypeComponent } from './block-type-master/add-update-block-type/add-update-block-type.component';
import { BlockTypeMasterComponent } from './block-type-master/block-type-master.component';
import { AddUpdateCategoryMasterComponent } from './category-master/add-update-category-master/add-update-category-master.component';
import { CategoryMasterComponent } from './category-master/category-master.component';
import { AddUpdateDepartmentComponent } from './department-master/add-update-department/add-update-department.component';
import { DepartmentMasterComponent } from './department-master/department-master.component';
import { AddUpdateGroupMasterComponent } from './group-master/add-update-group-master/add-update-group-master.component';
import { GroupMasterComponent } from './group-master/group-master.component';
import { AddUpdateJobDesignationMasterComponent } from './job-designation-master/add-update-job-designation-master/add-update-job-designation-master.component';
import { JobDesignationMasterComponent } from './job-designation-master/job-designation-master.component';
import {
  AssetsMasterAddUpdateResolver,
  AssetsMasterResolver,
  BannerAddUpdateResolver,
  BannerResolver,
  BlockTypeMasterAddUpdateResolver,
  BlockTypeMasterResolver,
  CategoryMasterAddUpdateResolver,
  CategoryMasterResolver,
  DepartmentMasterAddUpdateResolver,
  DepartmentMasterResolver,
  GroupMasterAddUpdateResolver,
  GroupMasterResolver,
  JobDesignationMasterAddUpdateResolver,
  JobDesignationMasterResolver,
  LookupAddUpdateResolver,
  LookupResolver,
  LookupTypeAddUpdateResolver,
  LookupTypeResolver,
  MenuMasterAddUpdateResolver,
  MenuMasterResolver,
  PageMasterAddUpdateResolver,
  PageMasterResolver,
  QualificationMasterAddUpdateResolver,
  QualificationMasterResolver,
  SubCategoryMasterAddUpdateResolver,
  SubCategoryMasterResolver,
} from './master.resolver';
import { AddUpdateMenuComponent } from './menu-master/add-update-menu/add-update-menu.component';
import { MenuMasterComponent } from './menu-master/menu-master.component';
import { AddUpdatePageComponent } from './page-master/add-update-page/add-update-page.component';
import { PageMasterComponent } from './page-master/page-master.component';
import { AddUpdateQualificationMasterComponent } from './qualification-master/add-update-qualification-master/add-update-qualification-master.component';
import { QualificationMasterComponent } from './qualification-master/qualification-master.component';
import { AddUpdateSubCategoryComponent } from './sub-category-master/add-update-sub-category/add-update-sub-category.component';
import { SubCategoryMasterComponent } from './sub-category-master/sub-category-master.component';
import { LookupTypeComponent } from './lookup-type/lookup-type.component';
import { AddUpdateLookupTypeComponent } from './lookup-type/add-update-lookup-type/add-update-lookup-type.component';
import { LookupComponent } from './lookup/lookup.component';
import { AddUpdateLookupComponent } from './lookup/add-update-lookup/add-update-lookup.component';
import { BannerMasterComponent } from './banner-master/banner-master.component';
import { AddUpdateBannerMasterComponent } from './banner-master/add-update-banner-master/add-update-banner-master.component';
export const routes: Routes = [
  //#region <menu>
  {
    path: 'menu',
    component: MenuMasterComponent,
    data: {
      breadcrumb: [{ name: 'Menu' }],
      pageAction: ActionTypes.LIST,
      component: "MenuMasterComponent"
    },
    resolve: {
      initialData: MenuMasterResolver,
    },
  },
  {
    path: 'menu/add',
    component: AddUpdateMenuComponent,
    data: {
      breadcrumb: [
        { name: 'Menu', path: '/master/menu' },
        { name: 'Add Menu' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateMenuComponent"
    },
    resolve: {
      initialData: MenuMasterAddUpdateResolver,
    },
  },
  {
    path: 'menu/edit/:id',
    component: AddUpdateMenuComponent,
    data: {
      breadcrumb: [
        { name: 'Menu', path: '/master/menu' },
        { name: 'Edit Menu' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateMenuComponent"
    },
    resolve: {
      initialData: MenuMasterAddUpdateResolver,
    },
  },
  {
    path: 'menu/:id',
    component: AddUpdateMenuComponent,
    data: {
      breadcrumb: [
        { name: 'Menu', path: '/master/menu' },
        { name: 'View Menu' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateMenuComponent"
    },
    resolve: {
      initialData: MenuMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <page>
  {
    path: 'page',
    component: PageMasterComponent,
    data: {
      breadcrumb: [{ name: 'Page' }],
      pageAction: ActionTypes.LIST,
      component: "PageMasterComponent"
    },
    resolve: {
      initialData: PageMasterResolver,
    },
  },
  {
    path: 'page/add',
    component: AddUpdatePageComponent,
    data: {
      breadcrumb: [{ name: 'Page', path: '../' }, { name: 'Add Page' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdatePageComponent"
    },
    resolve: {
      initialData: PageMasterAddUpdateResolver,
    },
  },
  {
    path: 'page/edit/:id',
    component: AddUpdatePageComponent,
    data: {
      breadcrumb: [{ name: 'Page', path: '../../' }, { name: 'Edit Page' }],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdatePageComponent"
    },
    resolve: {
      initialData: PageMasterAddUpdateResolver,
    },
  },
  {
    path: 'page/:id',
    component: AddUpdatePageComponent,
    data: {
      breadcrumb: [{ name: 'Page', path: '../../' }, { name: 'View Page' }],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdatePageComponent"
    },
    resolve: {
      initialData: PageMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <department>
  {
    path: 'department',
    component: DepartmentMasterComponent,
    data: {
      breadcrumb: [{ name: 'Department' }],
      pageAction: ActionTypes.LIST,
      component: "DepartmentMasterComponent"
    },
    resolve: {
      initialData: DepartmentMasterResolver,
    },
  },
  {
    path: 'department/add',
    component: AddUpdateDepartmentComponent,
    data: {
      breadcrumb: [
        { name: 'Department', path: '../' },
        { name: 'Add Department' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateDepartmentComponent"
    },
    resolve: {
      initialData: DepartmentMasterAddUpdateResolver,
    },
  },
  {
    path: 'department/edit/:id',
    component: AddUpdateDepartmentComponent,
    data: {
      breadcrumb: [
        { name: 'Department', path: '../../' },
        { name: 'Edit Department' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateDepartmentComponent"
    },
    resolve: {
      initialData: DepartmentMasterAddUpdateResolver,
    },
  },
  {
    path: 'department/:id',
    component: AddUpdateDepartmentComponent,
    data: {
      breadcrumb: [
        { name: 'Department', path: '../../' },
        { name: 'View Department' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateDepartmentComponent"
    },
    resolve: {
      initialData: DepartmentMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <job Designation>
  {
    path: 'jobdesignation',
    component: JobDesignationMasterComponent,
    data: {
      breadcrumb: [{ name: 'Designation' }],
      pageAction: ActionTypes.LIST,
      component: "JobDesignationMasterComponent"
    },
    resolve: {
      initialData: JobDesignationMasterResolver,
    },
  },
  {
    path: 'jobdesignation/add',
    component: AddUpdateJobDesignationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Designation', path: '../' },
        { name: 'Add Designation' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateJobDesignationMasterComponent"
    },
  },
  {
    path: 'jobdesignation/edit/:id',
    component: AddUpdateJobDesignationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Designation', path: '../../' },
        { name: 'Edit Designation' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateJobDesignationMasterComponent"
    },
    resolve: {
      initialData: JobDesignationMasterAddUpdateResolver,
    },
  },
  {
    path: 'jobdesignation/:id',
    component: AddUpdateJobDesignationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Designation', path: '../../' },
        { name: 'View Designation' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateJobDesignationMasterComponent"
    },
    resolve: {
      initialData: JobDesignationMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <QualificationMaster>
  {
    path: 'qualification',
    component: QualificationMasterComponent,
    data: {
      breadcrumb: [{ name: 'Qualification' }],
      pageAction: ActionTypes.LIST,
      component: "QualificationMasterComponent"
    },
    resolve: {
      initialData: QualificationMasterResolver,
    },
  },
  {
    path: 'qualification/add',
    component: AddUpdateQualificationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Qualification', path: '../' },
        { name: 'Add Qualification' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateQualificationMasterComponent"
    },
  },
  {
    path: 'qualification/edit/:id',
    component: AddUpdateQualificationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Qualification', path: '../../' },
        { name: 'Edit Qualification' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateQualificationMasterComponent"
    },
    resolve: {
      initialData: QualificationMasterAddUpdateResolver,
    },
  },
  {
    path: 'qualification/:id',
    component: AddUpdateQualificationMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Qualification', path: '../../' },
        { name: 'Edit Qualification' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateQualificationMasterComponent"
    },
    resolve: {
      initialData: QualificationMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <CategoryMaster>
  {
    path: 'category',
    component: CategoryMasterComponent,
    data: {
      breadcrumb: [{ name: 'Category' }],
      pageAction: ActionTypes.LIST,
      component: "CategoryMasterComponent"
    },
    resolve: {
      initialData: CategoryMasterResolver,
    },
  },
  {
    path: 'category/add',
    component: AddUpdateCategoryMasterComponent,
    data: {
      breadcrumb: [{ name: 'Category', path: '../' }, { name: 'Add Category' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateCategoryMasterComponent"
    },
  },
  {
    path: 'category/edit/:id',
    component: AddUpdateCategoryMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Category', path: '../../' },
        { name: 'Edit Category' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateCategoryMasterComponent"
    },
    resolve: {
      initialData: CategoryMasterAddUpdateResolver,
    },
  },
  {
    path: 'category/:id',
    component: AddUpdateCategoryMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Category', path: '../../' },
        { name: 'Edit Category' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateCategoryMasterComponent"
    },
    resolve: {
      initialData: CategoryMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <AssetsMaster>
  {
    path: 'assets',
    component: AssetsMasterComponent,
    data: {
      breadcrumb: [{ name: 'Assets' }],
      pageAction: ActionTypes.LIST,
      component: "AssetsMasterComponent"
    },
    resolve: {
      initialData: AssetsMasterResolver,
    },
  },
  {
    path: 'assets/add',
    component: AddUpdateAssetsMasterComponent,
    data: {
      breadcrumb: [{ name: 'Assets', path: '../' }, { name: 'Add Assets' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateAssetsMasterComponent"
    },
    resolve: {
      initialData: AssetsMasterAddUpdateResolver,
    },
  },
  {
    path: 'assets/edit/:id',
    component: AddUpdateAssetsMasterComponent,
    data: {
      breadcrumb: [{ name: 'Assets', path: '../../' }, { name: 'Edit Assets' }],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateAssetsMasterComponent"
    },
    resolve: {
      initialData: AssetsMasterAddUpdateResolver,
    },
  },
  {
    path: 'assets/:id',
    component: AddUpdateAssetsMasterComponent,
    data: {
      breadcrumb: [{ name: 'Assets', path: '../../' }, { name: 'View Assets' }],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateAssetsMasterComponent"
    },
    resolve: {
      initialData: AssetsMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <BlockTypeMaster>
  {
    path: 'blocktype',
    component: BlockTypeMasterComponent,
    data: {
      breadcrumb: [{ name: 'BlockType' }],
      pageAction: ActionTypes.LIST,
      component: "BlockTypeMasterComponent"
    },
    resolve: {
      initialData: BlockTypeMasterResolver,
    },
  },
  {
    path: 'blocktype/add',
    component: AddUpdateBlockTypeComponent,
    data: {
      breadcrumb: [
        { name: 'BlockType', path: '../' },
        { name: 'Add block type' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateBlockTypeComponent"
    },
  },
  {
    path: 'blocktype/edit/:id',
    component: AddUpdateBlockTypeComponent,
    data: {
      breadcrumb: [
        { name: 'BlockType', path: '../../' },
        { name: 'Edit block type' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateBlockTypeComponent"
    },
    resolve: {
      initialData: BlockTypeMasterAddUpdateResolver,
    },
  },
  {
    path: 'blocktype/:id',
    component: AddUpdateBlockTypeComponent,
    data: {
      breadcrumb: [
        { name: 'BlockType', path: '../../' },
        { name: 'View block type' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateBlockTypeComponent"

    },
    resolve: {
      initialData: BlockTypeMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <GroupMaster>
  {
    path: 'tags',
    component: GroupMasterComponent,
    data: {
      breadcrumb: [{ name: 'Tags' }],
      pageAction: ActionTypes.LIST,
      component: "GroupMasterComponent"
    },
    resolve: {
      initialData: GroupMasterResolver,
    },
  },
  {
    path: 'tags/add',
    component: AddUpdateGroupMasterComponent,
    data: {
      breadcrumb: [{ name: 'Tags', path: '../' }, { name: 'Add Tags' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateGroupMasterComponent"
    },
  },
  {
    path: 'tags/edit/:id',
    component: AddUpdateGroupMasterComponent,
    data: {
      breadcrumb: [{ name: 'Tags', path: '../../' }, { name: 'Edit Tags' }],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateGroupMasterComponent"
    },
    resolve: {
      initialData: GroupMasterAddUpdateResolver,
    },
  },
  {
    path: 'tags/:id',
    component: AddUpdateGroupMasterComponent,
    data: {
      breadcrumb: [{ name: 'Tags', path: '../../' }, { name: 'View Tags' }],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateGroupMasterComponent"
    },
    resolve: {
      initialData: GroupMasterAddUpdateResolver,
    },
  },

  //#endregion

  //#region <GroupMaster>
  {
    path: 'role',
    component: RoleComponent,
    data: {
      breadcrumb: [{ name: 'Role' }],
      pageAction: ActionTypes.LIST,
      component: "RoleComponent"
    },
    resolve: {
      initialData: UserTypeResolver,
    },
  },
  {
    path: 'role/add',
    component: AddUpdateRoleComponent,
    data: {
      breadcrumb: [{ name: 'Role', path: '../' }, { name: 'Add Role' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateRoleComponent"
    },
  },
  {
    path: 'role/edit/:id',
    component: AddUpdateRoleComponent,
    data: {
      breadcrumb: [{ name: 'Role', path: '../../' }, { name: 'Edit Role' }],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateRoleComponent"
    },
    resolve: {
      initialData: UserTypeAddResolver,
    },
  },
  {
    path: 'role/:id',
    component: AddUpdateRoleComponent,
    data: {
      breadcrumb: [{ name: 'Role', path: '../../' }, { name: 'View Role' }],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateRoleComponent"
    },
    resolve: {
      initialData: UserTypeAddResolver,
    },
  },
  //#endregion

  //#region <SubCategoryMaster>
  {
    path: 'sub-category',
    component: SubCategoryMasterComponent,
    data: {
      breadcrumb: [{ name: 'Sub Category' }],
      pageAction: ActionTypes.LIST,
      component: "SubCategoryMasterComponent"
    },
    resolve: {
      initialData: SubCategoryMasterResolver,
    },
  },
  {
    path: 'sub-category/add',
    component: AddUpdateSubCategoryComponent,
    data: {
      breadcrumb: [{ name: 'Sub-Category', path: '../' }, { name: 'Add Sub-Category' }],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateSubCategoryComponent"
    },
    resolve: {
      initialData: SubCategoryMasterAddUpdateResolver,
    },
  },
  {
    path: 'sub-category/edit/:id',
    component: AddUpdateSubCategoryComponent,
    data: {
      breadcrumb: [
        { name: 'Sub-Category', path: '../../' },
        { name: 'Edit Sub-Category' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateSubCategoryComponent"
    },
    resolve: {
      initialData: SubCategoryMasterAddUpdateResolver,
    },
  },
  {
    path: 'sub-category/:id',
    component: AddUpdateSubCategoryComponent,
    data: {
      breadcrumb: [
        { name: 'Sub Category', path: '../../' },
        { name: 'Edit Sub Category' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateSubCategoryComponent"
    },
    resolve: {
      initialData: SubCategoryMasterAddUpdateResolver,
    },
  },
  //#endregion

  //#region <LookupTypeMaster>
  {
    path: 'lookuptype',
    component: LookupTypeComponent,
    data: {
      breadcrumb: [{ name: 'LookupType' }],
      pageAction: ActionTypes.LIST,
      component: "LookupTypeComponent"
    },
    resolve: {
      initialData: LookupTypeResolver,
    },
  },
  {
    path: 'lookuptype/add',
    component: AddUpdateLookupTypeComponent,
    data: {
      breadcrumb: [
        { name: 'LookupType', path: '../' },
        { name: 'Add Lookup Type' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateLookupTypeComponent"
    },
  },
  {
    path: 'lookuptype/edit/:id',
    component: AddUpdateLookupTypeComponent,
    data: {
      breadcrumb: [
        { name: 'LookupType', path: '../../' },
        { name: 'Edit Lookup Type' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateLookupTypeComponent"
    },
    resolve: {
      initialData: LookupTypeAddUpdateResolver,
    },
  },
  {
    path: 'lookuptype/:id',
    component: AddUpdateLookupTypeComponent,
    data: {
      breadcrumb: [
        { name: 'LookupType', path: '../../' },
        { name: 'View Lookup Type' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateLookupTypeComponent"

    },
    resolve: {
      initialData: LookupTypeAddUpdateResolver,
    },
  },
  //#endregion

  //#region <LookupMaster>
  {
    path: 'lookup',
    component: LookupComponent,
    data: {
      breadcrumb: [{ name: 'Lookup' }],
      pageAction: ActionTypes.LIST,
      component: "LookupComponent"
    },
    resolve: {
      initialData: LookupResolver,
    },
  },
  {
    path: 'lookup/add',
    component: AddUpdateLookupComponent,
    data: {
      breadcrumb: [
        { name: 'Lookup', path: '../' },
        { name: 'Add Lookup' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateLookupComponent"
    },
    resolve: {
      initialData: LookupAddUpdateResolver,
    },
  },
  {
    path: 'lookup/edit/:id',
    component: AddUpdateLookupComponent,
    data: {
      breadcrumb: [
        { name: 'Lookup', path: '../../' },
        { name: 'Edit Lookup' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateLookupComponent"
    },
    resolve: {
      initialData: LookupAddUpdateResolver,
    },
  },
  {
    path: 'lookup/:id',
    component: AddUpdateLookupComponent,
    data: {
      breadcrumb: [
        { name: 'Lookup', path: '../../' },
        { name: 'View Lookup' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateLookupComponent"

    },
    resolve: {
      initialData: LookupAddUpdateResolver,
    },
  },
  //#endregion

  //#region <BannerMaster>
  {
    path: 'banner',
    component: BannerMasterComponent,
    data: {
      breadcrumb: [{ name: 'Banner' }],
      pageAction: ActionTypes.LIST,
      component: "BannerMasterComponent"
    },
    resolve: {
      initialData: BannerResolver,
    },
  },
  {
    path: 'banner/add',
    component: AddUpdateBannerMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Banner', path: '../' },
        { name: 'Add Banner' },
      ],
      pageAction: ActionTypes.ADD,
      component: "AddUpdateBannerMasterComponent"
    },
    resolve: {
      initialData: BannerAddUpdateResolver,
    },
  },
  {
    path: 'banner/edit/:id',
    component: AddUpdateBannerMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Banner', path: '../../' },
        { name: 'Edit Banner' },
      ],
      pageAction: ActionTypes.EDIT,
      component: "AddUpdateBannerMasterComponent"
    },
    resolve: {
      initialData: BannerAddUpdateResolver,
    },
  },
  {
    path: 'banner/:id',
    component: AddUpdateBannerMasterComponent,
    data: {
      breadcrumb: [
        { name: 'Banner', path: '../../' },
        { name: 'View Banner' },
      ],
      pageAction: ActionTypes.VIEW_DETAILS,
      component: "AddUpdateBannerMasterComponent"

    },
    resolve: {
      initialData: BannerAddUpdateResolver,
    },
  },
  //#endregion
];
