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
export class ItemListComponent implements OnInit {

  lists: List[] = [];
  items: Item[] = [];
  showFiller = false;
  selectedOption: string = 'default';

  constructor(private itemApiService: ItemApiService, private router: Router) { }

  ngOnInit(): void {
    this.getAvailableLists();
    // this.addItemsToList();
  }

  getAvailableLists() {
    this.itemApiService.getLists()
      .subscribe((lists: List[]) => {
        this.lists = lists;
      });

    for (var list of this.lists) {
      this.addItemsToList(list.publicId);
    }
  }


  addItemsToList(listId: string) {
    this.itemApiService.getItems(listId)
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
    this.router.navigateByUrl(`Items/${item.publicId}`, { state: { item } });
  }
}