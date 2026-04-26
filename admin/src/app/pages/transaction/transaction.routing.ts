import { Routes } from "@angular/router";
import { AdditionalPagesComponent } from "./additional-pages/additional-pages.component";
import { ActionTypes } from "src/app/core/models/fixed-value";
import { AddUpdateAdditionalPagesComponent } from "./additional-pages/add-update-additional-pages/add-update-additional-pages.component";
import { AdditionalPagesAddUpdateResolver, AdditionalPagesResolver } from "./additional-pages/additional-pages.resolver";
import { SchemeComponent } from "./scheme/scheme.component";
import { SchemeAddUpdateResolver, SchemeResolver } from "./scheme/scheme-resolver";
import { AddUpdateSchemeComponent } from "./scheme/add-update-scheme/add-update-scheme.component";
import { ArticleComponent } from "./article/article.component";
import { ArticleAddUpdateResolver, ArticleResolver } from "./article/article-resolver";
import { AddUpdateArticleComponent } from "./article/add-update-article/add-update-article.component";
import { PaperComponent } from "./paper/paper.component";
import { AddUpdatePaperComponent } from "./paper/add-update-paper/add-update-paper.component";
import { PaperAddUpdateResolver, PaperResolver } from "./paper/paper-resolver";
import { NotesComponent } from "./notes/notes.component";
import { AddUpdateNotesComponent } from "./notes/add-update-notes/add-update-notes.component";
import { NotesAddUpdateResolver, NotesResolver } from "./notes/notes-resolver";
import { SyllabusComponent } from "./syllabus/syllabus.component";
import { SyllabusAddUpdateResolver, SyllabusResolver } from "./syllabus/syllabus-resolver";
import { AddUpdateSyllabusComponent } from "./syllabus/add-update-syllabus/add-update-syllabus.component";

export const routes: Routes = [
    //#region additional
    {
        path: 'additional-pages', component: AdditionalPagesComponent,
        data: {
            breadcrumb: [
                { name: 'Additional Pages' }
            ],
            pageAction: ActionTypes.LIST,
            component: "AdditionalPagesComponent"
        },
        resolve: {
            initialData: AdditionalPagesResolver,
        },
    },
    {
        path: 'additional-pages/add', component: AddUpdateAdditionalPagesComponent,
        data: {
            breadcrumb: [
                { name: 'Additional Pages', path: '../' },
                { name: 'Add Additional Pages' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateAdditionalPagesComponent"
        },
        resolve: {
            initialData: AdditionalPagesAddUpdateResolver,
        },
    },
    {
        path: 'additional-pages/edit/:id', component: AddUpdateAdditionalPagesComponent,
        data: {
            breadcrumb: [
                { name: 'Additional Pages', path: '../../' },
                { name: 'Edit Additional Pages' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateAdditionalPagesComponent"
        },
        resolve: {
            initialData: AdditionalPagesAddUpdateResolver,
        },
    },

    // {
    //     path: ':id', component: AddUpdateAdditionalPagesComponent,
    //     data: {
    //         breadcrumb: [
    //             { name: 'Additional', path: '../../' },
    //             { name: 'View Additional Pages' }
    //         ],
    //         pageAction: ActionTypes.VIEW_DETAILS,
    //         component: "AddUpdateAdditionalPagesComponent"
    //     },
    //     resolve: {
    //         initialData: AdditionalPagesAddUpdateResolver,
    //     },
    // },
    //#endregion

    //#region Scheme
    {
        path: 'scheme', component: SchemeComponent,
        data: {
            breadcrumb: [
                { name: 'Scheme' }
            ],
            pageAction: ActionTypes.LIST,
            component: "SchemeComponent"
        },
        resolve: {
            initialData: SchemeResolver,
        },
    },
    {
        path: 'scheme/add', component: AddUpdateSchemeComponent,
        data: {
            breadcrumb: [
                { name: 'scheme', path: '../' },
                { name: 'Add Scheme' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateSchemeComponent"
        },
        resolve: {
            initialData: SchemeAddUpdateResolver,
        },
    },
    {
        path: 'scheme/edit/:id', component: AddUpdateSchemeComponent,
        data: {
            breadcrumb: [
                { name: 'Scheme', path: '../../' },
                { name: 'Edit Scheme' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateSchemeComponent"
        },
        resolve: {
            initialData: SchemeAddUpdateResolver,
        },
    },
    {
        path: 'scheme/:id', component: AddUpdateSchemeComponent,
        data: {
            breadcrumb: [
                { name: 'Scheme', path: '../../' },
                { name: 'View Scheme' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateSchemeComponent"
        },
        resolve: {
            initialData: SchemeAddUpdateResolver,
        },
    },
    //#endregion 


    //#region Article
    {
        path: 'article', component: ArticleComponent,
        data: {
            breadcrumb: [
                { name: 'Article' }
            ],
            pageAction: ActionTypes.LIST,
            component: "ArticleComponent"
        },
        resolve: {
            initialData: ArticleResolver,
        },
    },
    {
        path: 'article/add', component: AddUpdateArticleComponent,
        data: {
            breadcrumb: [
                { name: 'Article', path: '../' },
                { name: 'Add Article' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateArticleComponent"
        },
        resolve: {
            initialData: ArticleAddUpdateResolver,
        },
    },
    {
        path: 'article/edit/:id', component: AddUpdateArticleComponent,
        data: {
            breadcrumb: [
                { name: 'Article', path: '../../' },
                { name: 'Edit Article' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateArticleComponent"
        },
        resolve: {
            initialData: ArticleAddUpdateResolver,
        },
    },
    {
        path: 'article/:id', component: AddUpdateArticleComponent,
        data: {
            breadcrumb: [
                { name: 'Article', path: '../../' },
                { name: 'View Article' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateArticleComponent"
        },
        resolve: {
            initialData: ArticleAddUpdateResolver,
        },
    },
    //#endregion

    //#region Paper
    {
        path: 'paper', component: PaperComponent,
        data: {
            breadcrumb: [
                { name: 'Paper' }
            ],
            pageAction: ActionTypes.LIST,
            component: "PaperComponent"
        },
        resolve: {
            initialData: PaperResolver,
        },
    },
    {
        path: 'paper/add', component: AddUpdatePaperComponent,
        data: {
            breadcrumb: [
                { name: 'Paper', path: '../' },
                { name: 'Add Paper' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdatePaperComponent"
        },
        resolve: {
            initialData: PaperAddUpdateResolver,
        },
    },
    {
        path: 'paper/edit/:id', component: AddUpdatePaperComponent,
        data: {
            breadcrumb: [
                { name: 'Paper', path: '../../' },
                { name: 'Edit Paper' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdatePaperComponent"
        },
        resolve: {
            initialData: PaperAddUpdateResolver,
        },
    },
    {
        path: 'paper/:id', component: AddUpdatePaperComponent,
        data: {
            breadcrumb: [
                { name: 'Paper', path: '../../' },
                { name: 'View Paper' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdatePaperComponent"
        },
        resolve: {
            initialData: PaperAddUpdateResolver,
        },
    },
    //#endregion

    //#region Notes
    {
        path: 'notes', component: NotesComponent,
        data: {
            breadcrumb: [
                { name: 'Notes' }
            ],
            pageAction: ActionTypes.LIST,
            component: "NotesComponent"
        },
        resolve: {
            initialData: NotesResolver,
        },
    },
    {
        path: 'notes/add', component: AddUpdateNotesComponent,
        data: {
            breadcrumb: [
                { name: 'Notes', path: '../' },
                { name: 'Add Notes' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateNotesComponent"
        },
        resolve: {
            initialData: SyllabusAddUpdateResolver,
        },
    },
    {
        path: 'notes/edit/:id', component: AddUpdateNotesComponent,
        data: {
            breadcrumb: [
                { name: 'Notes', path: '../../' },
                { name: 'Edit Notes' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateNotesComponent"
        },
        resolve: {
            initialData: NotesAddUpdateResolver,
        },
    },
    {
        path: 'notes/:id', component: AddUpdateNotesComponent,
        data: {
            breadcrumb: [
                { name: 'Notes', path: '../../' },
                { name: 'View Notes' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateNotesComponent"
        },
        resolve: {
            initialData: NotesAddUpdateResolver,
        },
    },
    //#endregion

    //#region Syllabus
    {
        path: 'syllabus', component: SyllabusComponent,
        data: {
            breadcrumb: [
                { name: 'Syllabus' }
            ],
            pageAction: ActionTypes.LIST,
            component: "SyllabusComponent"
        },
        resolve: {
            initialData: SyllabusResolver,
        },
    },
    {
        path: 'syllabus/add', component: AddUpdateSyllabusComponent,
        data: {
            breadcrumb: [
                { name: 'Syllabus', path: '../' },
                { name: 'Add Syllabus' }
            ],
            pageAction: ActionTypes.ADD,
            component: "AddUpdateSyllabusComponent"
        },
        resolve: {
            initialData: SyllabusAddUpdateResolver,
        },
    },
    {
        path: 'syllabus/edit/:id', component: AddUpdateSyllabusComponent,
        data: {
            breadcrumb: [
                { name: 'Syllabus', path: '../../' },
                { name: 'Edit Syllabus' }
            ],
            pageAction: ActionTypes.EDIT,
            component: "AddUpdateSyllabusComponent"
        },
        resolve: {
            initialData: SyllabusAddUpdateResolver,
        },
    },
    {
        path: 'syllabus/:id', component: AddUpdateSyllabusComponent,
        data: {
            breadcrumb: [
                { name: 'Syllabus', path: '../../' },
                { name: 'View Syllabus' }
            ],
            pageAction: ActionTypes.VIEW_DETAILS,
            component: "AddUpdateSyllabusComponent"
        },
        resolve: {
            initialData: SyllabusAddUpdateResolver,
        },
    },
    //#endregion


]