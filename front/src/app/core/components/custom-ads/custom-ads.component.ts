import { Component, PLATFORM_ID, inject } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-custom-ads',
  standalone: true,
  imports: [],
  templateUrl: './custom-ads.component.html',
  styleUrl: './custom-ads.component.scss'
})
export class CustomAdsComponent {
  whatsappLink: string = 'https://chat.whatsapp.com/BW9AIPdks2DJcjZlGQ24Tp';

  private readonly platformId = inject(PLATFORM_ID);

  joinWhatsApp() {
    if (isPlatformBrowser(this.platformId)) {
      window.open(this.whatsappLink, '_blank');
    }
  }
}
