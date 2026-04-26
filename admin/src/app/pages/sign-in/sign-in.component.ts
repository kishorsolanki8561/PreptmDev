import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService } from 'src/app/core/services/alert.service';
import { CoreService } from 'src/app/core/services/core.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  form: FormGroup;
  constructor(
    private _authService: AuthService,
    private _fb: FormBuilder,
    private _router: Router,
    private _alertService: AlertService,
    private _coreService: CoreService
  ) { }

  ngOnInit(): void {
    this.createForm();
  }
  createForm() {
    this.form = this._fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }
  signIn(): void {
    if (this.form.invalid) {
      return;
    }
    this.form.disable();
    this._authService.signIn(this.form.value).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          let permissions = this._authService.processPermission(res.data.pageComponents)
          res.data.pageComponents = permissions
          this._coreService.setLocalStorage('userData', res.data)
          this._authService.updatePagePermissions();
          this._router.navigate(['/dashboard']);
        } else {
          this.onSignInFailed();
          this._alertService.error(res.message);
        }
      },
      error: (err: Error) => {
        this.onSignInFailed()
      }
    });
  }

  onSignInFailed() {
    this.form.enable();
  }

}
