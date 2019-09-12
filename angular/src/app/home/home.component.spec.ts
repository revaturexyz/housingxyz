// import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';

<<<<<<< HEAD
// import { HomeComponent } from './home.component';
// import { HttpClientTestingModule } from '@angular/common/http/testing';
// import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
// import { MsAdalAngular6ServiceMock } from '../testing/mock/Mock-adal-service';
// import { RouterTestingModule } from '@angular/router/testing';
// import { MockProvider, LocationData, mockLocationList } from '../testing/dummyData';
// import { AppComponent } from '../app.component';
// import { ApiService } from '../api.service';
// import { ApiServiceMock } from '../testing/mock/mock-api-service';
// import { FormsModule, ReactiveFormsModule} from '@angular/forms';
// import { Router } from '@angular/router';
// import { doesNotThrow } from 'assert';
=======
import { HomeComponent } from './home.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MsAdalAngular6Service } from 'microsoft-adal-angular6';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from '../app.component';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { Router } from '@angular/router';
import { doesNotThrow } from 'assert';
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819

// describe('HomeComponent', () => {
//   let component: HomeComponent;
//   let fixture: ComponentFixture<HomeComponent>;

<<<<<<< HEAD
//   beforeEach(async(() => {
//     TestBed.configureTestingModule({
//       declarations: [ HomeComponent ],
//       imports:[
//         HttpClientTestingModule,
//         RouterTestingModule,
//         FormsModule,
//         ReactiveFormsModule
        
//       ],
//       providers:[
//         {
//           provide:MsAdalAngular6Service,useClass:MsAdalAngular6ServiceMock
//         }
//       ]
//     })
//     .compileComponents();
//   }));

//   beforeEach(() => {
//     fixture = TestBed.createComponent(HomeComponent);
//     component = fixture.componentInstance;
//     // spyOn(component, 'getLocationInfo');
//     // spyOn(component, 'getRoomInfo');
//     // component.locationList = mockLocationList[0]; 
//     // spyOn(component, 'ngOnInit');
//     fixture.detectChanges();
//   });

//   it('should create', () => {
//     expect(component).toBeTruthy();
//   });

  
//    it ('should call getLocationInfo()', () =>{
//     spyOn(component, 'getLocationInfo');
//     component.getLocationInfo();
//     expect(component.getLocationInfo).toHaveBeenCalled();
//   });
//   it ('should call getRoomInfo()', () =>{ 
//       spyOn(component, 'getRoomInfo') 
//       component.getRoomInfo(); 
//       expect(component.getRoomInfo).toHaveBeenCalled();
//   });
//   it ('showLocation(id : number) should navigate to add-room', inject([Router],(router : Router) => {
//       spyOn(router, 'navigate').and.stub();
//       component.showLocation(1);
//       expect(router.navigate).toHaveBeenCalledWith(['add-room', 1]);
//   }));
//   it ('updateRoom(id : number) should navigate to update-room', inject([Router], (router : Router) => {
//     spyOn(router, 'navigate').and.stub();
//     component.updateRoom(1);
//     expect(router.navigate).toHaveBeenCalledWith(['update-room', 1])
//   }));
//   // it ('ngOnInit should call setTimeout()', () => {
//   //  spyOn(component, 'ngOnInit');
//   //  spyOn(global, 'setTimeout')
//   //   component.ngOnInit();
//   //   setTimeout(()=>{
//   //     this.getLocationInfo()
//   //    }, 300);
//   //   expect(global.setTimeout).toHaveBeenCalled(); 
     
//   // });

// });
=======
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomeComponent ],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        FormsModule,
        ReactiveFormsModule
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    // spyOn(component, 'getLocationInfo');
    // spyOn(component, 'getRoomInfo');
    // component.locationList = mockLocationList[0];
    // spyOn(component, 'ngOnInit');
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
>>>>>>> 37203c3dd2148c5500c3d2bc1e569c7593931819
