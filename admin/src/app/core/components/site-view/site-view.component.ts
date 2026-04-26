import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-site-view',
  templateUrl: './site-view.component.html',
  styleUrls: ['./site-view.component.scss']
})
export class SiteViewComponent implements OnInit {
  url: any;
  constructor(
    private _route: ActivatedRoute,
    private sanitizer: DomSanitizer
  ) {
    this._route.params.subscribe((data: any) => {
      this.url = `${environment.siteUrl}/${String(data.module).toLowerCase()}/${data.slug}?adminurl=admin.preptm.com`;
      this.url = this.sanitizer.bypassSecurityTrustResourceUrl(this.url)
    });
  }

  ngOnInit(): void {
  }

}
