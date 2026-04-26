import { Component, OnInit } from '@angular/core';
import { CoreService } from 'src/app/core/services/core.service';
import { SidebarService } from 'src/app/core/services/sidebar.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  isCollapsed = false;
  menus: any[]=[]
  constructor(
    private _sidebarService: SidebarService,
    private _coreService: CoreService,
    public sidebarService:SidebarService
  ) { }

  ngOnInit(): void {
    this._sidebarService.isSidebarCollapsed$.subscribe(isCollapsed => this.isCollapsed = isCollapsed)
    this.menus = this.getMenus();
  }

  getMenus() {
    return this._coreService.getLocalStorage('userData')['menuList']
  }

}
