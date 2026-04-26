import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminUsersComponent } from './admin-users/admin-users.component';
import { CoreModule } from 'src/app/core/core.module';
import { routes } from './users.routing';
import { RouterModule } from '@angular/router';
import { AddUpdateAdminUserComponent } from './admin-users/add-update-admin-user/add-update-admin-user.component';
import { AdminUserAddResolver, AdminUserResolver, PagePermissionResolver } from './admin-users.resolver';
import { PermissionComponent } from './permission/permission.component';
import { RoleComponent } from './role/role.component';
import { AddUpdateRoleComponent } from './role/add-update-role/add-update-role.component';



@NgModule({
  declarations: [
    AdminUsersComponent,
    AddUpdateAdminUserComponent,
    PermissionComponent,
    RoleComponent,
    AddUpdateRoleComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(routes)
  ],
  providers: [AdminUserAddResolver, AdminUserResolver,PagePermissionResolver]
})
export class UsersModule { }
