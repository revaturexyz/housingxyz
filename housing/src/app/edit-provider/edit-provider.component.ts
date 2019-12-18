import { Component, OnInit } from '@angular/core';
import { Provider } from 'src/interfaces/account/provider';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'dev-edit-provider',
  templateUrl: './edit-provider.component.html',
  styleUrls: ['./edit-provider.component.scss']
})
export class EditProviderComponent implements OnInit {

  formProvider: Provider;

  public selectOptionInvalid = '';

  constructor(
    private router: Router,
    private account: AccountService,
    private user: UserService) { }

  ngOnInit() {
    // Populate default form values
    this.user.UserId$.subscribe(id => {
      this.account.getProviderById$(id).subscribe(res => {
        this.formProvider = res;
        console.log(this.formProvider);
      });
    });
  }

  // this method is called when the user clicks the submit button to update provider information
  putUpdateProvider() {
    // handle the put here
    this.user.UserId$.subscribe(() => {
      this.account.updateProviderById$(this.formProvider);
    });
    this.router.navigate(['']);
  }

  // this method is called if the user clicks the Cancel button
  cancelAddProvider(): void {
    this.router.navigate(['']);
  }
}
