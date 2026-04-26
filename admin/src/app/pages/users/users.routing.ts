import { Route, Routes } from "@angular/router";
import { ActionTypes } from "src/app/core/models/fixed-value";
import { AdminUserAddResolver, AdminUserResolver, PagePermissionResolver } from "./admin-users.resolver";
import { AddUpdateAdminUserComponent } from "./admin-users/add-update-admin-user/add-update-admin-user.component";
import { AdminUsersComponent } from "./admin-users/admin-users.component";
import { PermissionComponent } from "./permission/permission.component";

export const routes: Routes = [
    {
        path: '', pathMatch:'full', component: AdminUsersComponent, resolve: {
            initialData: AdminUserResolver,
        },
        data: {
            breadcrumb: [
                { name: 'Admin Users' }
            ],
            pageAction: ActionTypes.LIST,
            component:"AdminUsersComponent"
        },
    },
    {
        path: 'add', component: AddUpdateAdminUserComponent, resolve: {
            initialData: AdminUserAddResolver,
        },
        data: {
            breadcrumb: [
                { name: 'Admin Users', path: '../' },
                { name: 'Add Admin User' }
            ],
            pageAction: ActionTypes.ADD,
            component:"AddUpdateAdminUserComponent"
        },
    },
    {
        path: 'edit/:id', component: AddUpdateAdminUserComponent, resolve: {
            initialData: AdminUserAddResolver,
        },
        data: {
            breadcrumb: [
                { name: 'Admin users', path: '../../' },
                { name: 'Edit Admin users' }
            ],
            pageAction: ActionTypes.EDIT,
            component:"AddUpdateAdminUserComponent"
        },
    },
    {
        path: 'permissions', component: PermissionComponent, data: {
            breadcrumb: [{ name: 'User permissions' }],
            pageAction: ActionTypes.LIST,
            component:"PermissionComponent"
        }, resolve: {
            initialData: PagePermissionResolver,
        },
    },
    {
        path: ':id', component: AddUpdateAdminUserComponent, resolve: {
            initialData: AdminUserAddResolver,
        },
        data: {
            breadcrumb: [
                { name: 'Admin users', path: '../../' },
                { name: 'View Admin users' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component:"AddUpdateAdminUserComponent"
        },
    },

    


]