import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Amenity } from 'src/interfaces/amenity';
import { MsalService } from '@azure/msal-angular';

@Injectable({
  providedIn: 'root'
})
export class ComplexService {

  apiUrl: string = environment.endpoints.providerXYZ + 'api/';

  httpOptions: any;
  constructor(
    private httpBus: HttpClient,
    msalService: MsalService
  ) {
    this.httpOptions = {
      headers: new HttpHeaders({
          'Authorization': msalService.getUser().userIdentifier
      })
    }
  }

  getAmenityByComplex(id: number): Observable<any> {
    return this.httpBus.get<Amenity[]>(this.apiUrl + `Complex/${id}/amenity`, this.httpOptions);
  }
}
