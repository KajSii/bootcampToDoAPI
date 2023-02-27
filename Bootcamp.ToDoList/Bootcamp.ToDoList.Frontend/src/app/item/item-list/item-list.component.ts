import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { mergeMap } from 'rxjs';
import { Item } from 'src/app/models/item';
import { List } from 'src/app/models/list';
import { ItemApiService } from '../item-api.service';
import { ItemUpdateComponent } from '../item-update/item-update.component';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent implements OnInit{

  lists: List[] = [];
  selectedOption: string = 'default';
  newItem = {} as Item;
  newList = {} as List;
  editItem = {} as Item;
  deadline: Date | undefined;
  minDate = new Date();
  dateControl = new FormControl(new Date());
  @ViewChild('picker') picker: any
  displayedColumns: string[] = ['status', 'name', 'deadline', 'actions']; // can add 'deadline' later

  constructor(private itemApiService: ItemApiService, private router: Router, private snackBar: MatSnackBar, private dialog: MatDialog) {
    
  }

  ngOnInit(): void {
    if (!localStorage.getItem("token")) {
      this.snackBar.open('You have to be logged in to access your tasks.', 'OK');
      this.router.navigateByUrl('/login');
    }
    this.getAvailableLists();
  }


  getAvailableLists(): void {
    this.itemApiService.getLists()
      .subscribe((lists: List[]) => {
        this.lists = lists;
        this.addItemsToList(this.lists);
      })
  }


  addItemsToList(lists: List[]): void {
    for (var list of lists) {
      this.itemApiService.getItems(list.publicId!)
        .subscribe((items: Item[]) => {
          list.items = items;
        });
    }
  }

  deleteItem(itemId: string | undefined, listId: string): void {
    this.itemApiService.deleteItem(itemId).subscribe(() => {
      this.snackBar.open("Item deleted.", 'OK');
      this.getAvailableLists();
    });
  }

  createItem(listId: string): void {
    this.itemApiService.createItem(listId, this.newItem)
      .subscribe((item: Item) => {
        if (item) {
          this.snackBar.open("Task created.", 'OK');
          this.getAvailableLists();
          this.newItem.name = '';
        };
        // this.lists = this.lists?.filter((item1) => item1.publicId === item.publicId);
      });
  }

  openEditDialog(itemId: string):void {
    const dialogRef = this.dialog.open(ItemUpdateComponent, { width: '275px', data: {} });

    dialogRef.afterClosed().subscribe((result: Item) => {
      if (result) {
        this.itemApiService.getItem(itemId).subscribe((item: Item) => {
          this.editItem.name = result.name;
          this.editItem.endTime = result.endTime;
          this.itemApiService.updateItem(itemId, this.editItem)
          .subscribe(() => {
            this.snackBar.open("Task edited.", 'OK');
            this.getAvailableLists();
            this.editItem.name = '';
            this.editItem.endTime = undefined;
          })
        });
        
      }
    });
  }

  createList(): void {
    this.itemApiService.createList(this.newList)
      .subscribe((list: List) => {
        if (list) {
          this.snackBar.open("Table created.", 'OK');
          this.getAvailableLists();
          this.newList.name = '';
        }
      })
  }

  deleteList(listId: string): void {
    this.itemApiService.deleteList(listId)
      .subscribe(() => {
        this.snackBar.open("Table deleted.", 'OK');
        this.getAvailableLists();
      });
  }

  changeStatus(itemId: string) {
    this.itemApiService.updateItemStatus(itemId, {} as Item)
      .subscribe((item: Item) => {
        if (item.status == true) {
          this.snackBar.open("Task completed.", 'OK');
        }
        else if (item.status == false) {
          this.snackBar.open("Task UNcompleted.", 'OK')
        }
        this.getAvailableLists();
      });
  }


}