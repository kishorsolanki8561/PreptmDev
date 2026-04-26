import {
  Component,
  DoCheck,
  EventEmitter,
  forwardRef,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { differenceInCalendarDays } from 'date-fns';

@Component({
  selector: 'app-date',
  templateUrl: './date.component.html',
  styleUrls: ['./date.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateComponent),
      multi: true,
    },
  ],
})
export class DateComponent implements ControlValueAccessor, OnInit, DoCheck {
  constructor(
    private _cdRef: ChangeDetectorRef
  ) { }
  // public value!: string;
  public changed = (value: string) => { };
  public touched: () => void = () => { };
  public isDisabled!: boolean;
  Validators = Validators;

  @Input() formControlName!: string;
  @Input() label!: string;
  @Input() form!: FormGroup | AbstractControl;
  @Input() value!: any;
  @Input() placeholder: any = '';
  @Input() minDate: undefined | Date;
  @Input() maxDate: undefined | Date;
  @Input() type: 'date' | 'dateRange' = 'date'

  @Output() valueChange: EventEmitter<Date | string> = new EventEmitter();

  isFormControl: boolean = true;

  get formField() {
    return this.form.get(this.formControlName) as FormControl;
  }

  ngOnInit(): void {
    if (!this.form && !this.formControlName) this.isFormControl = false;
  }

  disabledDate = (current: Date): boolean => {
    if (this.minDate && this.maxDate)
      return (differenceInCalendarDays(current, this.minDate) < 0) || (differenceInCalendarDays(current, this.maxDate) > 0)
    else if (this.minDate)
      return differenceInCalendarDays(current, this.minDate) < 0;
    else if (this.maxDate) {
      let diff = differenceInCalendarDays(current, this.maxDate)

      return diff > 0;
    }
    return false;
  }

  // angular says that value is changed from outside
  public writeValue(value: string): void {
    this.value = value;
    // this._cdRef.detectChanges();
  }

  onChange() {
    let trimmedDate: string = ''
    if (this.value) {
      trimmedDate = this.value.getFullYear() + '-' + (this.value.getMonth() + 1).toString().padStart(2, '0') + '-' + this.value.getDate().toString().padStart(2, '0')
    }
    if (this.isFormControl) this.changed(trimmedDate);
    this.valueChange.emit(trimmedDate);
  }

  //used to update disable state which is not be updated by setDisabledState()
  ngDoCheck(): void {
    // this.onChange()
    if (this.isFormControl)
      this.isDisabled = this.formField?.disabled as boolean
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
}
