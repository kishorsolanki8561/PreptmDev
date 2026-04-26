import { isPlatformServer } from '@angular/common';
import {
  ChangeDetectionStrategy, Component, HostListener,
  PLATFORM_ID, computed, inject, input, signal
} from '@angular/core';

@Component({
  standalone: true,
  imports: [],
  selector: 'preptm-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ImageComponent {
  readonly src        = input<string>('');
  readonly showPreview = input<boolean>(false);
  readonly alt        = input<string>('');
  readonly onlyLow    = input<boolean>(false);
  readonly onlyMedium = input<boolean>(false);
  readonly className  = input<string>('');
  readonly skipSmall  = input<boolean>(false);
  /** Set true for the above-the-fold LCP image. Adds fetchpriority=high + loading=eager. */
  readonly priority   = input<boolean>(false);

  readonly fallback   = 'assets/img/placeholder.svg';
  readonly srcWidth   = signal(0);
  readonly previewOpen = signal(false);

  readonly srcHigh   = computed(() => this._deriveSrc(this.src(), 'Th1200x628'));
  readonly srcMedium = computed(() => this._deriveSrc(this.src(), 'Th360x180'));
  readonly srcLow    = computed(() => this._deriveSrc(this.src(), 'Th360x180'));

  private readonly _platformId = inject(PLATFORM_ID);
  private readonly _isServer   = isPlatformServer(this._platformId);

  constructor() {
    this._calcWidth();
  }

  @HostListener('window:resize')
  onWindowResize(): void {
    this._calcWidth();
  }

  onImgError(event: Event): void {
    const img = event.target as HTMLImageElement;
    if (img.src !== this.fallback) {
      img.src = this.fallback;
    }
  }

  openPreview(): void {
    if (this.showPreview()) {
      this.previewOpen.set(true);
    }
  }

  closePreview(): void {
    this.previewOpen.set(false);
  }

  private _deriveSrc(url: string, variant: string): string {
    if (!url) return this.fallback;
    const ext = url.split('.').pop();
    return url
      .replaceAll('OriginalAttachment', variant)
      .replaceAll(`.${ext}`, '.png');
  }

  private _calcWidth(): void {
    if (!this._isServer) {
      this.srcWidth.set(window.innerWidth);
    }
  }
}
