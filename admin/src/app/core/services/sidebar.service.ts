import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  isSidebarCollapsed$ = new BehaviorSubject<boolean>(false);
  constructor() { }
  collapseSidebar() {
    this.isSidebarCollapsed$.next(true);
  }
  expandSidebar() {
    this.isSidebarCollapsed$.next(false);
  }

  // getListUrl(pages: any[]): string {
  //   let url = pages.filter((url: any) => url['permissionType'] == 4)
  //   if (url.length)
  //     return '/' + url[0]['url']
  //   else
  //     return '/'
  // }

}
