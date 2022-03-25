import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CharityEvent } from '../interfaces/charity-event.interface';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor(private readonly http: HttpClient) { }

  public getAreaEvents(zip: string): Observable<CharityEvent[]> {
    return this.http.get<CharityEvent[]>(`/api/Events/search/${zip}`);
  }
}
