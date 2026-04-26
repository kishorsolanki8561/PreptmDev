import { DOCUMENT, CommonModule, isPlatformBrowser, PlatformLocation } from '@angular/common';
import {
  Component, HostListener, Inject, OnDestroy,
  OnInit, PLATFORM_ID, Renderer2
} from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { API_ROUTES } from 'src/app/core/api.routes';
import { DdlLookup, PostTypesSlug } from 'src/app/core/fixed-values';
import { Breadcrumb, ddl, ddlLookup } from 'src/app/core/models/core.models';
import { BookmarkFilter, Post, PostListFilter, Search } from 'src/app/core/models/post.model';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { FormsModule } from '@angular/forms';
import { CoreModule } from 'src/app/core/core.module';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { PostFilterComponent } from './post-filter.component';

@Component({
  selector: 'preptm-post-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, CoreModule,
            LoaderComponent, PostFilterComponent],
  templateUrl: './post-list.component.html',
  styleUrls: ['./post-list.component.scss']
})
export class PostListComponent implements OnInit, OnDestroy {

  list: Post[] = [];
  filter = new PostListFilter();
  loading = false;
  loadingMore = false;
  hasMore = true;
  totalRecords = 0;

  type = '';
  categorySlug = '';
  ddls: ddl | undefined;
  ddlLookup: ddlLookup | undefined;
  search = '';
  postType = PostTypesSlug;
  showFilter = true;
  curModuleText = '';
  breadcrumb: Breadcrumb[] = [];
  ddlLookupEnum = DdlLookup;
  isPrivate?: boolean | null = false;

  private _scrollThrottle: any = null;

  constructor(
    private _postService: PostService,
    private _route: ActivatedRoute,
    private _coreService: CoreService,
    private renderer: Renderer2,
    @Inject(DOCUMENT) public document: any,
    @Inject(PLATFORM_ID) private platformId: Object,
    public platformLocation: PlatformLocation,
  ) {
    this.type = this._route.snapshot.data['type'];
    if (this.type === PostTypesSlug.PRIVATE_RECRUITMENT) this.isPrivate = true;
    else if (this.type === PostTypesSlug.SEARCH) this.isPrivate = null;
  }

  ngOnInit(): void {
    this.filter.searchText = '';
    this._route.params.subscribe((params: Params) => {
      this.categorySlug = params['categorySlug'];
      this._resetList();
      if (this.type === PostTypesSlug.SEARCH) {
        if (params['searchedData']) {
          this.filter.blockTypeSlug = '';
          this.filter.orderBy = 'latest';
          this.filter.orderByAsc = 0;
          this.filter.isPrivate = this.isPrivate;
          this.filter.searchText = this.search = params['searchedData'];
          this._loadPage(false);
        } else {
          this.search = '';
        }
      } else if (this.type === PostTypesSlug.BOOKMARK) {
        this.showFilter = false;
        this._loadBookmarks(false);
      } else {
        this.updateFilter();
        this.getDdls();
      }
    });
  }

  @HostListener('window:scroll')
  onWindowScroll(): void {
    if (!isPlatformBrowser(this.platformId)) return;
    if (!this.hasMore || this.loading || this.loadingMore) return;

    // throttle to max once per 200 ms
    if (this._scrollThrottle) return;
    this._scrollThrottle = setTimeout(() => { this._scrollThrottle = null; }, 200);

    const scrolled  = window.scrollY + window.innerHeight;
    const pageHeight = document.documentElement.scrollHeight;
    if (scrolled >= pageHeight - 400) {
      this.loadMore();
    }
  }

  ngOnDestroy(): void {
    clearTimeout(this._scrollThrottle);
  }

  private _resetList(): void {
    this.list = [];
    this.filter.page = 1;
    this.filter.pageSize = 20;
    this.hasMore = true;
    this.totalRecords = 0;
  }

  updateFilter(): void {
    this.filter.blockTypeSlug = this.type;
    if (this.type === PostTypesSlug.POPULAR) {
      this.filter.orderBy = 'popular'; this.filter.blockTypeSlug = '';
    }
    if (this.type === PostTypesSlug.UpCominingSoon) {
      this.filter.orderBy = 'upcoming'; this.filter.orderByAsc = 1; this.filter.blockTypeSlug = '';
    }
    if (this.type === PostTypesSlug.ExpireSoon) {
      this.filter.orderBy = 'expiredsoon'; this.filter.blockTypeSlug = ''; this.filter.orderByAsc = 1;
    }
    if (this.type === PostTypesSlug.LATEST) {
      this.filter.blockTypeSlug = ''; this.filter.orderBy = 'latest';
    }
    if (this.type === PostTypesSlug.CATEGORY) {
      this.filter.categorySlug = this.categorySlug;
      this.filter.categoryId = 0; this.filter.blockTypeSlug = '';
    }
    if (this.isPrivate) {
      this.filter.isPrivate = true;
      this.filter.blockTypeSlug = PostTypesSlug.RECRUITMENT;
    }
    if (this.type === PostTypesSlug.SCHEME) this.getDdlLookupData();
    this.filter.isPrivate = this.isPrivate;
    this._loadPage(false);
  }

  loadMore(): void {
    if (this.loadingMore || !this.hasMore) return;
    this.filter.page = (this.filter.page || 1) + 1;
    this._loadPage(true);
  }

  private _loadPage(append: boolean): void {
    if (append) this.loadingMore = true;
    else { this.loading = true; this.list = []; }

    this._postService.getPostLists({ ...this.filter }).subscribe({
      next: (res) => {
        this.loading = false;
        this.loadingMore = false;
        if (res.isSuccess) {
          const newItems: Post[] = res.data ?? [];
          this.list = append ? [...this.list, ...newItems] : newItems;
          this.totalRecords = res.totalRecords ?? 0;
          this.hasMore = this.list.length < this.totalRecords;
          this.showFilter = this.totalRecords > 0;
          this._coreService.addCommonTags(this.renderer);

          this.curModuleText = res?.otherData?.ModuleText || '';
          const pageName = this.type === PostTypesSlug.SEARCH ? 'Search'
            : this.isPrivate ? 'Private Jobs'
            : (res?.otherData?.ModuleText ?? '');
          this.breadcrumb = [{ text: this._coreService.titleCase(pageName) }];
          this._addPageMeta(pageName);
        } else {
          if (!append) { this.list = []; this.showFilter = false; }
          this.hasMore = false;
        }
      },
      error: () => { this.loading = false; this.loadingMore = false; }
    });
  }

  // kept for backwards compat — redirects to _loadPage
  getList(payload: PostListFilter): void {
    this.filter = { ...this.filter, ...payload };
    this._resetList();
    this._loadPage(false);
  }

  private _addPageMeta(pageName: string): void {
    if (!pageName) return;
    const title = `${this._coreService.titleCase(pageName)} | Preptm`;
    const desc = `Find the latest ${pageName} notifications, eligibility, apply online links and important dates on Preptm.`;
    this._coreService.setPageTitle(title);
    const tags: MetaDefinition[] = [
      { property: 'og:type',        content: 'website' },
      { property: 'og:title',       content: title },
      { property: 'description',    content: desc },
      { property: 'og:description', content: desc },
    ];
    this._coreService.manageMetaTags(tags, this.renderer);
  }

  getDdls(): void {
    this._coreService.getDdl(API_ROUTES.post.filterDdl).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.ddls = res.data;
          if (this.categorySlug) {
            const cat = this.ddls!['ddlCategory'].find(
              (i: any) => i.otherData.slugUrl.toLowerCase() === this.categorySlug.toLowerCase()
            );
            this.filter.categoryId = cat?.value || 0;
          }
        } else { this.ddls = undefined; }
      },
      error: () => { this.loading = false; }
    });
  }

  getDdlLookupData(): void {
    this._coreService.GetDDLLookupData('', '', `${this.ddlLookupEnum.SchemeEligibility}`).subscribe({
      next: (res) => { this.ddlLookup = res.isSuccess ? res.data : undefined; },
      error: () => {}
    });
  }

  private _loadBookmarks(append: boolean): void {
    if (append) this.loadingMore = true;
    else { this.loading = true; this.list = []; }

    const payload = new BookmarkFilter();
    payload.page = this.filter.page;
    payload.pageSize = this.filter.pageSize;

    this._postService.bookmarks(payload).subscribe({
      next: (res) => {
        this.loading = false; this.loadingMore = false;
        if (res.isSuccess && res.totalRecords > 0) {
          this.list = append ? [...this.list, ...(res.data ?? [])] : (res.data ?? []);
          this.totalRecords = res.totalRecords;
          this.hasMore = this.list.length < this.totalRecords;
        } else {
          if (!append) this.list = [];
          this.hasMore = false;
        }
      },
      error: () => { this.loading = false; this.loadingMore = false; }
    });
  }

  filterList(): void {
    this.filter.departmentId    = this.filter.departmentId    || 0;
    this.filter.jobDesignationId = this.filter.jobDesignationId || 0;
    this.filter.qualificationId = this.filter.qualificationId || 0;
    this._resetList();
    this._loadPage(false);
  }

  clearAll(): void {
    this.filter.departmentId    = 0;
    this.filter.jobDesignationId = 0;
    this.filter.qualificationId = 0;
    this.filter.title           = '';
    this.filter.eligibilityId   = 0;
    if (!this.categorySlug) this.filter.categoryId = 0;
    this._resetList();
    this._loadPage(false);
  }

  getSearchedData(): void {
    this._resetList();
    this._loadPage(false);
  }
}
