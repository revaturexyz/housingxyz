import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Room } from '../models/room';
import { Amenity} from '../models/Amenity';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })

export class ApiService {
    
    constructor(private http : HttpClient) { }

//     getTrainingCenters(): Observable<TrainingCenter[]>
// get full details (ie. address information)
// getComplexesByTrainingCenter(centerId: int): Observable<Complex[]>

}