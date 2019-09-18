import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Amenity } from 'src/interfaces/amenity';

@Injectable({
  providedIn: 'root'
})
export class ComplexService {
  apiUrl = environment.endpoints.providerXYZ;
  constructor(
    private httpBus: HttpClient
  ) { }

  getAmenityByComplex(id: number): Observable<Amenity[]> {
    return this.httpBus.get<Amenity[]>(this.apiUrl + `Complex/${id}/amenity`);
  }
}
