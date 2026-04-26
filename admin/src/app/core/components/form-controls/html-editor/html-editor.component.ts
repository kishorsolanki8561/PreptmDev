import {
  AfterViewInit,
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
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { EditorConfig } from 'ckeditor5/src/core';
import { CoreService } from 'src/app/core/services/core.service';
import { HttpClient, HttpXhrBackend } from '@angular/common/http';
import EditorJS, { OutputData } from '@editorjs/editorjs';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/core/services/base.service'
import { EditorVal } from 'src/app/core/models/core.model';
import { AlertService } from 'src/app/core/services/alert.service';
const List = require('@editorjs/list');
const Header = require('@editorjs/header');
const Table = require('@editorjs/table');
const RawTool = require('@editorjs/raw');
const Quote = require('@editorjs/quote');
const ImageTool = require('@editorjs/image');
const AttachesTool = require('@editorjs/attaches');
const edjsHTML = require("editorjs-html");

interface NumberFormat {
  isNumber?: boolean;
  isDecimalNumber?: boolean;
  isCommaOrDashAllowed?: boolean;
  isDotAllowed?: boolean;
}



@Component({
  selector: 'app-html-editor',
  templateUrl: './html-editor.component.html',
  styleUrls: ['./html-editor.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => HtmlEditorComponent),
      multi: true,
    },
  ],
})
export class HtmlEditorComponent implements ControlValueAccessor, OnInit, DoCheck, AfterViewInit {
  constructor(
    public _baseService: BaseService,
    public coreService: CoreService,
    private _alert: AlertService,
    public _http: HttpClient
  ) { }
  // public value!: string;
  public changed!: (value: EditorVal) => void;
  public touched: () => void = () => { };
  Validators = Validators;
  public Editor = ClassicEditor;
  // summerconfig= summerconfigs;
  @Input() formControlName: string = '';
  @Input() label: string | undefined;
  @Input() form: FormGroup | AbstractControl;
  @Input() placeholder: string | undefined = '';
  @Input() value: EditorVal
  @Input() maxLength: number | null = null;
  @Input() isDisabled: boolean = false;
  @Input() editorId: string;



  // @Input() isNumber: boolean = false;
  // @Input() isCommaOrDashInNumber: boolean = false;
  // @Input() isDecimalNumber: boolean = false;

  get formControl() {
    return this.form.get(this.formControlName) as FormControl
  }

  @Input() numberFormat: NumberFormat = {
    isNumber: false,
    isDecimalNumber: false,
    isCommaOrDashAllowed: false,
    isDotAllowed: false,
  };

  editor!: EditorJS
  edjsParser = edjsHTML({
    table: this.tableParcer,
    raw: this.rawParcer,
    image: this.imageParcer,
    quote: this.quoteParcer,
    attaches: this.attachmentParcer,
  });




  @Output() valueChange: EventEmitter<EditorVal> = new EventEmitter();

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
  public writeValue(value: EditorVal | null): void {
    if (!value) {
      this.value = new EditorVal()
      if (this.editor)
        this.editor.clear()
      return
    }
    let val = { ...value };

    if (typeof val?.json == 'object')
      val.json = val?.json
    else
      val.json = JSON.parse(val?.json || '{}')

    if (value)
      this.value = val;
    // this.initEditorJs();
    if (val.json && this.editorId)
      this.updateEditorJSValue(val.json)
  }

  onChange(value: EditorVal) {
    setTimeout(() => {
      this.touched()
      this.value = value;

      let val = { ...value }
      if(!value.html){
        val.json='{}'
      }
      // val.json = JSON.stringify(val.json)

      if (this.isFormControl) this.changed(val);
      this.valueChange.emit(val);
    }, 0);
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
    // this.editor.readOnly.toggle(true);
  }

  copyText(val: string) {
    this.coreService.copyTextToClipboard(val)
    this._alert.success('Copied!!')
  }


  ngAfterViewInit(): void {

    this.initEditorJs();
  }

  initEditorJs = () => {
    if (!this.editorId)
      return
    // if (this.editor)
    //   this.editor.clear();


    this.editor = new EditorJS({
      data: (this.value?.json as any) || {},
      placeholder: 'Let`s write an awesome story!',
      readOnly: this.isDisabled,

      holder: this.editorId,
      onReady: () => {
      },
      onChange: (api, event) => {
        this.saveEditorData();
        this.touched();
      },
      tools: {
        header: {
          class: Header,
          config: {
            levels: [2, 3, 4, 5, 6, 7],
            defaultLevel: 2
          }
        },
        list: {
          class: List,
          inlineToolbar: true,
          config: {
            defaultStyle: 'unordered'
          }
        },
        table: {
          class: Table,
          inlineToolbar: true,
          config: {
            rows: 2,
            cols: 3,
          },
        },
        raw: {
          class: RawTool,
        },
        quote: {
          class: Quote,
          config: {
            quotePlaceholder: 'Enter a quote',
            captionPlaceholder: 'Quote\'s author',
          },
        },
        image: {
          class: ImageToolExtended as any,

          config: {
            uploader: {
              uploadByFile: (file: File) => {
                let requestPayload = new FormData();
                requestPayload.append('file', file);

                return this._http.post(environment.rootFileEndPt + 'api/FileUploader/FileUpload?path=/editor&filename=' + file.name, requestPayload, { responseType: 'text' }).toPromise().then((resp: any) => {
                  if (resp) {
                    return {
                      success: 1,
                      file: {
                        url: environment.fileEndPoint + resp,
                        // any other image data you want to store, such as width, height, color, extension, etc
                      }
                    };
                  } else {
                    return {
                      success: 0,
                    }
                  }
                }).catch((err) => {
                })
              },
              uploadByUrl(url: string) {
                return new Promise((resolve, rej) => {
                  resolve(url);
                }).then((data) => {
                  return {
                    success: 1,
                    file: {
                      url: data
                    }
                  }
                })
              }
            }
          }
        },

        attaches: {
          class: AttachesToolExtended as any,
          inlineToolbar: true,
          config: {
            uploader: {

              uploadByFile: (file: File) => {
                let requestPayload = new FormData();
                requestPayload.append('file', file);
                return this._http.post(environment.rootFileEndPt + 'api/FileUploader/FileUpload?path=/editor&filename=' + file.name, requestPayload, { responseType: 'text' }).toPromise().then((resp: any) => {
                  if (resp) {
                    return {
                      success: 1,
                      file: {
                        url: environment.fileEndPoint + resp,
                        // any other image data you want to store, such as width, height, color, extension, etc
                      }
                    };
                  } else {
                    return {
                      success: 0,
                    }
                  }
                })
              },
              uploadByUrl(url: string) {
                return new Promise((resolve, rej) => {
                  resolve(url);
                }).then((data) => {
                  return {
                    success: 1,
                    file: {
                      url: data
                    }
                  }
                })
              }

            }
          }
        },
      },
    });


  }

  updateEditorJSValue(jsonValue: any) {
    if (!this.editorId)
      return
    if (this.editor && Object.keys(jsonValue).length)
      this.editor.blocks.render(jsonValue)
  }

  saveEditorData() {
    if (!this.editor.save)
      return
    this.editor.save().then((outputData: any) => {
      const html = this.edjsParser.parse(outputData).join('') || '';
      this.value = { json: JSON.stringify(outputData), html: html }
      this.onChange(this.value);
    }).catch((error: any) => {
    });
  }

  tableParcer(tableObj: any) {
    let data = tableObj.data.content;
    let isHeader = tableObj.data.withHeadings;
    let outputRows: string[] = [];
    let outputHeaderRow: string[] = [];

    for (let i = 0; i < data.length; i++) {
      let outputRow = ''
      let curRow = data[i]
      outputRow += `<tr>`
      for (let j = 0; j < curRow.length; j++) {
        let curCell = curRow[j]
        outputRow += `${(i === 0 && isHeader) ? '<th>' : '<td>'}${curCell}${(i === 0 && isHeader) ? '</th>' : '</td>'}`
      }
      outputRow += `</tr>`

      if (i === 0 && isHeader)
        outputHeaderRow.push(outputRow);
      else
        outputRows.push(outputRow);
    }

    let finalOutput = ''
    if (isHeader) {
      finalOutput += `<thead>${outputHeaderRow.join('')}</thead> <tbody> ${outputRows.join('')}</tbody>`
    } else {
      finalOutput += `<tbody> ${outputRows.join('')} </tbody>`
    }
    return `<div class="editor-table" ><table> ${finalOutput} </table></div>`

  }
  rawParcer(rawHtml: any) {
    return rawHtml.data.html
  }

  imageParcer(rawHtml: any) {
    let dt = rawHtml.data
    let html = `<figure>
      <img src="${dt.file.url}" alt="${dt.caption}" class="${dt.stretched ? 'strached' : ''}" >
      <figcaption>${dt.caption}</figcaption>
      </figure>`

    return html
  }
  quoteParcer(rawHtml: any) {
    let dt = rawHtml.data
    let html = `<blockquote style="text-align: ${dt.alignment == 'center' ? 'center' : 'left'};">
    ${dt.text}
    <cite>~${dt.caption}</cite>
    </blockquote>`
    return html
  }
  attachmentParcer(rawHtml: any) {
    return `<a title="Download" class="attachment" href="${rawHtml.data.file.url}" download>
    <i class="fa-solid fa-file-arrow-down"></i>
    ${ rawHtml.data.title}
     </a>`
  }



}




class AttachesToolExtended extends AttachesTool {

  httpClient = new HttpClient(new HttpXhrBackend({
    build: () => new XMLHttpRequest()
  }));

  removed() {
    let path = this['_data'].file?.url
    if (!path) return;

    this.httpClient.get(environment.rootFileEndPoint + 'RemoveFile?path=/editor/' + path.split('/').at(-1)).toPromise().then((resp: any) => {
      if (resp) {
        alert('delete success')
      } else {
        alert('delete failed')
      }
    })
  }
}

class ImageToolExtended extends ImageTool {

  httpClient = new HttpClient(new HttpXhrBackend({
    build: () => new XMLHttpRequest()
  }));

  removed() {
    let path = this['_data'].file?.url
    if (!path) return;

    this.httpClient.get(environment.rootFileEndPoint + 'RemoveFile?path=/editor/' + path.split('/').at(-1)).toPromise().then((resp: any) => {
      if (resp) {
        alert('deleted')
      } else {
        alert('failed')
      }
    })
  }


}