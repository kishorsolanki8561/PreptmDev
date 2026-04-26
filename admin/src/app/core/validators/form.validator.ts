import { AbstractControl } from "@angular/forms";

export const FileRequired = (control: AbstractControl): { [key: string]: any } | null => {
    if (!control.value || (!control.value.file.length && !control.value.urls.length)) {
        return { 'FileRequired': true };
    }

    return null;
}

export const maxLine = (length: number) => {
    return (control: AbstractControl): { maxLine: any } | null => {
        let val = control.value
        if (val && val.split('\n').length > length) {
            return { 'maxLine': { length } };
        }
        return null;
    }
}
export const maxCharEachLine = (length: number) => {
    return (control: AbstractControl): { maxCharEachLine: any } | null => {
        let val = control.value
        if (val) {
            let lines = val?.split('\n')
            for (let i = 0; i < lines.length; i++) {
                if (lines[i].length > length) {
                    return { 'maxCharEachLine': { length } };
                }
            }
        }
        return null;
    }
}

export const minLengthArr = (length: number) => {
    return (control: AbstractControl): { minLengthArr: any } | null => {
        let val = control.value || []
        if (val?.length < length) {
            return { 'minLengthArr': { length } };
        }
        return null;
    }
}
