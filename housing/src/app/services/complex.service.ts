import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Amenity } from 'src/interfaces/amenity';
import { Complex } from 'src/interfaces/complex';

@Injectable({
  providedIn: 'root'
})
export class ComplexService {

  complexApiUrl: string = environment.endpoints.complex + 'api/';

  httpOptions: any;

  constructor(private httpBus: HttpClient) { }

  getAmenityByComplex(id: number): Observable<any> {
    return this.httpBus.get<Amenity[]>(this.complexApiUrl + `Complex/${id}/amenity`, this.httpOptions);
  }
  getAllComplex(): Observable<any> {
    return this.httpBus.get<Complex[]>(this.complexApiUrl + `Complex/Getallcomplex`, this.httpOptions);
  }

  getComplexInfoByID(id: string): Observable<any> {
    return this.httpBus.get<Complex>(this.complexApiUrl + `complex/${id}`, this.httpOptions);
  }

  getComplexByProvider(id: string): Promise<any> {
    return this.httpBus.get<Complex[]>(this.complexApiUrl + `complex/provierId/${id}`, this.httpOptions).toPromise().then();
  }

}

