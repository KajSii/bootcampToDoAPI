import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CoreModule } from "../../core/core.module";

@Component({
    selector: 'app-home',
    standalone: true,
    imports: [CommonModule, CoreModule],
    template: '<app-banner></app-banner>',
    styles: ['.container {display: none}']
})
export class HomeComponent {

}
