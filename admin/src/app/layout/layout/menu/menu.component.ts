import { Component, Input, OnInit } from '@angular/core';
import { SidebarService } from 'src/app/core/services/sidebar.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
  @Input() menus: any[] = []
  constructor(
    public sidebarService:SidebarService
  ) { }

  ngOnInit(): void {
  }

  // getListUrl(pages: any[]): string {
  //   let url = pages.filter((url: any) => url['permissionType'] == 4)
  //   if (url.length)
  //     return '/' + url[0]['url']
  //   else
  //     return '/'
  // }

}
