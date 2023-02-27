import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ItemListComponent } from './item-list/item-list.component';
import { ItemRoutingModule } from './item-routing.module';
import { AngularMaterialModule } from 'src/angular-material/angular-material.module';
import { coerceElement } from '@angular/cdk/coercion';
import { CoreModule } from 'src/core/core.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ItemUpdateComponent } from './item-update/item-update.component';
import { NgxMatDatetimePickerModule, NgxMatNativeDateModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';



@NgModule({
  declarations: [
    ItemListComponent,
    ItemUpdateComponent
  ],
  imports: [
    CommonModule,
    ItemRoutingModule,
    AngularMaterialModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
    NgxMatDatetimePickerModule,
    MatDatepickerModule,
    MatInputModule,
    MatButtonModule,
    MatNativeDateModule
  ]
})
export class ItemModule { }
