import { isPlatformServer } from '@angular/common';
import { AfterViewInit, ChangeDetectionStrategy, Component, inject, input, PLATFORM_ID } from '@angular/core';
declare var adsbygoogle: any[];

@Component({
  standalone: true,
  selector: 'preptm-ads',
  host: { ngSkipHydration: 'true' },
  templateUrl: './ads.component.html',
  styleUrls: ['./ads.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AdsComponent implements AfterViewInit {
  readonly isHeader = input<boolean>(false);
  readonly isSidebar = input<boolean>(false);
  readonly isArticle = input<boolean>(false);

  private readonly platformId = inject(PLATFORM_ID);

  ngAfterViewInit(): void {
    if (!isPlatformServer(this.platformId)) {
      try {
        (adsbygoogle = (window as any).adsbygoogle || []).push({});
      } catch (e) {}
    }
  }
}
