import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';
import { NzMessageService } from 'ng-zorro-antd/message';



@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor(
        private _message:NzMessageService
    ) { }
    
    success(message:string){
        this._message.success(message,{nzAnimate:true,nzDuration:3000,nzPauseOnHover:true})
    }
    error(message:string){
        this._message.error(message,{nzAnimate:true,nzDuration:6000,nzPauseOnHover:true})
    }

    Question(message: string, title: string = '', okText: string = 'Yes', cancelText: string = 'No'): Promise<boolean> {
        return new Promise<boolean>((resolve) => {
            Swal.fire({
                icon: "question",
                title: title,
                text: message,
                showCancelButton: true,
                showCloseButton: false,
                confirmButtonText: okText,
                cancelButtonText: cancelText
            }).then((result: any) => {
                if (result.value)
                    return resolve(true);
                else if (result.dismiss == Swal.DismissReason.cancel)
                    return resolve(false);
                else
                    return resolve(false);
            });
        });
    }

    /*
    Error(message: string, title: string = '', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "error",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }


    Success(message: string): Promise<void> {
        return new Promise<void>((resolve) => {

            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                didOpen: (toast) => {
                  toast.addEventListener('mouseenter', Swal.stopTimer)
                  toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
              })

              Toast.fire({
                icon: 'success',
                title: message
              }).then(() => resolve());
        });
    }

    Warning(message: string, title: string = '', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "error",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    Info(message: string, title: string = '', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "info",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    
    showConfim(message: string, title: string = '', yesCallback: Function, noCallback?: Function) {
      Swal.fire({
        icon: "question",
        title: title,
        text: message ? message : 'Are you sure',
        showCancelButton: true,
        showCloseButton: false,
        confirmButtonText: "Yes",
        cancelButtonText: "No"
      }).then((result) => {
        if (result.value)
          yesCallback()
        else if (noCallback)
          noCallback()
      })
    }

    */
}
