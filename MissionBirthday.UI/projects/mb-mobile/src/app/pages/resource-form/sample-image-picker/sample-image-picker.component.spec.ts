import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SampleImagePickerComponent } from './sample-image-picker.component';

describe('SampleImagePickerComponent', () => {
  let component: SampleImagePickerComponent;
  let fixture: ComponentFixture<SampleImagePickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SampleImagePickerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SampleImagePickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
