import { ChangeDetectionStrategy, Component, computed, inject, input } from '@angular/core';
import { DOCUMENT, CommonModule, TitleCasePipe, DatePipe, DecimalPipe, NgClass } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Post } from '../../models/post.model';
import { DATE_FORMAT } from '../../fixed-values';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, TitleCasePipe, DatePipe, DecimalPipe, NgClass],
  selector: 'preptm-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostComponent {
  readonly minimal = input<boolean>(false);
  readonly post = input.required<Post>();
  readonly index = input<number | null>(null);
  readonly DATE_FORMAT = DATE_FORMAT;
  readonly document = inject(DOCUMENT);

  /** Left accent bar + badge colours derived from blockTypeSlug */
  readonly accentBar = computed(() => {
    const s = this.post().blockTypeSlug ?? '';
    if (s.includes('result'))    return 'bg-green-500';
    if (s.includes('admission')) return 'bg-violet-500';
    if (s.includes('exam'))      return 'bg-amber-400';
    if (s.includes('admit'))     return 'bg-sky-400';
    if (s.includes('scheme'))    return 'bg-teal-500';
    if (s.includes('job') || s.includes('recruit')) return 'bg-blue-500';
    return 'bg-neutral-300';
  });

  readonly badgeCls = computed(() => {
    const s = this.post().blockTypeSlug ?? '';
    if (s.includes('result'))    return 'bg-green-50 text-green-700';
    if (s.includes('admission')) return 'bg-violet-50 text-violet-700';
    if (s.includes('exam'))      return 'bg-amber-50 text-amber-700';
    if (s.includes('admit'))     return 'bg-sky-50 text-sky-700';
    if (s.includes('scheme'))    return 'bg-teal-50 text-teal-700';
    if (s.includes('job') || s.includes('recruit')) return 'bg-blue-50 text-blue-700';
    return 'bg-neutral-100 text-neutral-600';
  });
}
