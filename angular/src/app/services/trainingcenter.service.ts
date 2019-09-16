import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TrainingCenter } from '../../interfaces/trainingcenter';
import { Observable, of } from 'rxjs';
import { TestServiceData } from './static-test-data';
import { Complex } from 'src/interfaces/complex';

@Injectable({
  providedIn: 'root'
})
export class TrainingCenterService {

  constructor() { }

  getTrainingCenters(): Observable<TrainingCenter[]> {
    return of([TestServiceData.trainingcenter, TestServiceData.trainingcenter2]);
  }

  getComplexesByTrainingCenter(): Observable<Complex[]> {
    return of([TestServiceData.dummyComplex, TestServiceData.dummyComplex2]);
  }
}
