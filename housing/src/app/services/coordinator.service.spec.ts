import { TestBed, getTestBed } from '@angular/core/testing';
import { CoordinatorService } from './coordinator.service';

describe('CoordinatorService', () => {
    let myCoordinator: CoordinatorService;
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
            ],
            providers: [
                CoordinatorService
            ]
        });

        const testBed = getTestBed();
        myCoordinator = testBed.get(CoordinatorService);
    });


    it('should be created', () => {
        const service: CoordinatorService = TestBed.get(CoordinatorService);
        expect(service).toBeTruthy();
    });
});
