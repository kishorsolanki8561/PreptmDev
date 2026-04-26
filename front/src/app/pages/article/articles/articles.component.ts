import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { DdlLookupSlug } from 'src/app/core/fixed-values';
import { Breadcrumb, ddlItem } from 'src/app/core/models/core.models';
import { CoreService } from 'src/app/core/services/core.service';

@Component({
  selector: 'preptm-articles',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule],
  templateUrl: './articles.component.html',
  styleUrls: ['./articles.component.scss']
})
export class ArticlesComponent implements OnInit {
  articleTypeList: ddlItem[] = [];
  isLoading = false;
  breadcrumb: Breadcrumb[] = [{ text: 'Articles', path: '/article' }];

  readonly colors = ['#fff5e5', '#f5f5ff', '#ffe5e5', '#e5ffef', '#e5f4ff', '#fff0f5'];

  constructor(
    private _route: ActivatedRoute,
    private _coreService: CoreService
  ) {}

  getColor(index: number): string {
    return this.colors[index % this.colors.length];
  }

  ngOnInit(): void {
    this._route.params.subscribe((_params: Params) => {
      this.getArticles();
    });
  }

  getArticles() {
    this.isLoading = true;
    this._coreService.GetDDLLookupData(DdlLookupSlug.Article, '', '').subscribe({
      next: (res) => {
        this.isLoading = false;
        this.articleTypeList = res.isSuccess ? (res.data[DdlLookupSlug.Article] ?? []) : [];
      },
      error: () => {
        this.isLoading = false;
        this.articleTypeList = [];
      }
    });
  }
}
