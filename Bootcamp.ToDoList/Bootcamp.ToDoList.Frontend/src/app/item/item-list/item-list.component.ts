import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Item } from 'src/app/models/item';
import { List } from 'src/app/models/list';
import { ItemApiService } from '../item-api.service';

@Component({
  selector: 'app-item-list',
  templateUrl: './item-list.component.html',
  styleUrls: ['./item-list.component.scss']
})
export class ItemListComponent implements OnInit{

  lists: List[] | undefined;
  selectedOption: string = 'default';

  constructor(private itemApiService: ItemApiService, private router: Router) { }

  ngOnInit(): void {
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
      this.itemApiService.getItems(list.publicId)
        .subscribe((items: Item[]) => {
          list.items = items;
        });
    }
  }

  deleteItem(itemId: string): void {
    this.itemApiService.deleteItem(itemId);
  }

  createItem(listId: string): void {
    var item = {} as Item;
    item.name = "new";
    this.itemApiService.createItem(listId, item);
    this.itemApiService.getList(listId);
  }
}