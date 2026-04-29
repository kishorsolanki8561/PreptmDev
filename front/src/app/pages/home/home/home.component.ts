import { ChangeDetectionStrategy, Component, Inject, OnInit, PLATFORM_ID, Renderer2, inject, signal } from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { CockpitPanelsPosts, Post } from 'src/app/core/models/post.model';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BannerComponent } from './banner/banner.component';
import { PostContainerComponent } from './post-container/post-container.component';
import { CategoriesComponent } from './categories/categories.component';
import { AboutSiteComponent } from './about-site/about-site.component';

@Component({
  selector: 'preptm-home',
  standalone: true,
  imports: [CommonModule, RouterModule, BannerComponent, PostContainerComponent, CategoriesComponent, AboutSiteComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HomeComponent implements OnInit {
  readonly allPosts = signal<CockpitPanelsPosts | undefined>(undefined);
  readonly isLoading = signal(false);

  private readonly _postService = inject(PostService);
  private readonly _coreService = inject(CoreService);
  private readonly renderer = inject(Renderer2);

  constructor(@Inject(PLATFORM_ID) private platformId: object) {
    this._coreService.setPageTitle('Preptm : Latest Job Updates');
    let settled = false;
    this._postService.getPostsForCockpitPanels().subscribe(res => {
      settled = true;
      this.isLoading.set(false);
      if (res.isSuccess) {
        if (res?.data?.privateRecruitments) {
          res.data.privateRecruitments = res.data.privateRecruitments.map((item: Post) => {
            item.blockTypeSlug = 'private-jobs';
            return item;
          });
        }
        this.allPosts.set(res.data);
        this._coreService.addCommonTags(this.renderer);
      }
    });
    if (!settled && isPlatformBrowser(this.platformId)) {
      this.isLoading.set(true);
    }
  }

  ngOnInit(): void {
    this.addMetaTags();
  }

  addMetaTags() {
    const tags: MetaDefinition[] = [
      { property: 'og:type', content: 'website' },
      { property: 'description', content: 'Get the latest government job updates, admit cards, results, syllabus, and previous papers on Preptm.com. Prepare for SSC, RRB, IBPS, and State PSC exams with accurate, timely information and free job alerts.' },
      { property: 'og:description', content: 'Get the latest government job updates, admit cards, results, syllabus, and previous papers on Preptm.com. Prepare for SSC, RRB, IBPS, and State PSC exams with accurate, timely information and free job alerts.' },
      { property: 'og:title', content: 'Preptm : Latest Job Updates' },
      { property: 'keywords', content: 'Government Jobs 2025, Sarkari Naukri, Free Job Alert, Govt Job Notifications, Latest Govt Jobs, Competitive Exams in India, Online Govt Exam Preparation' },
    ];
    this._coreService.manageMetaTags(tags, this.renderer);
  }
}
