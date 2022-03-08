import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface EventUploadResponse {
  id: number;

  details: string;
  email: string;
  endTime: Date;
  organization: string;
  phoneNumber: string;
  startTime: Date;
  url: string;

  location: unknown; // Fill out later
}

@Injectable({
  providedIn: 'root'
})
export class AdminApiService {

  constructor(private readonly http: HttpClient) { }

  public uploadForOcr(file: Blob): Observable<EventUploadResponse> {
    const formData = new FormData();
    formData.append('file', file);

    return this.http.post<EventUploadResponse>("/api/Events/upload", formData);
  }
}
