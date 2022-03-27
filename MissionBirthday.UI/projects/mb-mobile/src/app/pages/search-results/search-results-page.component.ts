import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';
import { CharityEvent } from '../../interfaces/charity-event.interface';
import { EventService } from '../../services/event.service';

@Component({
  selector: 'app-search-results-page',
  templateUrl: './search-results-page.component.html',
  styleUrls: ['./search-results-page.component.scss']
})
export class SearchResultsPageComponent implements OnInit {
  public event$: Observable<CharityEvent[]>;
  public error: string;

  constructor(private readonly route: ActivatedRoute,
    private readonly events: EventService) { }

  ngOnInit(): void {
    this.event$ = this.events.getAreaEvents(this.route.snapshot.params.zipCode).pipe(
      catchError((err) => {
        this.error = 'Invalid Zip Code';
        return throwError(err);
      })
    );
  }

}
