import { Component, OnInit, Optional, Inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SERVER_RESPONSE } from 'src/app/core/tokens/ssr.tokens';

/**
 * Shown for unknown routes (**) and for detail pages that return no data.
 * On SSR, sets HTTP status 404 so Google does not treat it as a soft 404.
 */
@Component({
  selector: 'preptm-no-records',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './no-records.component.html',
  styleUrls: ['./no-records.component.scss']
})
export class NoRecordsComponent implements OnInit {
  constructor(
    @Optional() @Inject(SERVER_RESPONSE) private readonly _response: any
  ) {}

  ngOnInit(): void {
    // On the server, set a real 404 status so Googlebot does not index this as
    // a soft 404 (200 OK with "not found" content).
    if (this._response?.status) {
      this._response.status(404);
    }
  }
}
