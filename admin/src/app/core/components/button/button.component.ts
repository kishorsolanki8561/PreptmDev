import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Location } from '@angular/common';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent implements OnInit {
  @Input() type: 'submit' | 'reset' | 'back' | 'edit' | 'delete' | 'add' | 'update'
  @Input() isLoading=false
  @Input() itemName = "this item"  // item name to be deleted
  @Input() show = true  // item name to be deleted
  @Output() onClick = new EventEmitter<void>();
  constructor(
    private _location: Location,
  ) { }

  ngOnInit(): void {
  }
  click() {
    this.onClick.emit();
  }
  onBack() {
    this._location.back();
  }

  onDelete(e: Event) {
    // e.preventDefault();
    // this._alertService.Question(`Do you want to delete "${this.itemName}"`, '', 'Delete', 'Cancel').then((status) => {
    //   if (status) {
    //     this.click()
    //   }
    // })
  }

}
