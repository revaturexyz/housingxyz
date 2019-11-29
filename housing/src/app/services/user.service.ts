import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountService } from './account.service';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor (private account: AccountService, private auth: AuthService) {
    let decodedToken: string;

    this.userId$.subscribe(currentUserId => {
      if (currentUserId == "")
      {
        account.getId$().subscribe(res => {
          this._userId.next(res) 
        });
      }

      auth.getTokenSilently$().subscribe(res => {
        // atob decodes a Base64-encoded string
        decodedToken = atob(res.split('.')[1]);
        
        this._roles.next(JSON.parse(decodedToken)[environment.claimsDomain + 'roles']);
        this._email.next(JSON.parse(decodedToken)[environment.claimsDomain + 'email']);
      });
    });
   }

  private _userId: BehaviorSubject<string> = new BehaviorSubject("");
  public readonly userId$: Observable<string> = this._userId.asObservable();

  private _roles: BehaviorSubject<string[]> = new BehaviorSubject([]);
  public readonly roles$: Observable<string[]> = this._roles.asObservable();

  private _email: BehaviorSubject<string> = new BehaviorSubject("");
  public readonly email$: Observable<string> = this._email.asObservable();
  
}