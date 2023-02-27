import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Item } from 'src/app/models/item';

@Component({
  selector: 'app-item-update',
  templateUrl: './item-update.component.html',
  styleUrls: ['./item-update.component.scss']
})
export class ItemUpdateComponent {

  item = {} as Item;

  constructor(
    public dialogRef: MatDialogRef<ItemUpdateComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Item) {
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
