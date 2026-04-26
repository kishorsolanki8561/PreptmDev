import { Routes } from '@angular/router';
import { PostTypesSlug } from 'src/app/core/fixed-values';

export const postRoutes: Routes = [
  { path: PostTypesSlug.RECRUITMENT, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.RECRUITMENT } },
  { path: PostTypesSlug.PRIVATE_RECRUITMENT, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.PRIVATE_RECRUITMENT } },
  { path: PostTypesSlug.SCHEME, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.SCHEME } },
  { path: PostTypesSlug.ADMITCARD, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.ADMITCARD } },
  { path: PostTypesSlug.SYLLABUS, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.SYLLABUS } },
  { path: PostTypesSlug.RESULT, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.RESULT } },
  { path: PostTypesSlug.PAPER, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.PAPER } },
  { path: PostTypesSlug.EXAM, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.EXAM } },
  { path: PostTypesSlug.Answerkey, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.Answerkey } },
  { path: PostTypesSlug.Onlineform, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.Onlineform } },
  { path: PostTypesSlug.ADMISSION, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.ADMISSION } },
  { path: PostTypesSlug.BOOKMARK, loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.BOOKMARK } },
  { path: 'search/:searchedData', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.SEARCH } },
  { path: 'latest', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.LATEST } },
  { path: 'popular', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.POPULAR } },
  { path: 'upcoming', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.UpCominingSoon } },
  { path: 'expiresoon', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.ExpireSoon } },
  { path: ':categorySlug', loadComponent: () => import('./post-list/post-list.component').then(m => m.PostListComponent), data: { type: PostTypesSlug.CATEGORY } },
];
