import {
  Component,
  DoCheck,
  EventEmitter,
  forwardRef,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { CoreService } from 'src/app/core/services/core.service';

interface NumberFormat {
  isNumber?: boolean;
  isDecimalNumber?: boolean;
  isCommaOrDashAllowed?: boolean;
  isDotAllowed?: boolean;
}

@Component({
  selector: 'app-text',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextComponent),
      multi: true,
    },
  ],
})
export class TextComponent implements ControlValueAccessor, OnInit, DoCheck {
  constructor(private _coreService: CoreService) { }
  // public value!: string;
  public changed!: (value: string) => void;
  public touched: () => void = () => { };
  Validators = Validators;

  passwordVisible = false;

  @Input() formControlName: string = '';
  @Input() label: string | undefined;
  @Input() form: FormGroup | AbstractControl;
  @Input() placeholder: string | undefined = '';
  @Input() value: string;
  @Input() maxLength: number | null = null;
  @Input() isDisabled: boolean;
  @Input() isPassword: boolean = false;
  @Input() suffixTemplateRef: any;

  // @Input() isNumber: boolean = false;
  // @Input() isCommaOrDashInNumber: boolean = false;
  // @Input() isDecimalNumber: boolean = false;

  @Input() numberFormat?: NumberFormat = {
    isNumber: false,
    isDecimalNumber: false,
    isCommaOrDashAllowed: false,
    isDotAllowed: false,
  };
  // get isDis():boolean{
  //   return this.form.get(this.formControlName)?.disabled as boolean
  // }

  @Output() valueChange: EventEmitter<string> = new EventEmitter();
  @Output() onFocusOut: EventEmitter<FormControl> = new EventEmitter();

  isFormControl: boolean = true;

  get hasError() {
    return this.isFormControl && this.formField.touched && this.formField.invalid
  }

  get formField() {
    return this.form?.get(this.formControlName) as FormControl;
  }

  ngOnInit(): void {
    if (!this.form && !this.formControlName) this.isFormControl = false;
  }

  //used to update disable state which is not be updated by setDisabledState()
  ngDoCheck(): void {
    if (this.isFormControl)
      this.isDisabled = this.formField?.disabled as boolean
  }

  // angular says that value is changed from outside
  public writeValue(value: string): void {
    if (value) this.value = value.toString()?.trim();
    else this.value = value;
  }

  onChange(e: Event) {
    this.touched()
    const value: string = (e.target as HTMLInputElement).value?.trim();
    if (this.isFormControl) this.changed(value);
    this.valueChange.emit(value);
  }

  onKeyPress(e: any) {
    if (this.numberFormat?.isNumber) {
      const isNum = this._coreService.NumberOnly(
        e,
        this.numberFormat.isDotAllowed,
        this.numberFormat.isCommaOrDashAllowed
      );
      if (!isNum) {
        e.preventDefault();
      }
    } else if (this.numberFormat?.isDecimalNumber) {
      const isDecimalNumber = this._coreService.checkDecimalNumberOnly(e);
      if (!isDecimalNumber) {
        e.preventDefault();
      }
    }
  }

  //executes angular change detection,
  //telling angular about value is changed,
  //so angular will reflect in formGroup
  public registerOnChange(fn: any): void {
    this.changed = fn;
  }

  // when control blured - for validation error
  //telling angular about touched,
  //so angular will reflect in formGroup
  public registerOnTouched(fn: any): void {
    this.touched = fn;
  }

  // angular says that value is changed from outside
  public setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  focusOut() {
    this.onFocusOut.emit(this.formField)
  }
}
