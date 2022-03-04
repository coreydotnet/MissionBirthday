import { TestBed } from '@angular/core/testing';

import { HoundBotService } from './hound-bot.service';

describe('HoundBotService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HoundBotService = TestBed.get(HoundBotService);
    expect(service).toBeTruthy();
  });
});
