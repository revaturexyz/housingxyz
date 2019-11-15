import { TestBed, getTestBed } from '@angular/core/testing';
import { CoordinatorService } from './coordinator.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CoordinatorService', () => {
    let myCoordinator: CoordinatorService;
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
            ],
            providers: [
                CoordinatorService,
                HttpClientTestingModule
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
