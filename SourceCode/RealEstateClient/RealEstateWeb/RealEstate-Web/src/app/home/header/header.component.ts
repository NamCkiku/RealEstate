import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @ViewChild('modalLogin') public modalLogin: ModalDirective;
  @ViewChild('modalRegister') public modalRegister: ModalDirective;
  constructor() { }

  public showLoginModal(): void {
    this.modalLogin.show();
  }
  public hideLoginModal():void {
    this.modalLogin.hide();
  }
  public showRegisterModal(): void {
    this.modalRegister.show();
  }
  public hideRegisterModal():void {
    this.modalRegister.hide();
  }
  ngOnInit() {
  }

}
