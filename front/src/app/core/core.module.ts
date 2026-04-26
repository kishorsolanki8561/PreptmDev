import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { PostComponent } from './components/post/post.component';
import { LoaderComponent } from './components/loader/loader.component';
import { LoaderDirective } from './directive/loader.directive';
import { NoRecordsComponent } from './components/no-records/no-records.component';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { ShareButtonsComponent } from './components/share-buttons/share-buttons.component';
import { SafePipe } from './pipes/safe.pipe';
import { ImageComponent } from './components/image/image.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { AdsComponent } from './components/ads/ads.component';

const standaloneItems = [
  LoaderComponent,
  LoaderDirective,
  NoRecordsComponent,
  PostComponent,
  BreadcrumbComponent,
  AdsComponent,
  ImageComponent,
  ShareButtonsComponent,
  SafePipe,
  PaginationComponent,
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    ...standaloneItems,
  ],
  exports: [
    RouterModule,
    PaginationComponent,
    ...standaloneItems,
  ],
})
export class CoreModule { }
