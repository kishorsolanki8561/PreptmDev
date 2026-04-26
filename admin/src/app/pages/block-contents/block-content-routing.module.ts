import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActionTypes } from 'src/app/core/models/fixed-value';
import { AddUpdateBlockContentsComponent } from './add-update-block-contents/add-update-block-contents.component';
import { BlockContentAddUpdateResolver, BlockContentResolver } from './block-content.resolver';
import { BlockContentsComponent } from './block-contents.component';

export const routes: Routes = [
  {
    path: '',
    component: BlockContentsComponent,
    data: {
      breadcrumb: [{ name: 'BlockContent' }],
      pageAction:ActionTypes.LIST,
      component:"BlockContentsComponent"
    },
    resolve: {
      initialData: BlockContentResolver,
    },
  },
  {
    path: 'add',
    component: AddUpdateBlockContentsComponent,
    data: {
      breadcrumb: [
        { name: 'BlockContent', path: '../' },
        { name: 'Add Block Content' },
      ],
      pageAction:ActionTypes.ADD,
      component:"AddUpdateBlockContentsComponent"
    },
    resolve: {
      initialData: BlockContentAddUpdateResolver,
    },
  },
  {
    path: 'edit/:id',
    component: AddUpdateBlockContentsComponent,
    data: {
      breadcrumb: [
        { name: 'BlockContent', path: '../../' },
        { name: 'Edit Block Content' },
      ],
      pageAction:ActionTypes.EDIT,
      component:"AddUpdateBlockContentsComponent"
    },
    resolve: {
      initialData: BlockContentAddUpdateResolver,
    },
  },
  {
    path: ':id',
    component: AddUpdateBlockContentsComponent,
    data: {
      breadcrumb: [
        { name: 'BlockContent', path: '../../' },
        { name: 'View Block Content' },
      ],
      pageAction:ActionTypes.VIEW_DETAILS,
      component:"AddUpdateBlockContentsComponent"
    },
    resolve: {
      initialData: BlockContentAddUpdateResolver,
    },
  },
];
