import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemRoutingModule } from './item-routing.module';



@NgModule({
  declarations: [
    ItemListComponent
  ],
  imports: [
    CommonModule,
    ItemRoutingModule
  ]
})
export class ItemModule { }
