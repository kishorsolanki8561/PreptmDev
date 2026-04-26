import { Routes } from "@angular/router";
import { ActionTypes } from "src/app/core/models/fixed-value";
import { FrontUserReportComponent } from "./front-user-report/front-user-report/front-user-report.component";
import { FeedbackReportResolver, FrontUserReportResolver } from "./reports.resolver";
import { UserFeedbackReportComponent } from "./user-feedback-report/user-feedback-report.component";

export const routes: Routes = [
    {
        path: 'front-user-report', pathMatch: 'full', component: FrontUserReportComponent, resolve: {
            initialData: FrontUserReportResolver,
        },
        data: {
            breadcrumb: [
                { name: 'Front User Report' }
            ],
            pageAction: ActionTypes.LIST,
            component: "FrontUserReportComponent"
        },

    },
    {
        path: 'user-feedback-report', pathMatch: 'full', component: UserFeedbackReportComponent, resolve: {
            initialData: FeedbackReportResolver,
        },
        data: {
            breadcrumb: [
                { name: 'User Feedback Report' }
            ],
            pageAction: ActionTypes.LIST,
            component: "UserFeedbackReportComponent"
        },

    },
]