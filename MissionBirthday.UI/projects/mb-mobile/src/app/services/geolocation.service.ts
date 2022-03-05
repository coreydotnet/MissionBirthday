import { Injectable } from '@angular/core';
import { Geolocation, Position } from '@capacitor/geolocation';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeolocationService {

  public getCurrentPosition(): Observable<Position> {
    return from(Geolocation.getCurrentPosition());
  }
}
