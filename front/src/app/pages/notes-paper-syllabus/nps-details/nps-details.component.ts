import { isPlatformServer, CommonModule, PlatformLocation } from '@angular/common';
import {
  ChangeDetectionStrategy, Component, inject, OnInit,
  PLATFORM_ID, Renderer2, signal
} from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { CustomAdsComponent } from 'src/app/core/components/custom-ads/custom-ads.component';
import { DATE_FORMAT, PreptmLogo } from 'src/app/core/fixed-values';
import { Breadcrumb } from 'src/app/core/models/core.models';
import { BlockContaintDetails } from 'src/app/core/models/post.model';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

@Component({
  selector: 'app-nps-details',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, RouterModule, CoreModule, CustomAdsComponent, LoaderComponent],
  templateUrl: './nps-details.component.html',
  styleUrl: './nps-details.component.scss'
})
export class NpsDetailsComponent implements OnInit {
  private readonly _postService = inject(PostService);
  private readonly _route = inject(ActivatedRoute);
  private readonly _coreService = inject(CoreService);
  private readonly renderer = inject(Renderer2);
  readonly platformLocation = inject(PlatformLocation);
  private readonly platformId = inject(PLATFORM_ID);
  private readonly serverResponse = inject(SERVER_RESPONSE, { optional: true });

  readonly post = signal<BlockContaintDetails | undefined>(undefined);
  readonly breadcrumb = signal<Breadcrumb[]>([]);
  readonly isLoading = signal(false);
  readonly shortDesc = signal<string[]>([]);
  readonly lang = signal('');

  readonly DATE_FORMAT = DATE_FORMAT;
  readonly preptmLogo = PreptmLogo;

  ngOnInit(): void {
    this.lang.set(this._coreService.getCurrentLang());
    this._route.params.subscribe((params: Params) => {
      this.getDetails(params['slug']);
    });
  }

  private getDetails(slug: string): void {
    this.isLoading.set(true);
    this.post.set(undefined);
    this._postService.getBlockContaintDetails(slug).subscribe({
      next: (res) => {
        this.isLoading.set(false);
        if (res.isSuccess && res.data) {
          this.post.set(res.data);
          this.shortDesc.set(res.data.summary?.split('\n') ?? []);
          const p = res.data;
          this.breadcrumb.set([
            { text: this._coreService.titleCase(p.moduleText), path: '/' + p.moduleSlug },
            { text: p.title }
          ]);
          this.addMetaTags(p);
        } else {
          this.post.set(undefined);
          if (isPlatformServer(this.platformId) && this.serverResponse) {
            this.serverResponse.status(404);
          }
        }
      },
      error: () => {
        this.isLoading.set(false);
        if (isPlatformServer(this.platformId) && this.serverResponse) {
          this.serverResponse.status(404);
        }
      }
    });
  }

  private addMetaTags(details: BlockContaintDetails): void {
    const desc = this.shortDesc()[0] || details.title;
    const tags: MetaDefinition[] = [
      { property: 'og:type', content: 'article' },
      { property: 'description', content: desc },
      { property: 'og:description', content: desc },
    ];
    if (details.title) {
      this._coreService.setPageTitle(details.title);
      tags.push({ property: 'og:title', content: details.title });
    }
    if (details.thumbnail || details.departmentLogo) {
      tags.push({ property: 'og:image', content: details.thumbnail || details.departmentLogo });
      tags.push({ property: 'og:image:alt', content: details.title });
    }
    if (details.keywords) {
      tags.push({ property: 'keywords', content: details.keywords });
    }
    this._coreService.manageMetaTags(tags, this.renderer);
  }
}
