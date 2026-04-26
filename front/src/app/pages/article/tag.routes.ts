import { Routes } from '@angular/router';

export const tagRoutes: Routes = [
  { path: '', loadComponent: () => import('./tags/tags.component').then(m => m.TagsComponent) },
  { path: ':tagTypeSlug', loadComponent: () => import('./article-list/article-list.component').then(m => m.ArticleListComponent) },
];
