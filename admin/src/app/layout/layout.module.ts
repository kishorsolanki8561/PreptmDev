import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmptyLayoutComponent } from './empty-layout/empty-layout.component';
import { LayoutComponent } from './layout/layout.component';
import { RouterModule } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { CoreModule } from '../core/core.module';


import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { MenuComponent } from './layout/menu/menu.component';
 
@NgModule({
  declarations: [
    EmptyLayoutComponent,
    LayoutComponent,
    HeaderComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    CoreModule,
    NzLayoutModule,
    NzMenuModule

  ],
  exports: [
    EmptyLayoutComponent,
    LayoutComponent,
  ]
})
export class LayoutModule { }
