import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../../models/room';
import { Amenity} from '../../models/amenity';
import { TrainingCenter} from '../../models/trainingcenter';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TrainingcenterService {

  trainingcenter: TrainingCenter = new TrainingCenter(
    1,
    "UTA",
    "1001 s. Center St",
    "Arlington",
    "Texas",
    "76010",
    "UTA",
    "1231231234"
  );
  trainingcenter2: TrainingCenter = new TrainingCenter(
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

  getTrainingCenters(): Observable<TrainingCenter[]>{
    return of([this.trainingcenter, this.trainingcenter2]);
  }
  getComplexesByTrainingCenter(centerId: number): Observable<TrainingCenter[]>{
    return of([this.trainingcenter, this.trainingcenter2]);
  }
}
