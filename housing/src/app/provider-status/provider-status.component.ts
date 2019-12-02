import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'dev-provider-status',
  templateUrl: './provider-status.component.html',
  styleUrls: ['./provider-status.component.scss']
})
export class ProviderStatusComponent implements OnInit {
  public currentProvider;

  constructor(
    private account: AccountService,
    private user: UserService) { 
      
    }

  ngOnInit() {
    // Populate default form values
    this.user.UserId$.subscribe(id => {
      this.account.getProviderById$(id).subscribe(res => {
        this.currentProvider = res;
        console.log(this.currentProvider);
      });
    });

  }

}
