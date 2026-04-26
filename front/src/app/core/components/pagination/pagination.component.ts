import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';
import { RouterModule } from '@angular/router';

/**
 * Lightweight pagination — no ngx-pagination dependency.
 * Generates query-param page links using RouterModule.
 * All data is server-side paged; this component only renders the UI.
 */
@Component({
  standalone: true,
  imports: [RouterModule],
  selector: 'preptm-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginationComponent {
  readonly currentPage = input<number>(1);
  readonly totalItems  = input<number>(0);
  readonly pageSize    = input<number>(10);
  readonly maxSize     = input<number>(7);

  readonly totalPages = computed(() =>
    Math.max(1, Math.ceil(this.totalItems() / this.pageSize()))
  );

  /** Page numbers to display around the current page. */
  readonly pages = computed<number[]>(() => {
    const total = this.totalPages();
    if (total <= 1) return [];
    const cur = this.currentPage();
    const max = this.maxSize();
    let start = Math.max(1, cur - Math.floor(max / 2));
    let end   = Math.min(total, start + max - 1);
    if (end - start + 1 < max) {
      start = Math.max(1, end - max + 1);
    }
    return Array.from({ length: end - start + 1 }, (_, i) => start + i);
  });
}
