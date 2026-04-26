import { ChangeDetectionStrategy, Component, computed, input } from '@angular/core';
import { Post } from 'src/app/core/models/post.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from 'src/app/core/components/loader/loader.component';
import { PostComponent } from 'src/app/core/components/post/post.component';

@Component({
  selector: 'preptm-post-container',
  standalone: true,
  imports: [CommonModule, RouterModule, LoaderComponent, PostComponent],
  templateUrl: './post-container.component.html',
  styleUrls: ['./post-container.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PostContainerComponent {
  readonly showLoader = input<boolean>(false);
  readonly posts = input<Post[] | undefined>(undefined);
  readonly heading = input<string>('');
  readonly viewMoreUrl = input<string>('');

  readonly enrichedPosts = computed(() => {
    const posts = this.posts();
    if (!posts?.length) return posts;
    const now = Date.now();
    return posts.map(item => ({
      ...item,
      isNew: item.publishedDate
        ? Math.ceil(Math.abs(now - new Date(item.publishedDate).getTime()) / 86400000) < 3
        : false,
    }));
  });
}
