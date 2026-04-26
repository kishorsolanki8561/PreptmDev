import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'preptm-loader',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loader.component.html',
  styleUrls: ['./loader.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoaderComponent {
  readonly type = input<'card' | 'list' | 'detail' | 'spinner'>('spinner');
  readonly count = input<number>(6);

  readonly items = computed(() => Array.from({ length: this.count() }, (_, i) => i));
}
