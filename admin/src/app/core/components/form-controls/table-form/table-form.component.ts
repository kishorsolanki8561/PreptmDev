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
  ViewChild,
} from '@angular/core';
import {
  AbstractControl,
  ControlValueAccessor,
  FormControl,
  FormGroup,
  NG_VALUE_ACCESSOR,
  Validators,
} from '@angular/forms';
import { formElement } from 'src/app/core/models/core.model';
import { AlertService } from 'src/app/core/services/alert.service';
import { CoreService } from 'src/app/core/services/core.service';
import { FormComponent } from '../../form/form.component';
import { Message } from 'src/app/core/models/fixed-value';

interface NumberFormat {
  isNumber?: boolean;
  isDecimalNumber?: boolean;
  isCommaOrDashAllowed?: boolean;
  isDotAllowed?: boolean;
}

@Component({
  selector: 'app-table-form',
  templateUrl: './table-form.component.html',
  styleUrls: ['./table-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TableFormComponent),
      multi: true,
    },
  ],
})
export class TableFormComponent implements ControlValueAccessor, OnInit, DoCheck {
  constructor(
    private _coreService: CoreService,
    private _alertService: AlertService,
  ) { }
  // public value!: string;
  public changed!: (value: any[]) => void;
  public touched: () => void = () => { };
  Validators = Validators;
  isVisible = false
  isEditMode = false
  editIndex: number = -1

  isLoading = false

  @Input() formControlName: string = '';
  @Input() label: string | undefined;
  @Input() form: FormGroup | AbstractControl;
  @Input() childForm?: FormGroup | AbstractControl | any;
  @Input() childFormElements?: formElement[] | any = [];
  @Input() value: any[] = [];
  @Input() isDisabled: boolean;

  @Output() valueChange: EventEmitter<any[]> = new EventEmitter();
  @Output() onFocusOut: EventEmitter<FormControl> = new EventEmitter();

  @ViewChild('formComponent') formComponent: FormComponent;

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
  public writeValue(value: any[]): void {
    this.value = value || [];
  }

  onChange() {
    this.touched()
    if (this.isFormControl) this.changed(this.value);
    this.valueChange.emit(this.value);
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

  onAdd() {
    this.isVisible = true
    this.isEditMode = false
    this.childForm?.reset()
  }

  onFormClosed() {
    this.isVisible = false
    this.childForm?.reset()
  }

  onFormSubmit() {
    if (this.childForm?.valid) {
      this.isLoading = true
      this.formComponent.onSubmit();
    } else {
      this._alertService.error(Message.formError.invalidForm)
    }
  }

  saveForm = (data: any) => {
    // if (this.childForm.invalid) {
    //   this.childForm.markAllAsTouched();
    //   this._alertService.error(Message.formError.invalidForm)
    //   return;
    // }
    if (!this.value) {
      this.value = []
    }
    if (this.isEditMode) {
      this.value[this.editIndex] = data
    } else {
      this.value.push(data)
    }
    this.isVisible = false
    this.isLoading = false
    this.childForm?.reset()
    this.childForm?.enable()
    this.onChange()
  }

  onDelete(index: number) {
    this.value.splice(index, 1);
    this.onChange()
  }

  onEdit(index: number) {
    this.editIndex = index
    this.isEditMode = true
    this.childForm.patchValue(this.value[index])
    this.isVisible = true
  }



}
