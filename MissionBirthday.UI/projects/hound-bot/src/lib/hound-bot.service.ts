import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface BotToken {
  token: string;
  userId: string;
}

@Injectable({
  providedIn: 'root'
})
export class HoundBotService {

  constructor(private http: HttpClient) { }

  public getToken(): Observable<BotToken> {
    return this.http.get<BotToken>('/api/HoundBot/token');
  }
}
