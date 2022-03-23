import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResourceFormPageComponent } from './resource-form-page.component';

describe('ResourceFormPageComponent', () => {
  let component: ResourceFormPageComponent;
  let fixture: ComponentFixture<ResourceFormPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResourceFormPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResourceFormPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
