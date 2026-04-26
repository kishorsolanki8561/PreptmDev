import { Component, DoCheck, EventEmitter, forwardRef, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { AbstractControl, ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators, } from '@angular/forms';
import { CoreService } from 'src/app/core/services/core.service';

@Component({
  selector: 'app-switch',
  templateUrl: './switch.component.html',
  styleUrls: ['./switch.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => SwitchComponent),
      multi: true,
    },
  ],
})
export class SwitchComponent implements ControlValueAccessor, OnInit, DoCheck {
  constructor(private _coreService: CoreService) { }
  public changed: (value: string) => void;
  public touched: () => void = () => { };
  Validators = Validators;

  @Input() formControlName: string = '';
  @Input() label: string;
  @Input() form: FormGroup | AbstractControl;
  @Input() value: boolean = false;
  @Input() isDisabled: boolean = false;
  @Input() isLoading: boolean = false;

  @Output() valueChange: EventEmitter<string> = new EventEmitter();

  isFormControl: boolean = true;

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
  public writeValue(value: boolean): void {
    this.value = !!value;
  }

  onChange(value: any) {
    this.touched()
    if (this.isFormControl) this.changed(value);
    this.valueChange.emit(value);
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
