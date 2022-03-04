import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HoundBotComponent } from './hound-bot.component';

describe('HoundBotComponent', () => {
  let component: HoundBotComponent;
  let fixture: ComponentFixture<HoundBotComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HoundBotComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HoundBotComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
