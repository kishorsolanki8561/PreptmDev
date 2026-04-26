import {
  ChangeDetectionStrategy, Component, ElementRef, HostListener,
  computed, input, model, signal
} from '@angular/core';

export interface SelectOption {
  value: any;
  text: string;
}

/**
 * Lightweight searchable select — zero dependencies, signals-based.
 * Replaces NzSelectModule (nzShowSearch) with no CDK/animation overhead.
 *
 * Usage:
 *   <preptm-searchable-select
 *     [value]="filter.deptId"
 *     (valueChange)="filter.deptId = $event; onChange()"
 *     [options]="deptOptions"
 *     [allowClear]="true"
 *     placeholder="All departments">
 *   </preptm-searchable-select>
 */
@Component({
  selector: 'preptm-searchable-select',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './searchable-select.component.html',
})
export class SearchableSelectComponent {
  // External value binding — use [value] + (valueChange) on parent
  readonly value = model<any>(null);
  readonly options = input<SelectOption[]>([]);
  readonly placeholder = input<string>('Select…');
  readonly disabled = input<boolean>(false);
  readonly allowClear = input<boolean>(false);

  readonly isOpen = signal(false);
  readonly searchQuery = signal('');

  readonly filteredOptions = computed(() => {
    const q = this.searchQuery().toLowerCase().trim();
    if (!q) return this.options();
    return this.options().filter(o => o.text.toLowerCase().includes(q));
  });

  readonly selectedLabel = computed(() => {
    const v = this.value();
    if (v === null || v === undefined || v === 0 || v === '') return null;
    return this.options().find(o => o.value === v)?.text ?? null;
  });

  constructor(private _el: ElementRef) {}

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    if (!this._el.nativeElement.contains(event.target as Node)) {
      this._close();
    }
  }

  toggle(): void {
    if (this.disabled()) return;
    this.isOpen.update(v => !v);
    if (!this.isOpen()) this.searchQuery.set('');
  }

  select(val: any): void {
    this.value.set(val);
    this._close();
  }

  clear(event: MouseEvent): void {
    event.stopPropagation();
    this.value.set(null);
    this._close();
  }

  onSearchInput(event: Event): void {
    this.searchQuery.set((event.target as HTMLInputElement).value);
  }

  private _close(): void {
    this.isOpen.set(false);
    this.searchQuery.set('');
  }
}
