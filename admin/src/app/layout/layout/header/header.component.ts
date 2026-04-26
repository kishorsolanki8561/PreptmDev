import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { CoreService } from 'src/app/core/services/core.service';
import { SidebarService } from 'src/app/core/services/sidebar.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isCollapsed = false;
  userDetails: any;
  enableTranslate = this._coreService.translatorStaus();
  env = environment
  constructor(
    private _sidebarService: SidebarService,
    private _authService: AuthService,
    private _coreService: CoreService,
  ) {
    this.userDetails = this._coreService.getLocalStorage('userData')
  }

  ngOnInit(): void {
    this._sidebarService.isSidebarCollapsed$.subscribe(isCollapsed => this.isCollapsed = isCollapsed)
  }

  toggleSidebar() {
    if (this.isCollapsed)
      this._sidebarService.expandSidebar();
    else
      this._sidebarService.collapseSidebar();
  }

  logout() {
    this._authService.signOut()
  }

  onTranslateChange() {
    this.enableTranslate = !this.enableTranslate;
    this._coreService.changeTranslatorStatus(this.enableTranslate)
  }

}
