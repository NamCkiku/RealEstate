import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './header.component';
import { ModalModule } from 'ngx-bootstrap/modal';
@NgModule({
  imports: [
    CommonModule,
     ModalModule.forRoot(),
  ],
  declarations: [HeaderComponent]
})
export class HeaderModule { }
