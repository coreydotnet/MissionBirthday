import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { SampleDataService, SampleImage } from './sample-data.service';

@Component({
  selector: 'app-sample-image-picker',
  templateUrl: './sample-image-picker.component.html',
  styleUrls: ['./sample-image-picker.component.scss']
})
export class SampleImagePickerComponent implements OnInit {
  @Output() public imageSelected: EventEmitter<string> = new EventEmitter();
  public samples$: Observable<SampleImage[]>;

  constructor(private readonly sampleData: SampleDataService) { }

  ngOnInit(): void {
    this.samples$ = this.sampleData.getSampleImages();
  }

}
