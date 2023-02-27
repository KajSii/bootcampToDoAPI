import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { mergeMap } from 'rxjs';
import { Item } from 'src/app/models/item';
import { List } from 'src/app/models/list';
import { ItemApiService } from '../item-api.service';

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
  displayedColumns: string[] = ['status', 'name', 'actions'];

  constructor(private itemApiService: ItemApiService, private router: Router, private snackBar: MatSnackBar) {
    
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

  editItem(itemId: string): void {
    this.itemApiService.updateItem(itemId, this.newItem).subscribe();
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
    this.itemApiService.deleteList(listId).subscribe(() => {
      this.snackBar.open("Table deleted.", 'OK');
      this.getAvailableLists();
    });
  }
}