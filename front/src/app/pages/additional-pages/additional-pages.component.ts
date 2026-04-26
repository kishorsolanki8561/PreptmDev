import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { API_ROUTES } from 'src/app/core/api.routes';
import { AdditionalPages } from 'src/app/core/fixed-values';
import { AdditionalPagesService } from 'src/app/core/services/additional-pages.service';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';

@Component({
  selector: 'preptm-additional-pages',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule],
  templateUrl: './additional-pages.component.html',
  styleUrls: ['./additional-pages.component.scss']
})
export class AdditionalPagesComponent implements OnInit {
  pageType = this._route.snapshot.data['type']
  AdditionalPages = AdditionalPages
  data: string = ''
  isLoading = false

  constructor(
    private _route: ActivatedRoute,
    private _additionaPageService:AdditionalPagesService
  ) { }

  ngOnInit(): void {
    this.isLoading = true
    this._additionaPageService.getAdditionalPage(this.pageType).subscribe((resp) => {
      this.isLoading = false
      if (resp.isSuccess) {
        this.data = resp.data as string
      } else {
        this.data = ''
      }
    })

  }

}
