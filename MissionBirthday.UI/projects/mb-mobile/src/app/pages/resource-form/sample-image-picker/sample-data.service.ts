import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { CharityEvent } from '../../../interfaces/charity-event.interface';

export interface SampleImage {
  key: string;
  name: string;
  sourcePath: string;
}

@Injectable({
  providedIn: 'root'
})
export class SampleDataService {

  constructor(private readonly http: HttpClient) { }

  public getSampleImages(): Observable<SampleImage[]> {
    return this.http.get<SampleImage[]>('/api/Examples');
  }

  public getSampleEventData(key: string): Observable<CharityEvent> {
    return this.http.post<{ event: CharityEvent }>(`/api/Examples/${key}`, null).pipe(
      map(response => response.event)
    );
  }
}
