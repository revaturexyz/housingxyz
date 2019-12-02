import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountService } from './account.service';
import { AuthService } from './auth.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private account: AccountService, private auth: AuthService) {
    let decodedToken: string;
    this.UserId$.subscribe(currentUserId => {
      if (currentUserId === '') {
        account.getId$().subscribe(res => {
          this.userId.next(res);
        });
      }

      auth.getTokenSilently$().subscribe(res => {
        // atob decodes a Base64-encoded string
        decodedToken = atob(res.split('.')[1]);
        this.roles.next(JSON.parse(decodedToken)[environment.claimsDomain + 'roles']);
        this.email.next(JSON.parse(decodedToken)[environment.claimsDomain + 'email']);
      });
    });
  }

  private userId: BehaviorSubject<string> = new BehaviorSubject('');
  public readonly UserId$: Observable<string> = this.userId.asObservable();

  private roles: BehaviorSubject<string[]> = new BehaviorSubject([]);
  public readonly Roles$: Observable<string[]> = this.roles.asObservable();

  private email: BehaviorSubject<string> = new BehaviorSubject('');
  public readonly Email$: Observable<string> = this.email.asObservable();
}
