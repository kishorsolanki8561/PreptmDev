import {
  ChangeDetectionStrategy, Component, ElementRef, OnDestroy, OnInit,
  PLATFORM_ID, ViewChild, inject, signal
} from '@angular/core';
import { isPlatformServer } from '@angular/common';
import { BannerListModel } from 'src/app/core/models/Banner.model';
import { SearchService } from 'src/app/core/services/search.service';

@Component({
  selector: 'preptm-banner',
  standalone: true,
  imports: [],
  templateUrl: './banner.component.html',
  styleUrls: ['./banner.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BannerComponent implements OnInit, OnDestroy {
  readonly isLoading   = signal(true);
  readonly banners     = signal<BannerListModel[]>([]);
  readonly activeIndex = signal(0);

  @ViewChild('track') trackEl?: ElementRef<HTMLElement>;

  private readonly _searchService = inject(SearchService);
  private readonly _platformId    = inject(PLATFORM_ID);
  private _timer: ReturnType<typeof setInterval> | undefined;

  ngOnInit(): void {
    this._searchService.GetBanners().subscribe({
      next: (res) => {
        this.isLoading.set(false);
        if (res.isSuccess && res.data?.length) {
          this.banners.set(res.data);
          if (!isPlatformServer(this._platformId) && res.data.length > 1) {
            this._startTimer();
          }
        }
      },
      error: () => this.isLoading.set(false)
    });
  }

  ngOnDestroy(): void {
    clearInterval(this._timer);
  }

  goTo(index: number): void {
    this.activeIndex.set(index);
    const el = this.trackEl?.nativeElement;
    if (el) {
      el.scrollTo({ left: el.offsetWidth * index, behavior: 'smooth' });
    }
  }

  onImgError(event: Event): void {
    (event.target as HTMLImageElement).style.display = 'none';
  }

  private _startTimer(): void {
    this._timer = setInterval(() => {
      const next = (this.activeIndex() + 1) % this.banners().length;
      this.goTo(next);
    }, 4000);
  }
}
