import { Injectable } from '@angular/core';
import { Provider } from '../../models/Provider';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ProviderServiceService {

  constructor(private httpBus: HttpClient) { }

  
}
