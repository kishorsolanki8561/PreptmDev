import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdditionalPagesComponent } from './additional-pages/additional-pages.component';
import { AddUpdateAdditionalPagesComponent } from './additional-pages/add-update-additional-pages/add-update-additional-pages.component';
import { RouterModule } from '@angular/router';
import { routes } from './transaction.routing';
import { CoreModule } from 'src/app/core/core.module';
import { AdditionalPagesAddUpdateResolver, AdditionalPagesResolver } from './additional-pages/additional-pages.resolver';
import { SchemeComponent } from './scheme/scheme.component';
import { AddUpdateSchemeComponent } from './scheme/add-update-scheme/add-update-scheme.component';
import { SchemeAddUpdateResolver, SchemeResolver } from './scheme/scheme-resolver';
import { ArticleComponent } from './article/article.component';
import { AddUpdateArticleComponent } from './article/add-update-article/add-update-article.component';
import { ArticleAddUpdateResolver, ArticleResolver } from './article/article-resolver';
import { PaperComponent } from './paper/paper.component';
import { AddUpdatePaperComponent } from './paper/add-update-paper/add-update-paper.component';
import { PaperAddUpdateResolver, PaperResolver } from './paper/paper-resolver';
import { NotesComponent } from './notes/notes.component';
import { AddUpdateNotesComponent } from './notes/add-update-notes/add-update-notes.component';
import { NotesAddUpdateResolver, NotesResolver } from './notes/notes-resolver';
import { SyllabusAddUpdateResolver, SyllabusResolver } from './syllabus/syllabus-resolver';
import { SyllabusComponent } from './syllabus/syllabus.component';
import { AddUpdateSyllabusComponent } from './syllabus/add-update-syllabus/add-update-syllabus.component';



@NgModule({
  declarations: [
    AdditionalPagesComponent,
    AddUpdateAdditionalPagesComponent,
    SchemeComponent,
    AddUpdateSchemeComponent,
    ArticleComponent,
    AddUpdateArticleComponent,
    PaperComponent,
    AddUpdatePaperComponent,
    NotesComponent,
    AddUpdateNotesComponent,
    SyllabusComponent,
    AddUpdateSyllabusComponent,
  ],
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(routes)
  ],
  providers: [
    AdditionalPagesAddUpdateResolver, AdditionalPagesResolver, SchemeResolver, SchemeAddUpdateResolver
    , ArticleResolver, ArticleAddUpdateResolver, PaperResolver, PaperAddUpdateResolver, NotesResolver, NotesAddUpdateResolver,
    SyllabusResolver, SyllabusAddUpdateResolver
  ]
})
export class TransactionModule { }
