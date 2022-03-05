import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CameraService } from './services/camera.service';
import { GeolocationService } from './services/geolocation.service';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public photo$: Observable<string>;
  public location$: Observable<string>;
  title = 'mb-mobile';

  constructor(private readonly camera: CameraService,
    private readonly gps: GeolocationService) {
  }

  public snapPicture(): void {
    this.photo$ = this.camera.takePicture();
  }

  public getLocation(): void {
    this.location$ = this.gps.getCurrentPosition().pipe(
      map(c => `Lat: ${c.coords.latitude} Long: ${c.coords.longitude}`)
    );
  }
}
