import { Injectable } from '@angular/core';
import { Camera, CameraResultType } from '@capacitor/camera';
import { from, Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CameraService {

  public takePicture(): Observable<string> {
    return from(Camera.getPhoto({
      allowEditing: true,
      resultType: CameraResultType.Base64
    })).pipe(
      map(photo => photo.base64String)
    );
  }

  public getPhotoBlob(base64: string): Blob {
    const rawData = atob(base64);
    const bytes = new Array(rawData.length);
    for (var x = 0; x < rawData.length; x++) {
        bytes[x] = rawData.charCodeAt(x);
    }
    const arr = new Uint8Array(bytes);
    return new Blob([arr], {type: 'image/png'});
  }
}
