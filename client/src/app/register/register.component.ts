import { AccountService } from './../_service/account.service';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {}

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
        next: response => {
          console.log(response);
          this.cancel();
        },
        error: error => this.toastr.error(error.error)
      }
    );
  }

  cancel(){
    this.cancelRegister.emit(false);
  }


}
