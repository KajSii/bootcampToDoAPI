import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { BannerComponent } from './banner/banner.component';
import { RouterLink, RouterLinkActive } from '@angular/router';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    BannerComponent
  ],
  imports: [
    CommonModule,
    RouterLink,
    RouterLinkActive
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    BannerComponent
  ]
})
export class CoreModule { }
