import { ChangeDetectionStrategy, Component, OnInit, inject, signal, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ddlItem } from 'src/app/core/models/core.models';
import { CoreService } from 'src/app/core/services/core.service';

@Component({
  selector: 'preptm-categories',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CategoriesComponent implements OnInit {
  readonly isLoading = signal(false);
  readonly categories = signal<ddlItem[]>([]);

  private readonly _coreService = inject(CoreService);
  private readonly _platformId = inject(PLATFORM_ID);

  ngOnInit(): void {
    if (isPlatformBrowser(this._platformId)) {
      this.isLoading.set(true);
    }
    this._coreService.getDdl('ddlCategory').subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.categories.set(res.data.ddlCategory ?? []);
        }
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  /** On broken image: hide the <img> and show the first-letter fallback */
  showFallback(event: Event, _text: string): void {
    if (!isPlatformBrowser(this._platformId)) return;
    const img = event.target as HTMLImageElement;
    const parent = img.parentElement;
    if (!parent) return;
    img.style.display = 'none';
    const span = document.createElement('span');
    span.className = 'text-lg font-bold text-brand-600';
    span.textContent = (_text?.charAt(0) ?? '?').toUpperCase();
    parent.appendChild(span);
  }
}
