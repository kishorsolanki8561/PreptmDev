import { Routes } from '@angular/router';
import { AuthGuard, PermissionGuard } from './core/guard/auth.guard';
import { NoAuthGuard } from './core/guard/noAuth.guard';
import { EmptyLayoutComponent } from './layout/empty-layout/empty-layout.component';
import { LayoutComponent } from './layout/layout/layout.component';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { UnauthorizedComponent } from './pages/unauthorized/unauthorized.component';
import { TestingComponent } from './testing/testing.component';
import { SiteViewComponent } from './core/components/site-view/site-view.component';



export const routes: Routes = [
    // Auth routes for guests
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'test', component: TestingComponent },
    {
        path: '',
        component: EmptyLayoutComponent,
        canActivate: [NoAuthGuard],
        canActivateChild: [NoAuthGuard],
        children: [
            { path: 'sign-in', loadChildren: () => import("./pages/sign-in/sign-in.module").then(m => m.SignInModule) },
        ]
    },

    // Auth routes for authenticated users
    {
        path: '',
        component: LayoutComponent,
        canActivate: [AuthGuard],
        canActivateChild: [AuthGuard, PermissionGuard],
        children: [
            { path: 'dashboard', loadChildren: () => import("./pages/dashboard/dashboard.module").then(m => m.DashboardModule) },
            { path: 'users', loadChildren: () => import("./pages/users/users.module").then(m => m.UsersModule) },
            { path: 'report', loadChildren: () => import("./pages/reports/reports.module").then(m => m.ReportModule) },
            { path: 'master', loadChildren: () => import("./pages/master/master.module").then(m => m.MasterModule) },
            { path: 'post', loadChildren: () => import("./pages/recruitment/recruitment.module").then(m => m.RecruitmentModule) },
            { path: 'blockcontent', loadChildren: () => import("./pages/block-contents/block-content.module").then(m => m.BlockContentModule) },
            { path: 'transaction', loadChildren: () => import("./pages/transaction/transaction.module").then(m => m.TransactionModule) },

        ]
    },
    {
        path: 'siteview/:module/:slug', component: SiteViewComponent, canActivate: [AuthGuard],
        canActivateChild: [AuthGuard, PermissionGuard],
    },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: '404', component: PageNotFoundComponent },
    { path: '**', component: PageNotFoundComponent }

];