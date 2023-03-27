import { AccountService } from './../_service/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
  }

  login(){
    this.accountService.login(this.model).subscribe({
      next: repones =>{
        console.log(repones);
        this.loggedIn = true;
      },
      error: err =>{
        console.log(err);
      }
    });
  }

  logout(){
    this.accountService.logout();
    this.loggedIn = false;
  }

}
