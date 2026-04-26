import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuMasterComponent } from './menu-master/menu-master.component';
import { RouterModule } from '@angular/router';
import { routes } from './master.routing';
import { PageMasterComponent } from './page-master/page-master.component';
import { AddUpdateMenuComponent } from './menu-master/add-update-menu/add-update-menu.component';
import { AddUpdatePageComponent } from './page-master/add-update-page/add-update-page.component';
import { CoreModule } from 'src/app/core/core.module';
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
import { DepartmentMasterComponent } from './department-master/department-master.component';
import { AddUpdateDepartmentComponent } from './department-master/add-update-department/add-update-department.component';
import { JobDesignationMasterComponent } from './job-designation-master/job-designation-master.component';
import { AddUpdateJobDesignationMasterComponent } from './job-designation-master/add-update-job-designation-master/add-update-job-designation-master.component';
import { QualificationMasterComponent } from './qualification-master/qualification-master.component';
import { AddUpdateQualificationMasterComponent } from './qualification-master/add-update-qualification-master/add-update-qualification-master.component';
import { CategoryMasterComponent } from './category-master/category-master.component';
// import { AddUpdateCategoryMasterComponent } from './category-master/add-update-category-master/add-update-category-master.component';
import { BlockTypeMasterComponent } from './block-type-master/block-type-master.component';
import { AssetsMasterComponent } from './assets-master/assets-master.component';
import { AddUpdateBlockTypeComponent } from './block-type-master/add-update-block-type/add-update-block-type.component';
import { AddUpdateAssetsMasterComponent } from './assets-master/add-update-assets-master/add-update-assets-master.component';
import { GroupMasterComponent } from './group-master/group-master.component';
import { AddUpdateGroupMasterComponent } from './group-master/add-update-group-master/add-update-group-master.component';
import { UserTypeAddResolver, UserTypeResolver } from '../users/admin-users.resolver';
import { SubCategoryMasterComponent } from './sub-category-master/sub-category-master.component';
import { AddUpdateSubCategoryComponent } from './sub-category-master/add-update-sub-category/add-update-sub-category.component';
import { LookupTypeComponent } from './lookup-type/lookup-type.component';
import { AddUpdateLookupTypeComponent } from './lookup-type/add-update-lookup-type/add-update-lookup-type.component';
import { LookupComponent } from './lookup/lookup.component';
import { AddUpdateLookupComponent } from './lookup/add-update-lookup/add-update-lookup.component';
import { BannerMasterComponent } from './banner-master/banner-master.component';
import { AddUpdateBannerMasterComponent } from './banner-master/add-update-banner-master/add-update-banner-master.component';

@NgModule({
  declarations: [
    MenuMasterComponent,
    PageMasterComponent,
    AddUpdateMenuComponent,
    AddUpdatePageComponent,
    DepartmentMasterComponent,
    AddUpdateDepartmentComponent,
    JobDesignationMasterComponent,
    AddUpdateJobDesignationMasterComponent,
    QualificationMasterComponent,
    AddUpdateQualificationMasterComponent,
    CategoryMasterComponent,
    // AddUpdateCategoryMasterComponent,
    BlockTypeMasterComponent,
    AssetsMasterComponent,
    AddUpdateBlockTypeComponent,
    AddUpdateAssetsMasterComponent,
    GroupMasterComponent,
    AddUpdateGroupMasterComponent,
    SubCategoryMasterComponent,
    AddUpdateSubCategoryComponent,
    LookupTypeComponent,
    AddUpdateLookupTypeComponent,
    LookupComponent,
    AddUpdateLookupComponent,
    BannerMasterComponent,
    AddUpdateBannerMasterComponent,
  ],
  imports: [CommonModule, RouterModule.forChild(routes), CoreModule],
  providers: [
    MenuMasterResolver,
    MenuMasterAddUpdateResolver,
    PageMasterResolver,
    PageMasterAddUpdateResolver,
    DepartmentMasterAddUpdateResolver,
    DepartmentMasterResolver,
    JobDesignationMasterAddUpdateResolver,
    JobDesignationMasterResolver,
    QualificationMasterResolver,
    QualificationMasterAddUpdateResolver,
    CategoryMasterResolver,
    CategoryMasterAddUpdateResolver,
    BlockTypeMasterResolver,
    BlockTypeMasterAddUpdateResolver,
    AssetsMasterResolver,
    AssetsMasterAddUpdateResolver,
    GroupMasterResolver,
    GroupMasterAddUpdateResolver,
    UserTypeAddResolver,
    UserTypeResolver,
    SubCategoryMasterResolver,
    SubCategoryMasterAddUpdateResolver,
    LookupTypeResolver,
    LookupTypeAddUpdateResolver,
    LookupResolver,
    LookupAddUpdateResolver,
    BannerResolver,
    BannerAddUpdateResolver
  ],
})
export class MasterModule {}
