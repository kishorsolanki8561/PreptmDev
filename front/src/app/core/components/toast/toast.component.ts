import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { AlertService } from '../../services/alert.service';

@Component({
  selector: 'preptm-toast',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  template: `
    <div class="fixed bottom-4 right-4 z-[9999] flex flex-col gap-2 pointer-events-none"
         aria-live="polite" aria-atomic="true">
      @for (toast of _alert.toasts(); track toast.id) {
        <div class="pointer-events-auto flex items-center gap-3 bg-neutral-900 text-white text-sm
                    px-4 py-3 rounded-xl shadow-xl min-w-[240px] max-w-[360px]">
          @if (toast.type === 'info') {
            <svg class="h-4 w-4 text-sky-400 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z"/>
            </svg>
          }
          @if (toast.type === 'success') {
            <svg class="h-4 w-4 text-green-400 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7"/>
            </svg>
          }
          @if (toast.type === 'error') {
            <svg class="h-4 w-4 text-red-400 flex-shrink-0" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
            </svg>
          }
          <span class="flex-1 leading-snug">{{ toast.message }}</span>
          <button (click)="_alert.dismiss(toast.id)"
            class="ml-1 text-neutral-400 hover:text-white transition-colors flex-shrink-0 p-0.5"
            aria-label="Dismiss">
            <svg class="h-3.5 w-3.5" fill="none" viewBox="0 0 24 24" stroke="currentColor">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"/>
            </svg>
          </button>
        </div>
      }
    </div>
  `
})
export class ToastComponent {
  readonly _alert = inject(AlertService);
}
