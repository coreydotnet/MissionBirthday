import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IonModal, LoadingController } from '@ionic/angular';
import { catchError, from, map, shareReplay, switchMap, tap, throwError, withLatestFrom } from 'rxjs';
import { AdminApiService } from '../../services/admin-api.service';
import { CameraService } from '../../services/camera.service';
import { SampleDataService } from './sample-image-picker/sample-data.service';

@Component({
  selector: 'app-resource-form-page',
  templateUrl: './resource-form-page.component.html',
  styleUrls: ['./resource-form-page.component.scss']
})
export class ResourceFormPageComponent implements OnInit {
  public resourceForm: FormGroup;
  public locationGroup: FormGroup;
  @ViewChild(IonModal) public  sampleModal;

  constructor(private readonly camera: CameraService,
    private readonly adminApi: AdminApiService,
    private readonly sampleData: SampleDataService,
    private readonly loadingController: LoadingController,
    private readonly router: Router) { }

  ngOnInit(): void {
    this.resourceForm = new FormGroup({
      organization: new FormControl('', Validators.required),
      title: new FormControl('', Validators.required),
      phoneNumber: new FormControl(),
      email: new FormControl(),
      url: new FormControl(),
      location: new FormGroup({
        street1: new FormControl(),
        street2: new FormControl(),
        city: new FormControl('', Validators.required),
        state: new FormControl('', [Validators.required, Validators.minLength(2)]),
        zip: new FormControl('', [Validators.required, Validators.minLength(5)])
      }),
      date: new FormControl(),
      time: new FormControl(),
      details: new FormControl(),
      items: new FormControl([])
    });
    this.locationGroup = this.resourceForm.controls.location as FormGroup;
  }

  addItem(input: HTMLInputElement): void {
    // I did not say it was OK to do this in real life
    this.resourceForm.controls.items.value.push(input.value);
    input.value = '';
  }

  removeItem(index): void {
    this.resourceForm.controls.items.value.splice(index, 1);
  }

  addEvent(): void {
    if (this.resourceForm.valid) {
      this.adminApi.createEvent(this.resourceForm.getRawValue()).subscribe(() => {
        this.router.navigate(['..']);
      });
    } else {
      this.resourceForm.markAllAsTouched();
    }
  }

  getFieldsFromImage(): void {
    const spinner$ = from(this.loadingController.create()).pipe(
      shareReplay()
    );
    spinner$.pipe(
      tap(spinner => spinner.present()),
      switchMap(() => this.camera.takePicture()),
      map(image => this.camera.getPhotoBlob(image)),
      switchMap(blob => this.adminApi.uploadForOcr(blob)),
      withLatestFrom(spinner$),
      catchError((err) => {
        return spinner$.pipe(
          tap(spinner => spinner.dismiss()),
          switchMap(() => throwError(err))
        )
      })
    ).subscribe(([parsedEvent, spinner]) => {
      this.resourceForm.patchValue(parsedEvent);
      spinner.dismiss();
    });
  }

  public autofillSample(key: string) {
    this.sampleModal.dismiss();
    const spinner$ = from(this.loadingController.create()).pipe(
      shareReplay()
    );

    spinner$.pipe(
      tap(spinner => spinner.present()),
      switchMap(() => this.sampleData.getSampleEventData(key)),
      withLatestFrom(spinner$)
    ).subscribe(([parsedEvent, spinner]) => {
      this.resourceForm.patchValue(parsedEvent);
      spinner.dismiss();
    });
  }
}
