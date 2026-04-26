import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArticleFilterDTO, ArticleResponseDTO, ArticleViewListDTO } from 'src/app/core/models/article.model';
import { ApiResponseModel, Breadcrumb, columns } from 'src/app/core/models/core.model';
import { progressDdl } from 'src/app/core/models/fixed-value';
import { ArticleService } from 'src/app/core/services/article.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.scss']
})
export class ArticleComponent implements OnInit {
  initialData: ApiResponseModel<ArticleViewListDTO[]>;
  filter: ArticleFilterDTO = new ArticleFilterDTO();
  breadcrumb: Breadcrumb[] = [];
  columns: columns;

  constructor( 
    private _route: ActivatedRoute,
    public _articleSrevice : ArticleService) {
      this.initialData = this._route.snapshot.data['initialData'].article;
      this.addColumns()
     }

  ngOnInit(): void {
    this._route.data.subscribe((data) => {
      this.breadcrumb = data['breadcrumb'];
    });
  }

  addColumns() {
    this.columns = [
      {
        columnKey: 'title',
        columnText: 'Title',
        searchType: 'text',

      },
      {
        columnKey: 'visitCount',
        columnText: 'Visit',
      }
      ,
      {
        columnKey: 'publisherName',
        columnText: 'Publisher',
        searchType: 'singleSelect',
      },

      {
        columnKey: 'modifiedByName',
        columnText: 'Modified By',
      },
      {
        columnKey: 'modifiedDate',
        columnText: 'Modified Date',
        sorting: true,
        type: 'date',
      },
      {
        columnKey: 'status',
        columnText: 'Update Progress',
        type: 'updateProgress',
        statusDdl: progressDdl,
        columnWidth: '150px',
        columnAlign: 'right',
      },
      // {
      //   columnKey: 'slugUrl',
      //   columnText: 'Slug Url',
      //   type: 'siteView',
      //   isHide: true,
      // },
    ];
  }
}
