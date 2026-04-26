import { Routes } from "@angular/router";
import { AdditionalPages, PostTypesSlug } from "./core/fixed-values";
import { NoRecordsComponent } from "./core/components/no-records/no-records.component";

export const routes: Routes = [

  { path: 'department/:slug', loadComponent: () => import('./pages/department/department-details/department-details.component').then(m => m.DepartmentDetailsComponent) },

  { path: 'terms-and-conditions', loadComponent: () => import('./pages/additional-pages/additional-pages.component').then(m => m.AdditionalPagesComponent), data: { type: AdditionalPages.TermsConditions } },
  { path: 'privacy-policy', loadComponent: () => import('./pages/additional-pages/additional-pages.component').then(m => m.AdditionalPagesComponent), data: { type: AdditionalPages.PrivacyPolicy } },
  { path: 'about-us', loadComponent: () => import('./pages/additional-pages/additional-pages.component').then(m => m.AdditionalPagesComponent), data: { type: AdditionalPages.AboutUs } },
  { path: 'disclaimer', loadComponent: () => import('./pages/additional-pages/additional-pages.component').then(m => m.AdditionalPagesComponent), data: { type: AdditionalPages.Disclaimer } },
  { path: 'manage-account', loadComponent: () => import('./pages/additional-pages/additional-pages.component').then(m => m.AdditionalPagesComponent), data: { type: AdditionalPages.ManageAccount } },

  { path: 'contact', loadComponent: () => import('./pages/contact-us/contact-us.component').then(m => m.ContactUsComponent) },

  { path: 'article', loadChildren: () => import('./pages/article/article.routes').then(m => m.articleRoutes) },
  { path: 'topic', loadChildren: () => import('./pages/article/tag.routes').then(m => m.tagRoutes) },

  { path: PostTypesSlug.PAPER + '/:slug', loadComponent: () => import('./pages/notes-paper-syllabus/nps-details/nps-details.component').then(m => m.NpsDetailsComponent), data: { type: PostTypesSlug.PAPER } },
  { path: PostTypesSlug.SYLLABUS + '/:slug', loadComponent: () => import('./pages/notes-paper-syllabus/nps-details/nps-details.component').then(m => m.NpsDetailsComponent), data: { type: PostTypesSlug.SYLLABUS } },
  { path: PostTypesSlug.NOTES + '/:slug', loadComponent: () => import('./pages/notes-paper-syllabus/nps-details/nps-details.component').then(m => m.NpsDetailsComponent), data: { type: PostTypesSlug.NOTES } },

  { path: PostTypesSlug.SCHEME + '/:slug', loadComponent: () => import('./pages/post/scheme-details/scheme-details.component').then(m => m.SchemeDetailsComponent) },
  { path: PostTypesSlug.RECRUITMENT + '/:slug', loadComponent: () => import('./pages/post/recruitment-details/recruitment-details.component').then(m => m.RecruitmentDetailsComponent) },
  { path: PostTypesSlug.PRIVATE_RECRUITMENT + '/:slug', loadComponent: () => import('./pages/post/recruitment-details/recruitment-details.component').then(m => m.RecruitmentDetailsComponent) },
  { path: PostTypesSlug.ADMISSION + '/:slug', loadComponent: () => import('./pages/post/admission-details/admission-details.component').then(m => m.AdmissionDetailsComponent) },
  { path: PostTypesSlug.ADMITCARD + '/:slug', loadComponent: () => import('./pages/post/post-containt-details/post-containt-details.component').then(m => m.PostContaintDetailsComponent), data: { type: PostTypesSlug.ADMITCARD } },
  { path: PostTypesSlug.RESULT + '/:slug', loadComponent: () => import('./pages/post/post-containt-details/post-containt-details.component').then(m => m.PostContaintDetailsComponent), data: { type: PostTypesSlug.RESULT } },
  { path: PostTypesSlug.EXAM + '/:slug', loadComponent: () => import('./pages/post/post-containt-details/post-containt-details.component').then(m => m.PostContaintDetailsComponent), data: { type: PostTypesSlug.EXAM } },
  { path: PostTypesSlug.Answerkey + '/:slug', loadComponent: () => import('./pages/post/post-containt-details/post-containt-details.component').then(m => m.PostContaintDetailsComponent), data: { type: PostTypesSlug.Answerkey } },
  { path: PostTypesSlug.Onlineform + '/:slug', loadComponent: () => import('./pages/post/post-containt-details/post-containt-details.component').then(m => m.PostContaintDetailsComponent), data: { type: PostTypesSlug.Onlineform } },

  { path: '', pathMatch: 'full', loadComponent: () => import('./pages/home/home/home.component').then(m => m.HomeComponent) },
  { path: '', loadChildren: () => import('./pages/post/post.routes').then(m => m.postRoutes) },

  { path: '404', component: NoRecordsComponent },
  { path: '**', component: NoRecordsComponent },
];
