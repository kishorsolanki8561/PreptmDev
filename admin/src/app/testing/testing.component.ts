import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FilesWithPrev } from '../core/models/FormElementsModel';
import { FileRequired } from '../core/validators/form.validator';

@Component({
  selector: 'app-testing',
  templateUrl: './testing.component.html',
  styleUrls: ['./testing.component.scss']
})
export class TestingComponent implements OnInit {
  form: FormGroup


  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      profile: [null,[FileRequired]]
    })

    // this.form.patchValue({
    //   profile:
    //   {
    //     file: [],
    //     urls: [
    //       {
    //         id: 1,
    //         path: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
    //       },
    //       {
    //         id: 2,
    //         path: 'https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png'
    //       },
    //     ]

    //   }
    // })
  }
}
