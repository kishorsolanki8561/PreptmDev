import { Component, OnInit } from '@angular/core';
import { ProgressBarService } from '../../services/progress-bar.service';

@Component({
  selector: 'app-progress-bar',
  templateUrl: './progress-bar.component.html',
  styleUrls: ['./progress-bar.component.scss']
})
export class ProgressBarComponent implements OnInit {
  progress = 0
  interval: any
  constructor(
    private progressBarService: ProgressBarService
  ) {
    this.progressBarService.progressBarStatus$.subscribe((status: boolean) => {
      if (status) {
        this.startLoading()
      } else {
        this.stopLoading()
      }

    })
  }

  startLoading() {

    this.progress = 0;
    this.interval = setInterval(() => {
      if (this.progress < 93) {
        this.progress += 6;
      }
    }, 1)
  }

  stopLoading() {
    clearInterval(this.interval);

    let stopInterval = setInterval(() => {
      if (this.progress < 100) {
        this.progress += 1;
      } else {
        clearInterval(stopInterval)
        this.progress = 0
      }
    }, 3)

  }

  ngOnInit(): void {
  }

}
