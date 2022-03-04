import { Injectable } from '@angular/core';
import { Camera, CameraResultType } from '@capacitor/camera';
import { from, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CameraService {

  public takePicture(): Observable<string> {
    return from(Camera.getPhoto({
      allowEditing: true,
      resultType: CameraResultType.DataUrl
    })).pipe(
      map(photo => photo.dataUrl)
    );
  }
}
