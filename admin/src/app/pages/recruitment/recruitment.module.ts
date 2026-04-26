import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecruitmentComponent } from './recruitment/recruitment.component';
import { AddUpdateRecruitmentComponent } from './recruitment/add-update-recruitment/add-update-recruitment.component';
import { RecruitmentAddUpdateResolver, RecruitmentResolver } from './recruitment.resolver';
import { CoreModule } from 'src/app/core/core.module';
import { RouterModule } from '@angular/router';
import { routes } from './recruitment.routing';

@NgModule({
  declarations: [
    RecruitmentComponent,
    AddUpdateRecruitmentComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    RecruitmentAddUpdateResolver, RecruitmentResolver
  ]
})
export class RecruitmentModule { }
