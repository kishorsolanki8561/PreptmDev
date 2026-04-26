import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { CoreModule } from "src/app/core/core.module";
import { routes } from "./reports.routing";
import { FrontUserReportComponent } from './front-user-report/front-user-report/front-user-report.component';
import { FeedbackReportResolver, FrontUserReportResolver } from "./reports.resolver";
import { UserFeedbackReportComponent } from './user-feedback-report/user-feedback-report.component';

@NgModule({
    declarations: [
    FrontUserReportComponent,
    UserFeedbackReportComponent
  ],
    imports: [
      CommonModule,
      CoreModule,
      RouterModule.forChild(routes)
    ],
    providers: [FrontUserReportResolver,FeedbackReportResolver]
  })
  export class ReportModule { }