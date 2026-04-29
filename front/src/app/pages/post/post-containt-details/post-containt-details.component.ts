import { isPlatformBrowser, isPlatformServer, PlatformLocation, CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy, Component, inject, OnInit,
  PLATFORM_ID, Renderer2, signal
} from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { DATE_FORMAT, ExamModeEnum, PreptmLogo } from 'src/app/core/fixed-values';
import { Breadcrumb, ShareContent } from 'src/app/core/models/core.models';
import { BlockContaintDetails, Post, PostListFilter } from 'src/app/core/models/post.model';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

@Component({
  selector: 'preptm-post-containt-details',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, RouterModule, CoreModule, CustomAdsComponent],
  templateUrl: './post-containt-details.component.html',
  styleUrls: ['./post-containt-details.component.scss']
})
export class PostContaintDetailsComponent implements OnInit {
  private readonly _postService = inject(PostService);
  private readonly _route = inject(ActivatedRoute);
  private readonly _authService = inject(AuthService);
  private readonly _coreService = inject(CoreService);
  private readonly renderer = inject(Renderer2);
  readonly platformLocation = inject(PlatformLocation);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly serverResponse = inject(SERVER_RESPONSE, { optional: true });

  readonly post = signal<BlockContaintDetails | undefined>(undefined);
  readonly isLoading = signal(false);
  readonly isBookmarkLoading = signal(false);
  readonly shareContent = signal<ShareContent | undefined>(undefined);
  readonly breadcrumb = signal<Breadcrumb[]>([]);
  readonly shortDesc = signal<string[]>([]);
  readonly lang = signal('');
  readonly upcommingList = signal<Post[]>([]);
  readonly latestList = signal<Post[]>([]);
  readonly expiredList = signal<Post[]>([]);

  readonly DATE_FORMAT = DATE_FORMAT;
  readonly ExamModeEnum = ExamModeEnum;
  readonly preptmLogo = PreptmLogo;

  private readonly postListFilter: PostListFilter = new PostListFilter();

  ngOnInit(): void {
    this.lang.set(this._coreService.getCurrentLang());
    this._route.params.subscribe((params: Params) => {
      this.getBlockContaintDetails(params['slug']);
    });
  }

  private getBlockContaintDetails(slug: string): void {
    this.post.set(undefined);
    if (isPlatformBrowser(this.platformId)) {
      this.isLoading.set(true);
    }
    this._postService.getBlockContaintDetails(slug).subscribe({
      next: (res) => {
        this.isLoading.set(false);
        if (res.isSuccess && res.data) {
          this.post.set(res.data);
          this.shortDesc.set(res.data.summary?.split('\n') ?? []);
          this.shareContent.set(this.buildShareContent(res.data));
          this.breadcrumb.set([
            { text: this._coreService.titleCase(res.data.moduleText), path: '/' + res.data.moduleSlug },
            { text: res.data.title }
          ]);
          this.addMetaTags(res.data);
        } else {
          this.post.set(undefined);
          if (isPlatformServer(this.platformId) && this.serverResponse) {
            this.serverResponse.status(404);
          }
        }
      },
      error: () => {
        this.isLoading.set(false);
        if (isPlatformServer(this.platformId) && this.serverResponse) {
          this.serverResponse.status(404);
        }
      }
    });
  }

  private addMetaTags(data: BlockContaintDetails): void {
    const desc = this.shortDesc()[0] || data.title || '';
    const tags: MetaDefinition[] = [
      { property: 'og:type', content: 'article' },
      { property: 'description', content: desc },
      { property: 'og:description', content: desc },
    ];
    if (data.title) {
      this._coreService.setPageTitle(data.title);
      tags.push({ property: 'og:title', content: data.title });
    }
    if (data.thumbnail || data.departmentLogo) {
      tags.push({ property: 'og:image', content: data.thumbnail || data.departmentLogo });
      tags.push({ property: 'og:image:alt', content: data.title });
    }
    if (data.keywords) {
      tags.push({ property: 'keywords', content: data.keywords });
    }
    this._coreService.manageMetaTags(tags, this.renderer);
  }

  private buildShareContent(data: BlockContaintDetails): ShareContent {
    const sc = new ShareContent();
    sc.extendedDate = data.extendedDate;
    sc.lastDate = data.lastDate;
    sc.link = this.platformLocation.href;
    sc.startDate = data.startDate;
    sc.FeeLastDate = data.feePaymentLastDate;
    sc.title = data.title;
    return sc;
  }

  manageBookmark(shouldAdd: boolean): void {
    if (!this._authService.isUserLoggedIn()) {
      if (isPlatformBrowser(this.platformId)) alert('Please login first');
      return;
    }
    const currentPost = this.post();
    if (!currentPost) return;
    this.isBookmarkLoading.set(true);

    this._postService.manageBookmark(shouldAdd, currentPost).subscribe({
      next: (response) => {
        this.isBookmarkLoading.set(false);
        if (response.isSuccess) {
          this.post.update(p => p ? { ...p, bookmarkId: shouldAdd ? response.data : null } : p);
        } else {
          if (isPlatformBrowser(this.platformId)) alert(response.message);
        }
      },
      error: () => this.isBookmarkLoading.set(false)
    });
  }

  GetUpCommingPost(): void {
    this.postListFilter.orderBy = 'upcoming';
    this.postListFilter.orderByAsc = 1;
    this.postListFilter.pageSize = 5;
    this.getList({ ...this.postListFilter }, 'upcoming');
  }

  GetLatestPost(): void {
    this.postListFilter.orderBy = 'latest';
    this.postListFilter.pageSize = 5;
    this.postListFilter.orderByAsc = 0;
    this.getList({ ...this.postListFilter }, 'latest');
  }

  GetExpirePost(): void {
    this.postListFilter.orderBy = 'expiredsoon';
    this.postListFilter.orderByAsc = 1;
    this.postListFilter.pageSize = 5;
    this.getList({ ...this.postListFilter }, 'expiredsoon');
  }

  private getList(payload: PostListFilter, type: string): void {
    this._postService.getPostLists(payload).subscribe((res) => {
      if (res.isSuccess) {
        const title = this.post()?.title;
        const filtered = (res.data ?? []).filter(s => s.title !== title);
        if (type === 'upcoming') this.upcommingList.set(filtered);
        else if (type === 'latest') this.latestList.set(filtered);
        else if (type === 'expiredsoon') this.expiredList.set(filtered);
      } else {
        this.upcommingList.set([]);
        this.latestList.set([]);
        this.expiredList.set([]);
      }
    });
  }
}
