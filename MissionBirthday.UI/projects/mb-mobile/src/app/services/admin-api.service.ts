import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, mapTo, Observable } from 'rxjs';
import { CharityEvent } from '../interfaces/charity-event.interface';

@Injectable({
  providedIn: 'root'
})
export class AdminApiService {

  constructor(private readonly http: HttpClient) { }

  public uploadForOcr(file: Blob): Observable<CharityEvent> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<{ event: CharityEvent }>("/api/Events/upload", formData).pipe(
      map(response => response.event)
    );
  }

  public createEvent(event: CharityEvent): Observable<void> {
    return this.http.post('/api/Events', event).pipe(
      mapTo(null)
    );
  }
}
