import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CameraService } from './services/camera.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public photo$: Observable<string>;
  title = 'mb-mobile';

  constructor(private readonly camera: CameraService) {
  }

  public snapPicture(): void {
    this.photo$ = this.camera.takePicture();
  }
}
