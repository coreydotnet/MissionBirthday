import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DataAdminPageComponent } from './data-admin-page.component';

describe('DataAdminAgeComponent', () => {
  let component: DataAdminPageComponent;
  let fixture: ComponentFixture<DataAdminPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DataAdminPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DataAdminPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
