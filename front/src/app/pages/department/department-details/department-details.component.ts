import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit, Optional, PLATFORM_ID, Renderer2 } from '@angular/core';
import { MetaDefinition } from '@angular/platform-browser';
import { ActivatedRoute, Params, RouterModule } from '@angular/router';
import { CoreModule } from 'src/app/core/core.module';
import { DepartmentDetail, DepartmentDetailsFilter } from 'src/app/core/models/department.model';
import { CoreService } from 'src/app/core/services/core.service';
import { PostService } from 'src/app/core/services/post.service';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

@Component({
  selector: 'preptm-department-details',
  standalone: true,
  imports: [CommonModule, RouterModule, CoreModule],
  templateUrl: './department-details.component.html',
  styleUrls: ['./department-details.component.scss']
})
export class DepartmentDetailsComponent implements OnInit {
  department: DepartmentDetail | undefined;
  payload: DepartmentDetailsFilter = new DepartmentDetailsFilter();
  isLoading = false;

  constructor(
    private _postService: PostService,
    private _route: ActivatedRoute,
    private _coreService: CoreService,
    private renderer: Renderer2,
    @Inject(PLATFORM_ID) private platformId: Object,
    @Optional() @Inject('IS_MOBILE') private isMobileReq: any,
    @Optional() @Inject(SERVER_RESPONSE) private serverResponse: any,
  ) {
    this._route.params.subscribe((params: Params) => {
      this.payload.slugUrl = params['slug'];
      this.getDetails(this.payload)
    })
  }

  ngOnInit(): void {
  }
  getDetails(payload: DepartmentDetailsFilter) {
    this.department = undefined;
    this.isLoading = true
    this._postService.getDepartmentDetails(payload).subscribe(res => {
      this.isLoading = false
      if (res.isSuccess) {
        this.department = res.data
        this.addMetaTags(this.department)
      } else {
        this.department = undefined;
        this.serverResponse?.status(404);
      }
    }, () => {
      this.isLoading = false
      this.department = undefined;
      this.serverResponse?.status(404);
    })
  }

  addMetaTags(departmentDetails: DepartmentDetail ) {
    let tags: MetaDefinition[] = [];

    tags.push({
      property: 'og:type',
      content: "article"
    })
  
    if (departmentDetails.name) {
      tags.push({
        property: 'description',
        content: departmentDetails.name
      })
      tags.push({
        property: 'og:description',
        content: departmentDetails.name
      })

      this._coreService.setPageTitle(departmentDetails.name)
      
      tags.push({
        property: 'og:title',
        content: departmentDetails.name
      })

      tags.push({
        property: 'keywords',
        content: departmentDetails.name
      })
    }

    if (departmentDetails.logo) {
      tags.push({
        property: 'og:image',
        content: departmentDetails.logo
      })

      tags.push({
        property: 'og:image:alt',
        content: departmentDetails.name
      })

      tags.push({
        property: 'og:image:type',
        content: 'image/webp'
      })
    }


    this._coreService.manageMetaTags(tags, this.renderer);
  }

}
