import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity} from '../../models/amenity';
import { Trainingcenter} from '../../models/trainingcenter';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingcenterService {

  trainingcenter: Trainingcenter = new Trainingcenter(
    1,
    "UTA",
    "1001 s. Center St",
    "Arlington",
    "Texas",
    "76010",
    "UTA",
    "1231231234"
  );
  trainingcenter2: Trainingcenter = new Trainingcenter(
    2,
    "UIC",
    "123 s. Chicago Ave",
    "Chicago",
    "Illinois",
    "60645",
    "UIC",
    "3213213214"
  );
  constructor(private http : HttpClient) { }

  getTrainingCenters(): Observable<Trainingcenter[]>{
    return of([this.trainingcenter, this.trainingcenter2]);
  }
  getComplexesByTrainingCenter(centerId: number): Observable<Trainingcenter[]>{
    return of([this.trainingcenter, this.trainingcenter2]);
  }
}
