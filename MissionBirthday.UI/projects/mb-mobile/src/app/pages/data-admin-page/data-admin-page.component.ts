import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CharityEvent } from '../../interfaces/charity-event.interface';
import { AdminApiService } from '../../services/admin-api.service';

@Component({
  selector: 'app-data-admin-page',
  templateUrl: './data-admin-page.component.html',
  styleUrls: ['./data-admin-page.component.scss']
})
export class DataAdminPageComponent implements OnInit {
  public events$: Observable<CharityEvent[]>;

  constructor(private readonly api: AdminApiService) { }

  ngOnInit(): void {
    this.events$ = this.api.getAllEvents();
  }

  removeEvent(eventId: number): void {
    this.api.deleteEvent(eventId).subscribe(() => {
      this.events$ = this.api.getAllEvents();
    });
  }
}
