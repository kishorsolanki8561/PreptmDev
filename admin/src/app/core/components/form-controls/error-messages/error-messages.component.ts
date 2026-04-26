import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-error-messages',
  templateUrl: './error-messages.component.html',
  styleUrls: ['./error-messages.component.scss']
})
export class ErrorMessagesComponent implements OnInit {

  constructor() { }

  @Input() formField:FormControl
  @Input() label:string|undefined
  @Input() maxLength:null|number = null

  ngOnInit(): void {
  }

}
