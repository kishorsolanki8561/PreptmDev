import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Post } from 'src/app/core/models/post.model';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { HeaderTileComponent } from '../header-tile/header-tile.component';

@Component({
  selector: 'preptm-post-card',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule, HeaderTileComponent],
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.scss']
})
export class PostCardComponent implements OnInit, OnChanges {
  @Input() className: string;
  @Input() showLoader: boolean;
  @Input() posts: Post[] | undefined;
  @Input() title: string;
  @Input() viewMoreUrl: string;
  curDate:Date = new Date();

  ngOnInit(): void {

  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['posts'].currentValue?.length){
      this.posts?.forEach((item)=>{
        if(item.publishedDate){
          item['isNew'] = this.checkNewPost(item.publishedDate)
        }
      })
    }
  }



  checkNewPost(createDate: any): boolean {
    // Calculate the difference in milliseconds
    const timeDifference = Math.abs(this.curDate.getTime() - new Date(createDate).getTime());

    // Convert the difference to days
    const daysDifference = Math.ceil(timeDifference / (86400000));    // 1000 * 3600 * 24 = 86400000
    return daysDifference < 3;
  }


}
