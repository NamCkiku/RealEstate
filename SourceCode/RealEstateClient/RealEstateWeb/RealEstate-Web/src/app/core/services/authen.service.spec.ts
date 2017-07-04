import { TestBed, inject } from '@angular/core/testing';

import { AuthenService } from './authen.service';

describe('AuthenService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthenService]
    });
  });

  it('should be created', inject([AuthenService], (service: AuthenService) => {
    expect(service).toBeTruthy();
  }));
});
