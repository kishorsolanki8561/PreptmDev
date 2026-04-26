import { Component } from '@angular/core';
import { RouterModule, ActivatedRoute, Params, Router } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { FooterComponent } from './layout/footer/footer.component';
import { ToastComponent } from './core/components/toast/toast.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, HeaderComponent, FooterComponent, ToastComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'preptm';
  constructor(
    private _route: ActivatedRoute,
    private _router: Router
  ) {
    this._route.queryParams.subscribe((params: Params) => {
      if (params['moduleName'] && params['slugUrl']) {
        this._router.navigate(['/', params['moduleName'], params['slugUrl']]);
      }
    });
  }
}
