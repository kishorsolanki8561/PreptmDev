import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'preptm-header-tile',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header-tile.component.html',
  styleUrls: ['./header-tile.component.scss']
})
export class HeaderTileComponent {
  @Input() titleName: string;
  @Input() viewMoreUrl: string;
}
