import { Routes } from '@angular/router';

export const articleRoutes: Routes = [
  { path: '', loadComponent: () => import('./articles/articles.component').then(m => m.ArticlesComponent) },
  { path: ':articleTypeSlug', loadComponent: () => import('./article-list/article-list.component').then(m => m.ArticleListComponent) },
  { path: ':articleTypeSlug/:articleSlug', loadComponent: () => import('./article-details/article-details.component').then(m => m.ArticleDetailsComponent) },
];
