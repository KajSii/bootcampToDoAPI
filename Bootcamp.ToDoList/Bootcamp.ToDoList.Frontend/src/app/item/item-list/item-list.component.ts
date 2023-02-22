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
  items: Item[] | undefined;
  selectedOption: string = 'default';

  constructor(private itemApiService: ItemApiService, private router: Router) { }

  ngOnInit(): void {
    this.getAvailableLists();
    this.addItemsToList();
  }

  getAvailableLists() {
    this.itemApiService.getLists()
      .subscribe((lists: List[]) => {
        this.lists = lists;
      });
  }

  
  addItemsToList() {
    this.itemApiService.getItems()
      .subscribe((items: Item[]) => {
        this.items = items;
      });
  }
  

  listChanged($event: any) {
    if (this, this.selectedOption === 'default') {
      return;
    }

    this.itemApiService.getList($event)
      .subscribe((list) => {
        this.items = list.items!;
      });
  }

  goToItem(item: Item) {
    this.router.navigateByUrl(`Items/${item.publicId}`, {state: {item}});
  }
}