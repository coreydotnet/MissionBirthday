import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { CameraService } from './services/camera.service';
import { GeolocationService } from './services/geolocation.service';
import { map, share, switchMap } from 'rxjs/operators';
import { AdminApiService } from './services/admin-api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public photo$: Observable<string>;
  public location$: Observable<string>;
  public ocr$: Observable<string>;
  title = 'mb-mobile';

  private photoSource$: Observable<string>;

  constructor(private readonly camera: CameraService,
    private readonly gps: GeolocationService,
    private readonly admin: AdminApiService) {
  }

  public snapPicture(): void {
    this.photoSource$ = this.camera.takePicture().pipe(
      share()
    );
    this.photo$ = this.photoSource$.pipe(
      map(photo => `data:image/png;base64,${photo}`)
    );

    // Upload later
    this.ocr$ = this.photoSource$.pipe(
      map(base64 => this.camera.getPhotoBlob(base64)),
      switchMap(imageBlob => this.admin.uploadForOcr(imageBlob)),
      map(response => response.details)
    )
  }

  public getLocation(): void {
    this.location$ = this.gps.getCurrentPosition().pipe(
      map(c => `Lat: ${c.coords.latitude} Long: ${c.coords.longitude}`)
    );
  }
}
