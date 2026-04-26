import { CommonModule, isPlatformBrowser } from '@angular/common';
import { AfterViewInit, ChangeDetectorRef, Component, ElementRef, Inject, OnInit, PLATFORM_ID, Renderer2, ViewChild } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, NavigationEnd, Params, Router, RouterModule } from '@angular/router';
import { debounceTime, of, switchMap } from 'rxjs';
import { ENABLE_LOGIN } from 'src/app/core/fixed-values';
import { LoginReq } from 'src/app/core/models/auth.model';
import { user } from 'src/app/core/models/user.model';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { SearchService } from 'src/app/core/services/search.service';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { LoaderDirective } from 'src/app/core/directive/loader.directive';

@Component({
  selector: 'preptm-header',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    LoaderComponent,
    LoaderDirective
  ],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit, AfterViewInit {

  ENABLE_LOGIN = ENABLE_LOGIN;
  keyword = 'title';
  showSearchList = false;
  isSearchLoading = false;
  historySerachText: string[] = [];
  searchTextList: string[] = [];
  searchText = new FormControl('');
  ishistory = true;
  isLoginLoading = false;
  isMobileMenuOpen = false;
  isSidebarOpen = false;
  lastSearchedText = '';
  showProfileMenu = false;
  isLangChanged = false;
  curLang: any;
  userDetails: user | null = null;
  isSearchCompleted = false;

  @ViewChild('toggleButton') toggleButton!: ElementRef;
  @ViewChild('searchDiv') searchDiv!: ElementRef;

  constructor(
    private _searchService: SearchService,
    private renderer: Renderer2,
    private _coreService: CoreService,
    private _authService: AuthService,
    private _router: Router,
    private _route: ActivatedRoute,
    private _cd: ChangeDetectorRef,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    if (_coreService.checkIsClientSide()) {
      this.historySerachText = [..._coreService.getLocalStorage('serach') || []];
    }
  }

  ngOnInit(): void {
    this.curLang = this._coreService.getCurrentLang();
    this.userDetails = this._authService.getUserDetails();

    this._router.events.subscribe((e) => {
      let currentRoute = this._route.root;
      while (currentRoute.children[0] !== undefined) {
        currentRoute = currentRoute.children[0];
      }
      if (e instanceof NavigationEnd) {
        const searchedVal = currentRoute.snapshot.params['searchedData'];
        this.searchText.setValue(searchedVal ?? null);
      }
    });

    this.renderer.listen('window', 'click', (e: Event) => {
      if (e.target !== this.toggleButton?.nativeElement && e.target !== this.searchDiv?.nativeElement) {
        this.showSearchList = false;
      }
    });

    this.searchText.valueChanges.subscribe((res) => {
      if (!res) {
        this.searchTextList = [];
        this.ishistory = true;
        const saved = this._coreService.getLocalStorage('serach');
        if (saved) this.historySerachText = [...saved];
      }
    });

    this.searchText.valueChanges.pipe(
      debounceTime(500),
      switchMap((query: any) => {
        if (query && this.lastSearchedText !== query) {
          this.ishistory = false;
          this.lastSearchedText = query;
          if (!this.isSearchCompleted) this.onGetPopularBySearchText();
        }
        return of(query);
      })
    ).subscribe();

    if (ENABLE_LOGIN) {
      this._authService.isLoggedIn$.subscribe(isLoggedIn => {
        if (isLoggedIn) {
          const u = this._authService.getUserDetails();
          this.userDetails = u ? { ...u } : null;
          this._coreService.setLang(this.userDetails?.language || 'en');
        } else {
          this.userDetails = null;
        }
      });
    }
  }

  changeLang(lang: 'en' | 'hi') {
    this.isLangChanged = true;
    this._coreService.setLang(lang);
  }

  ngAfterViewInit() {
    if (ENABLE_LOGIN && isPlatformBrowser(this.platformId)) {
      this.renderGoogleButton();
    }
  }

  renderGoogleButton() {
    if (!this.userDetails) {
      this._authService.renderGoogleButton((resp: any) => {
        this.isLoginLoading = true;
        this.userDetails = null;
        resp = this._coreService.jwtDecode(resp.credential);
        const payload = new LoginReq();
        payload.firstName = resp.given_name;
        payload.lastName = resp.family_name;
        payload.language = resp?.locale?.split('-')[0] || 'en';
        payload.profileImg = resp.picture;
        payload.email = resp.email;
        payload.mobileNumber = resp.phoneNumber;
        payload.platform = 'web';
        payload.provider = 'GOOGLE';
        payload.uId = resp.sub || '';

        this._authService.googleLogin(payload).subscribe(
          (res) => {
            this.isLoginLoading = false;
            if (res.isSuccess) {
              this._coreService.setLocalStorage('user', res.data);
              this._authService.loggedStatusChanged(true);
            } else {
              console.error('Login failed:', res.message || 'Please try again.');
            }
            this._cd.detectChanges();
          },
          () => { this.isLoginLoading = false; }
        );
      });
    }
  }

  logout() {
    this.showProfileMenu = false;
    this._authService.logoutUser();
  }

  onSearchFocus() { this.showSearchList = true; this.isSearchCompleted = false; }
  onSearchFocusout() { if (!this.searchText.value) this.showSearchList = true; }

  onSearch(value: any) {
    if (!String(value).trim()) return;
    const searchText = String(value).trim();
    this.searchText.setValue(searchText);
    if (!this.historySerachText.includes(searchText)) {
      if (this.historySerachText.length < 4) {
        this.historySerachText.push(searchText);
      } else {
        this.historySerachText = [...this.historySerachText.slice(-1), searchText];
      }
    }
    this._coreService.setLocalStorage('serach', this.historySerachText);
    this.showSearchList = false;
    this.isSearchCompleted = true;
    this._router.navigate(['/search', searchText]);
  }

  onGetPopularBySearchText() {
    this.isSearchLoading = true;
    this._searchService.GetPopularBySearchText(String(this.searchText.value?.trim())).subscribe(
      (res) => {
        this.isSearchLoading = false;
        this.searchTextList = res.isSuccess && res.data ? res.data : [];
        if (this.searchTextList.length) this.showSearchList = true;
      },
      () => { this.isSearchLoading = false; this.searchTextList = []; }
    );
  }

  onEnter() {}

  openMobileMenu() {
    this.isMobileMenuOpen = true;
    if (isPlatformBrowser(this.platformId)) {
      document.body.style.overflow = 'hidden';
    }
  }

  closeMobileMenu() {
    this.isMobileMenuOpen = false;
    if (isPlatformBrowser(this.platformId)) {
      document.body.style.overflow = '';
    }
  }
  closeSidebar() { this.isSidebarOpen = false; }
  openSidebar() { this.isSidebarOpen = true; }

}
