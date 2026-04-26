import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { SearchableSelectComponent } from 'src/app/core/components/searchable-select/searchable-select.component';
import { PostListFilter } from 'src/app/core/models/post.model';
import { ddl, ddlLookup } from 'src/app/core/models/core.models';
import { DdlLookup, PostTypesSlug } from 'src/app/core/fixed-values';

/**
 * Filter sidebar for PostListComponent.
 *
 * Uses a custom SearchableSelectComponent instead of ng-zorro NzSelectModule —
 * eliminates CDK overhead and hydration mismatches. ngSkipHydration on the
 * host element (in the parent template) is kept for safety but is no longer
 * strictly required.
 */
@Component({
  selector: 'preptm-post-filter',
  standalone: true,
  imports: [SearchableSelectComponent, CustomAdsComponent],
  templateUrl: './post-filter.component.html',
})
export class PostFilterComponent {
  @Input() ddls: ddl | undefined;
  @Input() ddlLookup: ddlLookup | undefined;
  /** Mutable reference — SearchableSelectComponent emits valueChange events */
  @Input() filter!: PostListFilter;
  @Input() type = '';
  @Input() categorySlug = '';
  @Input() ddlLookupEnum: typeof DdlLookup = DdlLookup;

  readonly postType = PostTypesSlug;

  /** Emitted when any filter dropdown value changes — parent should call filterList() */
  @Output() filterChange = new EventEmitter<void>();
  /** Emitted when "Clear all" is clicked — parent should call clearAll() */
  @Output() cleared = new EventEmitter<void>();
}
