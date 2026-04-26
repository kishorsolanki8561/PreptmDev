import { ChangeDetectionStrategy, Component, computed, inject, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AlertService } from '../../services/alert.service';
import { CoreService } from '../../services/core.service';
import { ShareContent } from '../../models/core.models';

@Component({
  standalone: true,
  imports: [CommonModule],
  selector: 'preptm-share-buttons',
  templateUrl: './share-buttons.component.html',
  styleUrls: ['./share-buttons.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShareButtonsComponent {
  readonly content = input<ShareContent | undefined>(undefined);
  readonly showShareButtons = signal(false);

  private readonly _alertService = inject(AlertService);
  private readonly _coreService = inject(CoreService);

  readonly socialMediaShareLink = computed(() => {
    const c = this.content();
    const links = {
      facebook: 'https://www.facebook.com/sharer/sharer.php?u=',
      twitter: 'http://x.com/share?text=',
      whatsapp: 'https://api.whatsapp.com/send?text=',
      telegram: 'https://t.me/share/url?url=',
      copy: '',
    };
    if (!c) return links;

    let body = '';
    body += c.totalPost ? `▪️ Total Posts : ${c.totalPost}%0A` : '';
    body += c.startDate ? `▪️ Start Date : ${this._coreService.dateString(c.startDate)}%0A` : '';
    body += c.date ? `▪️ Date : ${this._coreService.dateString(c.date)}%0A` : '';
    body += c.lastDate ? `▪️ Last Date : ${this._coreService.dateString(c.lastDate)}%0A` : '';
    body += c.extendedDate ? `▪️ Extended Date : ${this._coreService.dateString(c.extendedDate)}%0A` : '';
    body += c.admitCardDate ? `▪️ Admit Card Date : ${this._coreService.dateString(c.admitCardDate)}%0A  %0A` : '';
    body += c.FeeLastDate ? `▪️ Fee Last Date : ${this._coreService.dateString(c.FeeLastDate)}%0A  %0A` : '';

    links.facebook += c.link;

    links.whatsapp += c.title ? `*${encodeURIComponent(c.title)}* %0A %0A` : '';
    links.whatsapp += body;
    links.whatsapp += `%0AClick below link for more details 👇🏻 %0A`;
    links.whatsapp += `${c.link} %0A  %0A`;
    links.whatsapp += `Join us to stay updated %0A`;
    links.whatsapp += `Telegram : https://t.me/official_5study %0A`;
    links.whatsapp += `Whatsapp : https://chat.whatsapp.com/BW9AIPdks2DJcjZlGQ24Tp`;

    links.twitter += c.title ? `${encodeURIComponent(c.title)} %0A %0A` : '';
    links.twitter += body;
    links.twitter += `${c.link}`;

    links.telegram += c.title ? `${encodeURIComponent(c.title)} %0A %0A` : '';
    links.telegram += body;
    links.telegram += `%0AClick below link for more details 👇🏻 %0A`;
    links.telegram += `${c.link} %0A  %0A`;
    links.telegram += `Telegram : https://t.me/official_5study %0A`;
    links.telegram += `Whatsapp : https://chat.whatsapp.com/BW9AIPdks2DJcjZlGQ24Tp`;

    links.copy += c.title ? `${c.title} \n\n` : '';
    links.copy += body;
    links.copy += `%0AClick below link for more details 👇🏻 \n`;
    links.copy += `${c.link} \n  \n`;
    links.copy += `Join us to stay updated \n`;
    links.copy += `Telegram : https://t.me/official_5study \n`;
    links.copy += `Whatsapp : https://chat.whatsapp.com/BW9AIPdks2DJcjZlGQ24Tp`;
    links.copy = links.copy.replace(/%0A/g, '\n');

    return links;
  });

  copy() {
    this._coreService.copyTextToClipboard(this.socialMediaShareLink().copy);
    this._alertService.info('Copied');
  }

  toggle() {
    this.showShareButtons.update(v => !v);
  }

  close() {
    this.showShareButtons.set(false);
  }
}
