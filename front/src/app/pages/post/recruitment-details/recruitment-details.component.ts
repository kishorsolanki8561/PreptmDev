import { Component, OnInit, Renderer2, ViewChild, Inject, Optional, PLATFORM_ID } from '@angular/core';
import { DomSanitizer, MetaDefinition, SafeHtml } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { DATE_FORMAT, ExamModeEnum, PostTypesSlug, PreptmLogo } from 'src/app/core/fixed-values';
import { Breadcrumb, ShareContent } from 'src/app/core/models/core.models';
import { Post, PostListFilter, RecruitmentDetails } from 'src/app/core/models/post.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { isPlatformServer, PlatformLocation, CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';
@Component({
  selector: 'preptm-recruitment-details',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule, CustomAdsComponent, LoaderComponent],
  templateUrl: './recruitment-details.component.html',
  styleUrls: ['./recruitment-details.component.scss']
})
export class RecruitmentDetailsComponent implements OnInit {
  @ViewChild('descHtml') descHtml: any

  isLoading = false;

  post: RecruitmentDetails | undefined;
  slug: string;
  DATE_FORMAT = DATE_FORMAT
  ExamModeEnum = ExamModeEnum
  currentUrl = '';
  breadcrumb: Breadcrumb[] = []
  jsonLD: SafeHtml;
  json: Object

  isBookmarkLoading = false;
  shareContent: ShareContent;

  postListFilter: PostListFilter = new PostListFilter();
  upcommingList: Post[] = [];
  latestList: Post[] = [];
  expiredList: Post[] = [];
  shortDesc: string[] = []
  activeTab: 'upcoming' | 'latest' | 'expired' = 'upcoming';

  constructor(
    private _postService: PostService,
    private _route: ActivatedRoute,
    private _authService: AuthService,
    private _coreService: CoreService,
    private _alert: AlertService,
    private renderer: Renderer2,
    private platformLocation: PlatformLocation,
    private _sanitizer: DomSanitizer,
    @Inject(PLATFORM_ID) private platformId: Object,
    @Optional() @Inject(SERVER_RESPONSE) private serverResponse: any,
  ) {
  }

  ngOnInit(): void {

    this._route.params.subscribe((params: Params) => {
      this.slug = params['slug'];
      this.getDetails();

    });


  }

  getDetails() {
    this.isLoading = true
    this.post = undefined;
    this._postService.getRecruitmentDetails(this.slug).subscribe(res => {
      if (res.isSuccess && res.data) {
        this.isLoading = false
        this.post = res.data
        this.shortDesc = res.data.shortDesription?.split('\n') || []
        if (this.post)
          this.shareContent = this.getShareContent(this.post)

        const isPrivate = this.post.isPrivate;
        this.breadcrumb = [
          { text: this._coreService.titleCase(isPrivate ? "Private Jobs" : this.post.moduleText), path: '/' + (isPrivate ? PostTypesSlug.PRIVATE_RECRUITMENT : this.post.moduleSlug) },
          { text: this.post.title }
        ];

        this.addMetaTags(this.post)

        this.jsonLD = this.getSafeHTML(this.getJsonLdData());

      } else {
        this.isLoading = false;
        this.post = undefined;
        if (isPlatformServer(this.platformId) && this.serverResponse) {
          this.serverResponse.status(404);
        }
      }
    }, (err) => {
      this.isLoading = false;
      if (isPlatformServer(this.platformId) && this.serverResponse) {
        this.serverResponse.status(404);
      }
    })
    this.GetUpCommingPost();
    this.GetExpirePost();
    this.GetLatestPost();
  }

  addMetaTags(recruitmentDetails: RecruitmentDetails) {
    let tags: MetaDefinition[] = [];

    tags.push({
      property: 'og:type',
      content: "article"
    })

    tags.push({
      property: 'description',
      content: this.shortDesc[0] ?? recruitmentDetails.title
    })
    tags.push({
      property: 'og:description',
      content: this.shortDesc[0] ?? recruitmentDetails.title
    })

    if (recruitmentDetails.title) {
      this._coreService.setPageTitle(recruitmentDetails.title)
      tags.push({
        property: 'og:title',
        content: recruitmentDetails.title
      })
    }

    if (recruitmentDetails.thumbnail || recruitmentDetails.departmentLogo) {
      tags.push({
        property: 'og:image',
        content: recruitmentDetails.thumbnail || recruitmentDetails.departmentLogo
      })

      tags.push({
        property: 'og:image:alt',
        content: recruitmentDetails.title
      })

      tags.push({
        property: 'og:image:type',
        content: 'image/webp'
      })
    }

    if (recruitmentDetails.keywords) {
      tags.push({
        property: 'keywords',
        content: recruitmentDetails.keywords
      })
    }
    this._coreService.manageMetaTags(tags, this.renderer);
  }

  getShareContent(data: RecruitmentDetails): ShareContent {
    let shareContent = new ShareContent();
    shareContent.admitCardDate = data.admitCardDate;
    shareContent.extendedDate = data.extendedDate;
    shareContent.lastDate = data.lastDate;
    shareContent.link = this.platformLocation.href // fetch current url
    shareContent.startDate = data.startDate;
    shareContent.title = data.title;
    shareContent.totalPost = data.totalPost?.toString();
    return shareContent
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
        if (Type === 'upcoming') {
          this.upcommingList = res.data ?? [];
          if (this.upcommingList) {
            this.upcommingList = this.upcommingList.filter(s => s.slugUrl != this.post?.slugUrl);
          }
        }
        if (Type === 'latest') {
          this.latestList = res.data ?? [];
          if (this.latestList) {
            this.latestList = this.latestList.filter(s => s.slugUrl != this.post?.slugUrl);
          }
        }
        if (Type === 'expiredsoon') {
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


  getSafeHTML(value: {}) {
    const json = JSON.stringify(value, null, 2);
    const html = `<script type="application/ld+json">${json}</script>`;
    // Inject to inner html without Angular stripping out content
    return this._sanitizer.bypassSecurityTrustHtml(html);
  }

  getJsonLdData() {
    return {
      "@context": "https://schema.org",
      "articleBody": this.post?.description.replace(/<[^>]*>/g, '') || undefined,
      "@type": "Article",
      "mainEntityOfPage": this.platformLocation.href,
      "inLanguage": this._coreService.getCurrentLang(),
      "headline": this.post?.title,
      "keywords": this.post?.keywords,
      "url": this.platformLocation.href,
      "datePublished": this.post?.publishedDate + '+05:30',
      "description": this.shortDesc.join(' '),
      "thumbnailUrl": this.post?.thumbnail,
      "author": {
        "@type": "Person",
        "name": "Suresh",
        "url": 'https://www.preptm.com/assets/img/writter.jpg'
      },
      "publisher": {
        "@type": "Organization",
        "name": "Preptm",
        "url": "https://www.preptm.com",
        "logo": {
          "@type": "ImageObject",
          "url": PreptmLogo
        }
      },
      "image": {
        "@type": "ImageObject",
        "url": this.post?.thumbnail
      }
    }
  }
}
