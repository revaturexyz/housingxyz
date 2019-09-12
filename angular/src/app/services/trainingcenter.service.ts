import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity} from '../../models/amenity';
import { Observable, of } from 'rxjs';
import { TrainingCenter} from '../../interfaces/trainingcenter';
import { TestServiceData } from './static-test-data';
import { Complex } from 'src/interfaces/complex';

@Injectable({
  providedIn: 'root'
})
export class TrainingcenterService {

  constructor(private http: HttpClient) { }

  getTrainingCenters(): Observable<TrainingCenter[]> {
    return of([TestServiceData.trainingcenter, TestServiceData.trainingcenter2]);
  }

  getComplexesByTrainingCenter(centerId: number): Observable<Complex[]> {
    return of([TestServiceData.dummyComplex, TestServiceData.dummyComplex2]);
  }
}
