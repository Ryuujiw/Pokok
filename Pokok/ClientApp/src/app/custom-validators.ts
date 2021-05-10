import { FormControl, FormGroupDirective, NgForm, ValidatorFn, AbstractControl } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

/**
 * Custom validator functions for reactive form validation
 */
export class CustomValidators {

  static latitudeValidation: ValidatorFn = (control: AbstractControl) => {
    if (Number(control.value) && Math.abs(control.value) <= 90) {
      return null;
    }
    return { invalid: true }
  };

  static longitudeValidation: ValidatorFn = (control: AbstractControl) => {
    if (Number(control.value) && Math.abs(control.value) <= 180) {
      return null;
    }
    return { invalid: true }
  };
}

/**
 * Custom ErrorStateMatcher which returns true (error exists) when the parent form group is invalid and the control has been touched
 */
export class CustomValidatorMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    return control.parent.invalid && control.touched;
  }
}
