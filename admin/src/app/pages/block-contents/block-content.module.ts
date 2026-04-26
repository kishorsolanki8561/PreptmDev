import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddUpdateBlockContentsComponent } from './add-update-block-contents/add-update-block-contents.component';
import { BlockContentsComponent } from './block-contents.component';
import { RouterModule } from '@angular/router';
import { routes } from './block-content-routing.module';
import { CoreModule } from 'src/app/core/core.module';
import {
  BlockContentAddUpdateResolver,
  BlockContentResolver,
} from './block-content.resolver';

@NgModule({
  declarations: [BlockContentsComponent, AddUpdateBlockContentsComponent],
  imports: [CommonModule, CoreModule, RouterModule.forChild(routes)],
  providers: [BlockContentAddUpdateResolver, BlockContentResolver],
})
export class BlockContentModule {}
