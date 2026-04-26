import { Component, DoCheck, EventEmitter, forwardRef, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { DdlItem } from 'src/app/core/models/core.model';


@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SelectComponent),
      multi: true,
    },
  ],
})
export class SelectComponent implements ControlValueAccessor, OnInit, DoCheck {
  // public value!: string;
  public changed: (value: number[] | number | null) => void;
  public touched: () => void = () => { };
  Validators = Validators;
  @Input() isDisabled?: boolean;
  @Input() formControlName: string;
  @Input() label: string;
  @Input() form: FormGroup | AbstractControl;
  @Input() items: DdlItem[] | undefined;
  @Input() multiple: any = false;
  @Input() placeholder: any = '';
  @Input() value: any;
  @Input() replaceValueWithKey = false;
  @Input() otherKeyValue = "";
  @Input() showValueOnly: any = null;
  @Input() clearable: boolean = true;
  @Input() addPrefixIconClass: boolean = false;
  // @Input() isStatusFilter: boolean = false; //used to maintain -1 in status filter when no record selected

  @Output() valueChange: EventEmitter<any> = new EventEmitter();
  @Output() valueChangedOutside: EventEmitter<any> = new EventEmitter();
  isFormControl: boolean = true;
  constructor() { }

  get hasError() {
    return this.isFormControl && this.formField.touched && this.formField.invalid
  }

  get formField() {
    return this.form?.get(this.formControlName) as FormControl;
  }

  ngOnInit(): void {
    if (!this.form && !this.formControlName) this.isFormControl = false;
  }

  // angular says that value is changed from outside
  public writeValue(value: any): void {
    this.valueChangedOutside.emit(value);
    this.value = value;
  }

  //used to update disable state which is not be updated by setDisabledState()
  ngDoCheck(): void {
    if (this.isFormControl && !this.isDisabled)
      this.isDisabled = this.formField?.disabled as boolean;
  }

  onChange(e: any) {
    this.touched()
    if (!e || (Array.isArray(e) && !e.length)) {
      if (this.isFormControl) this.changed(null);
      this.valueChange.emit(null);
      return;
    }


    if (this.isFormControl) this.changed(e);
    this.valueChange.emit(e);
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

  isSelected(value: number | string): boolean {
    if (this.multiple)
      return !!this.value?.find((item: number) => {
        return item === value
      })
    else
      return this.value === value
  }

  addIcon(className: string) {
    return `<i class="${className}" aria-hidden="true"></i> -${className}`
  }
}
