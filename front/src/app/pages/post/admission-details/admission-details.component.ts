import { isPlatformServer, PlatformLocation, CommonModule } from '@angular/common';
import { Component, Inject, OnInit, Optional, PLATFORM_ID, Renderer2 } from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { DATE_FORMAT, ExamModeEnum, PreptmLogo } from 'src/app/core/fixed-values';
import { Breadcrumb, ShareContent } from 'src/app/core/models/core.models';
import { AdmissionDetails, Post, PostListFilter } from 'src/app/core/models/post.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

@Component({
  selector: 'preptm-admission-details',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule, CustomAdsComponent, LoaderComponent],
  templateUrl: './admission-details.component.html',
  styleUrls: ['./admission-details.component.scss']
})
export class AdmissionDetailsComponent implements OnInit {

  isMobile = true
  isLoading = false
  private _isServer = false;
  preptmLogo = PreptmLogo

  post: AdmissionDetails | undefined;
  slug: string;
  DATE_FORMAT = DATE_FORMAT
  ExamModeEnum = ExamModeEnum

  breadcrumb: Breadcrumb[] = []

  isBookmarkLoading = false;
  shareContent: ShareContent;
  postListFilter: PostListFilter = new PostListFilter();
  upcommingList: Post[] = [];
  latestList: Post[] = [];
  expiredList: Post[] = [];
  shortDesc: string[] = []
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
    this.lang = this._coreService.getCurrentLang()
    this._route.params.subscribe((params: Params) => {
      this.slug = params['slug'];
      this.getDetails()
    })
  }

  ngOnInit(): void {

  }

  getDetails() {
    this.isLoading = true
    this.post = undefined;
    this._postService.getAdmissionDetails(this.slug).subscribe(res => {
      this.isLoading = false
      if (res.isSuccess) {
        this.post = res.data
        this.shortDesc = res.data.shortDesription?.split('\n') || []
        if (this.post)
          this.shareContent = this.getShareContent(this.post)
        this.breadcrumb = [
          { text: this._coreService.titleCase(this.post.moduleText), path: '/' + this.post.moduleSlug + 's' },
          { text: this.post.title }
        ]
        this.addMetaTags(this.post)
        // this.GetUpCommingPost();
        // this.GetExpirePost();
        // this.GetLatestPost();
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

  getShareContent(data: AdmissionDetails): ShareContent {
    let shareContent = new ShareContent();
    shareContent.admitCardDate = data.admitCardDate;
    shareContent.extendedDate = data.extendedDate;
    shareContent.lastDate = data.lastDate;
    shareContent.link = this.platformLocation.href; // fetch current url
    shareContent.startDate = data.startDate;
    shareContent.title = data.title;
    shareContent.totalPost = data.totalPost?.toString();
    return shareContent
  }

  addMetaTags(blockContaintDetails: AdmissionDetails) {
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

    if (blockContaintDetails.title) {
      this._coreService.setPageTitle(blockContaintDetails.title)
      tags.push({
        property: 'og:title',
        content: blockContaintDetails.title
      })
    }

    if (blockContaintDetails.thumbnail || blockContaintDetails.departmentLogo) {
      tags.push({
        property: 'og:image',
        content: blockContaintDetails.thumbnail || blockContaintDetails.departmentLogo
      });
      tags.push({
        property: 'og:image:alt',
        content: blockContaintDetails.title
      })
    }

    if (blockContaintDetails.keywords) {
      tags.push({
        property: 'keywords',
        content: blockContaintDetails.keywords
      })
    }
    this._coreService.manageMetaTags(tags, this.renderer);
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
  GetUpCommingPost() {
    this.postListFilter.orderBy = 'upcoming';
    this.postListFilter.orderByAsc = 1;
    this.postListFilter.pageSize = 5;
    this.getList({ ...this.postListFilter }, 'upcoming');
  }
  GetLatestPost() {
    this.postListFilter.orderBy = 'latest';
    this.postListFilter.pageSize = 5;
    this.postListFilter.orderByAsc = 0;
    this.getList({ ...this.postListFilter }, 'latest');
  }
  GetExpirePost() {
    this.postListFilter.orderBy = 'expiredsoon';
    this.postListFilter.orderByAsc = 1;
    this.postListFilter.pageSize = 5;
    this.getList({ ...this.postListFilter }, 'expiredsoon');
  }

  getList(payload: PostListFilter, Type: string = '') {
    this._postService.getPostLists(payload).subscribe((res) => {
      if (res.isSuccess) {
        if (Type == 'upcoming') {
          this.upcommingList = res.data ?? [];
          if (this.upcommingList) {
            this.upcommingList = this.upcommingList.filter(s => s.slugUrl != this.post?.slugUrl);
          }
        }
        if (Type == 'latest') {
          this.latestList = res.data ?? [];
          if (this.latestList) {
            this.latestList = this.latestList.filter(s => s.slugUrl != this.post?.slugUrl);
          }
        }
        if (Type == 'expiredsoon') {
          this.expiredList = res.data ?? [];
          if (this.expiredList) {
            this.expiredList = this.expiredList.filter(s => s.slugUrl != this.post?.slugUrl);
          }
        }
      } else {
        this.upcommingList = []
        this.latestList = []
        this.expiredList = []
      }
    })
  }
}
