import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CoreService } from 'src/app/core/services/core.service';

@Component({
  selector: 'preptm-footer',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
  lang: string = '';
  constructor(private _coreService: CoreService) {
    this.lang = this._coreService.getCurrentLang().includes('en') ? '' : '/hi';
  }
}
