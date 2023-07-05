import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { alreadyLoginedGuard } from './already-logined.guard';

describe('alreadyLoginedGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => alreadyLoginedGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
