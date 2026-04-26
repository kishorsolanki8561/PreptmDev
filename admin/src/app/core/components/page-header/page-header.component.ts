import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Breadcrumb } from '../../models/core.model';
import { ActionTypes } from '../../models/fixed-value';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {

  @Output() onSearch = new EventEmitter<string>();
  @Input() breadcrumb: Breadcrumb[] = []
  @Input() addButtonUrl: string = ''
  permissions = {
    add: false
  }
  constructor(
    private _authService: AuthService,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.updatePermissions()
  }
  search(val: any) {
    this.onSearch.emit(val.trim())
  }

  updatePermissions() {
    let permissions = this._authService.getPermissions()
    let component = this._route.snapshot.data['component']
    let actions = permissions[component]
    this.permissions.add = actions.includes(ActionTypes.ADD)
  }

}
