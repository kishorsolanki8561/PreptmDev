import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { FeedbackTypeDdl } from 'src/app/core/fixed-values';
import { AdditionalPagesService } from 'src/app/core/services/additional-pages.service';
import { AlertService } from 'src/app/core/services/alert.service';
import { AuthService } from 'src/app/core/services/auth.service';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';

@Component({
  selector: 'preptm-contact-us',
  standalone: true,
  imports: [CommonModule, RouterModule, ReactiveFormsModule, CoreModule],
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.scss']
})
export class ContactUsComponent implements OnInit {
  feedbackTypeDdl = FeedbackTypeDdl;
  form: FormGroup = this._fb.group({
    type: [4, Validators.required],
    message: [null, Validators.required]
  });
  isLoading = false;

  constructor(
    private _fb: FormBuilder,
    private _authService: AuthService,
    private _additionalPagesService: AdditionalPagesService,
    private _router: Router,
    private _alert: AlertService
  ) { }

  ngOnInit(): void { }

  submit(): void {
    this.form.markAllAsTouched();
    const value = this.form.getRawValue();
    if (this.form.valid && value.message) {
      this.isLoading = true;
      this._additionalPagesService.sendUserMessage(value).subscribe(
        (resp) => {
          this.isLoading = false;
          if (resp.isSuccess) {
            this._alert.info('Thank you for your feedback');
            this._router.navigateByUrl('/');
          } else {
            this._alert.info('We are facing a technical issue right now, please try again later.');
          }
        },
        () => {
          this._alert.info('We are facing a technical issue right now, please try again later.');
          this.isLoading = false;
        }
      );
    }
  }
}
