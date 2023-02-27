import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemRoutingModule } from './item-routing.module';
import { AngularMaterialModule } from 'src/angular-material/angular-material.module';
import { coerceElement } from '@angular/cdk/coercion';
import { CoreModule } from 'src/core/core.module';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ItemListComponent
  ],
  imports: [
    CommonModule,
    ItemRoutingModule,
    AngularMaterialModule,
    FormsModule
  ]
})
export class ItemModule { }
