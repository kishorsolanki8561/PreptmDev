import { isPlatformServer, PlatformLocation, CommonModule } from '@angular/common';
import { AfterViewInit, Component, Inject, OnInit, Optional, PLATFORM_ID, Renderer2 } from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { DATE_FORMAT, ExamModeEnum, LEVEL, ATTACHMENT_TYPE, PreptmLogo } from 'src/app/core/fixed-values';
import { Breadcrumb, ShareContent } from 'src/app/core/models/core.models';
import { SchemeModel } from 'src/app/core/models/post.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

@Component({
  selector: 'preptm-scheme-details',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule, CustomAdsComponent, LoaderComponent],
  templateUrl: './scheme-details.component.html',
  styleUrls: ['./scheme-details.component.scss']
})
export class SchemeDetailsComponent implements OnInit, AfterViewInit {
  isLoading = false;
  preptmLogo = PreptmLogo

  post: SchemeModel | undefined;
  slug: string;
  DATE_FORMAT = DATE_FORMAT
  ExamModeEnum = ExamModeEnum
  LEVEL = LEVEL
  ATTACHMENT_TYPE = ATTACHMENT_TYPE

  breadcrumb: Breadcrumb[] = []
  shareContent: ShareContent;
  isBookmarkLoading = false;
  shortDesc: string[] = []

  images: any[] = []
  documents: any[] = []
  videos: any[] = []
  lang: string;

  constructor(
    private _postService: PostService,
    private _route: ActivatedRoute,
    private _authService: AuthService,
    private _coreService: CoreService,
    private _alert: AlertService,
    private renderer: Renderer2,
    public platformLocation: PlatformLocation,
    @Inject(PLATFORM_ID) private platformId: Object,
    @Optional() @Inject(SERVER_RESPONSE) private serverResponse: any,
  ) {

    this._route.params.subscribe((params: Params) => {
      this.slug = params['slug'];
      this.getDetails()
    })
    this.lang = this._coreService.getCurrentLang()
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
  }

  addMetaTags(schemeDetails: SchemeModel) {
    let tags: MetaDefinition[] = [];

    tags.push({
      property: 'og:type',
      content: "article"
    })

    tags.push({
      property: 'description',
      content: this.shortDesc[0] || this.post?.title || ''
    })
    tags.push({
      property: 'og:description',
      content: this.shortDesc[0] || this.post?.title || ''
    })

    if (schemeDetails.title) {
      this._coreService.setPageTitle(schemeDetails.title)
      tags.push({
        property: 'og:title',
        content: schemeDetails.title
      })
    }

    if (schemeDetails.thumbnail) {
      tags.push({
        property: 'og:image',
        content: schemeDetails.thumbnail
      })

      tags.push({
        property: 'og:image:alt',
        content: schemeDetails.title
      })
    }

    if (schemeDetails.keywords) {
      tags.push({
        property: 'keywords',
        content: schemeDetails.keywords
      })
    }
    this._coreService.manageMetaTags(tags, this.renderer);
  }

  getShareContent(data: SchemeModel): ShareContent {
    let shareContent = new ShareContent();
    shareContent.extendedDate = data.extendedDate;
    shareContent.lastDate = data.endDate;
    shareContent.link = this.platformLocation.href // fetch current url
    shareContent.startDate = data.startDate;
    shareContent.title = data.title;
    return shareContent
  }
  getDetails() {
    this.isLoading = true
    this.post = undefined;
    this._postService.getSchemeDetails(this.slug).subscribe(res => {
      if (res.isSuccess) {
        this.isLoading = false
        this.post = res.data
        this.shortDesc = res.data.shortDescription?.split('\n') || []
        if (this.post)
          this.shareContent = this.getShareContent(this.post)
        this.breadcrumb = [
          { text: this._coreService.titleCase(this.post.moduleText), path: '/' + this.post.moduleSlug },
          { text: this.post.title }
        ]

        this.images = this.post.attachments.filter(x => x.type === ATTACHMENT_TYPE.IMAGE)
        this.documents = this.post.attachments.filter(x => x.type === ATTACHMENT_TYPE.PDF)
        this.videos = this.post.attachments.filter(x => x.type === ATTACHMENT_TYPE.VIDEO)

        this.addMetaTags(this.post)
      } else {
        this.isLoading = false;
        this.post = undefined;
        if (isPlatformServer(this.platformId) && this.serverResponse) {
          this.serverResponse.status(404);
        }
      }
    }, () => {
      this.isLoading = false;
      if (isPlatformServer(this.platformId) && this.serverResponse) {
        this.serverResponse.status(404);
      }
    })
  }

  manageBookmark(shouldAdd: boolean) {
    if (!this._authService.isUserLoggedIn()) {
      this._alert.info('Please login first');
      return;
    }
    this.isBookmarkLoading = true

    // @ts-ignore
    this._postService.manageBookmark(shouldAdd, this.post).subscribe((response) => {
      this.isBookmarkLoading = false;
      if (response.isSuccess) {
        if (shouldAdd)
          // @ts-ignore
          this.post.bookmarkId = response.data;
        else
          // @ts-ignore
          this.post.bookmarkId = null;
      } else {
        this._alert.info(response.message);
      }
    }, () => {
      this.isBookmarkLoading = false;
    })
  }
}
