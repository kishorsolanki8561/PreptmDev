import { ChangeDetectionStrategy, Component, input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Breadcrumb } from '../../models/core.models';

@Component({
  standalone: true,
  imports: [RouterModule],
  selector: 'preptm-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BreadcrumbComponent {
  readonly breadcrumb = input<Breadcrumb[]>([]);
}
