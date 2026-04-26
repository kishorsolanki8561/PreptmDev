import { Routes } from "@angular/router";
import { ActionTypes } from "src/app/core/models/fixed-value";
import { RecruitmentAddUpdateResolver, RecruitmentResolver } from "./recruitment.resolver";
import { AddUpdateRecruitmentComponent } from "./recruitment/add-update-recruitment/add-update-recruitment.component";
import { RecruitmentComponent } from "./recruitment/recruitment.component";

export const routes: Routes = [
    //#region <recruitment>
    {
        path: 'recruitments', component: RecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Recruitment' }
            ],
            pageAction: ActionTypes.LIST,
            component: "RecruitmentComponent"
        },
        resolve: {
            initialData: RecruitmentResolver,
        },
    },
    {
        path: 'recruitments/add', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Recruitment', path: '../' },
                { name: 'Add recruitment' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateRecruitmentComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    {
        path: 'recruitments/edit/:id', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Recruitment', path: '../../' },
                { name: 'Edit recruitment' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateRecruitmentComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    {
        path: 'recruitments/:id', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Recruitment', path: '../../' },
                { name: 'View recruitment' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateRecruitmentComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    //#endregion


    //#region <admission>
    {
        path: 'admission', component: RecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Admission' }
            ],
            pageAction: ActionTypes.LIST,
            component: "AdmissionComponent"
        },
        resolve: {
            initialData: RecruitmentResolver,
        },
    },
    {
        path: 'admission/add', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Admission', path: '../' },
                { name: 'Add Admission' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateAdmissionComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    {
        path: 'admission/edit/:id', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Admission', path: '../../' },
                { name: 'Edit Admission' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateAdmissionComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    {
        path: 'admission/:id', component: AddUpdateRecruitmentComponent,
        data: {
            breadcrumb: [
                { name: 'Admission', path: '../../' },
                { name: 'View Admission' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateAdmissionComponent"
        },
        resolve: {
            initialData: RecruitmentAddUpdateResolver,
        },
    },
    //#endregion

]