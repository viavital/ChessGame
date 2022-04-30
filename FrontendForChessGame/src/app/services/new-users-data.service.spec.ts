import { TestBed } from '@angular/core/testing';

import { NewUsersDataService } from './new-users-data.service';

describe('NewUsersDataService', () => {
  let service: NewUsersDataService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NewUsersDataService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
