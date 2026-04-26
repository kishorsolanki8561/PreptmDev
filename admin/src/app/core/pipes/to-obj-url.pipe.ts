import { Pipe, PipeTransform } from '@angular/core';
import { environment } from 'src/environments/environment';

@Pipe({
  name: 'toObjUrl'
})
export class ToObjUrlPipe implements PipeTransform {

  transform(file: File): string {
    return URL.createObjectURL(file);
  }

}
@Pipe({
  name: 'replace'
})
export class ReplacePipe implements PipeTransform {

  transform(value: string, regexValue: string, replaceValue: string): any {
    let regex = new RegExp(regexValue, 'g');
    return value.replace(regex, replaceValue);
  }


}
@Pipe({
  name: 'findValue'
})
export class FindValuePipe implements PipeTransform {

  transform(value: number, key: string, outkey: string, array: any[]): any {

    if (key && (array && array.length)) {
      return array.find(s => s[key] == value)?.[outkey]
    }
    return ""

  }
}

@Pipe({
  name: 'absPath'
})
export class AbsPath implements PipeTransform {

  transform(value: string): any {
    if (!value || value?.includes('http')) {
      return value
    } else {
      return environment.fileEndPoint + value
    }
  }
}