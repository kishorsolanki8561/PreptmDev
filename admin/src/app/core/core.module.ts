import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzProgressModule } from 'ng-zorro-antd/progress';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { NzListModule } from 'ng-zorro-antd/list';
import { SafePipeModule } from 'safe-pipe';
import { NzTabsModule } from 'ng-zorro-antd/tabs';


import { RemoveWrapperDirective } from './directives/remove-wrapper.directive';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { ButtonComponent } from './components/button/button.component';
import { TableComponent } from './components/table/table.component';
import { SwitchComponent } from './components/form-controls/switch/switch.component';
import { ErrorMessagesComponent } from './components/form-controls/error-messages/error-messages.component';
import { TextComponent } from './components/form-controls/text/text.component';
import { SelectComponent } from './components/form-controls/select/select.component';
import { FormComponent } from './components/form/form.component';
import { UploaderComponent } from './components/form-controls/uploader/uploader.component';
import { AbsPath, FindValuePipe, ReplacePipe, ToObjUrlPipe } from './pipes/to-obj-url.pipe';
import { DateComponent } from './components/form-controls/date/date.component';
import { TextareaComponent } from './components/form-controls/textarea/textarea.component';
import { HtmlEditorComponent } from './components/form-controls/html-editor/html-editor.component';
import { ProgressBarComponent } from './components/progress-bar/progress-bar.component';
import { RouterModule } from '@angular/router';
import { AddUpdateCategoryMasterComponent } from '../pages/master/category-master/add-update-category-master/add-update-category-master.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzAnchorModule } from 'ng-zorro-antd/anchor';
import { NgxSummernoteModule } from 'ngx-summernote';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { SiteViewComponent } from './components/site-view/site-view.component';
import { ImageCropperModule } from 'ngx-image-cropper';
import { RadioComponent } from './components/form-controls/radio/radio.component';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { TableFormComponent } from './components/form-controls/table-form/table-form.component';
const components = [
  PageHeaderComponent,
  ButtonComponent,
  TableComponent,
  SwitchComponent,
  ErrorMessagesComponent,
  TextComponent,
  TableFormComponent,
  SelectComponent,
  FormComponent,
  UploaderComponent,
  ToObjUrlPipe,
  ReplacePipe,
  DateComponent,
  TextareaComponent,
  HtmlEditorComponent,
  ProgressBarComponent,
  AddUpdateCategoryMasterComponent,
  FindValuePipe,
  AbsPath
]

const modules = [
  FormsModule,
  ReactiveFormsModule,
  HttpClientModule,
  NzIconModule,
  NzTableModule,
  NzDropDownModule,
  NzSwitchModule,
  NzToolTipModule,
  NzPopconfirmModule,
  NzInputModule,
  NzButtonModule,
  NzSelectModule,
  NzSpaceModule,
  NzMessageModule,
  NzDividerModule,
  NzModalModule,
  NzImageModule,
  NzAvatarModule,
  NzPopoverModule,
  NzListModule,
  SafePipeModule,
  CKEditorModule,
  NzTabsModule,
  NzCollapseModule,
  NzAnchorModule,
  NzUploadModule,
  NgxSummernoteModule,
  NzTypographyModule,
  NzRadioModule
]

@NgModule({
  declarations: [
    components,
    RemoveWrapperDirective,
    SiteViewComponent,
    RadioComponent,


  ],
  imports: [
    CommonModule,
    modules,
    NzBreadCrumbModule,
    NzUploadModule,
    NzDatePickerModule,
    NzProgressModule,
    RouterModule,
    ImageCropperModule,

  ],
  exports: [
    modules,
    components,

  ],
  providers: [ToObjUrlPipe, ReplacePipe, FindValuePipe]
})
export class CoreModule { }
