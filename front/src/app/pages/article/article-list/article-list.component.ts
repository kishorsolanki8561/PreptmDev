import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { Breadcrumb, PaginationModel } from 'src/app/core/models/core.models';
import { ArticleList, ArticleListFilter } from 'src/app/core/models/post.model';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';

@Component({
  selector: 'preptm-article-list',
  templateUrl: './article-list.component.html',
  standalone: true,
  imports: [CoreModule, CommonModule, RouterModule, LoaderComponent],
  styleUrls: ['./article-list.component.scss']
})
export class ArticleListComponent {
  articleItems: ArticleList[] | [] = [];
  latestArticles: ArticleList[] | [] = [];
  loading = false;
  breadcrumb: Breadcrumb[] = [];

  typeSlug = '';
  moduleName = '';
  payload = new ArticleListFilter();
  pagination = new PaginationModel();

  constructor(
    private _postService: PostService,
    private _route: ActivatedRoute,
    private _coreService: CoreService
  ) {
    this._route.params.subscribe((params: Params) => {
      this.payload.tagTypeSlug     = params['tagTypeSlug']     || '';
      this.payload.articleTypeSlug = params['articleTypeSlug'] || '';
    });

    this._route.queryParams.subscribe((params: Params) => {
      if (params['page']) this.payload.page = +params['page'];
      this.articleList();
    });
  }

  ngOnInit(): void { }

  articleList(): void {
    this.loading = true;
    this._postService.getArticleList(this.payload).subscribe(
      res => {
        this.loading = false;
        if (res.isSuccess) {
          this.articleItems    = [...(res.data?.articles ?? [])];
          this.latestArticles  = [...(res.data?.latestArticle ?? [])];
          this.moduleName      = res.otherData['moduleText'];
          this.pagination.totalItems = res.totalRecords;

          this.breadcrumb = [
            { text: this.payload.tagTypeSlug ? 'Topic' : 'Article', path: '../' },
            { text: this._coreService.titleCase(res?.otherData?.moduleText ?? '') }
          ];
        }
      },
      () => { this.loading = false; }
    );
  }
}
