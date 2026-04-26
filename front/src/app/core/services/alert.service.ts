import { Injectable, signal } from '@angular/core';

export interface Toast {
  id: number;
  message: string;
  type: 'info' | 'success' | 'error';
}

@Injectable({ providedIn: 'root' })
export class AlertService {
  private _nextId = 0;
  /** Reactive list of active toasts — read by ToastComponent. */
  readonly toasts = signal<Toast[]>([]);

  info(message: string): void    { this._add(message, 'info'); }
  success(message: string): void { this._add(message, 'success'); }
  error(message: string): void   { this._add(message, 'error'); }

  dismiss(id: number): void {
    this.toasts.update(t => t.filter(x => x.id !== id));
  }

  private _add(message: string, type: Toast['type']): void {
    const id = ++this._nextId;
    this.toasts.update(t => [...t, { id, message, type }]);
    setTimeout(() => this.dismiss(id), 4000);
  }
}
