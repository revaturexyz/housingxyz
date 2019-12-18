import { TestBed, getTestBed } from '@angular/core/testing';
import { TenantService } from './tenant.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('TenantService', () => {
    let myCoordinator: TenantService;
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                HttpClientTestingModule
            ],
            providers: [
                TenantService
            ]
        });

        const testBed = getTestBed();
        myCoordinator = testBed.get(TenantService);
    });


    it('should be created', () => {
        const service: TenantService = TestBed.get(TenantService);
        expect(service).toBeTruthy();
    });
});
