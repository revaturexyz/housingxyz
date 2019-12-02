// import { Injectable } from '@angular/core';
// import { environment } from 'src/environments/environment';
// import { Observable } from 'rxjs';
// import { HttpClient } from '@angular/common/http';
// import { Notification } from '../../interfaces/account/notification';
// import { Room } from 'src/interfaces/room';
// import { Complex } from 'src/interfaces/complex';
// import { Provider } from 'src/interfaces/account/provider';
// import { AccountService } from './account.service';

// @Injectable({
//   providedIn: 'root'
// })
// export class NotificationService {

//   constructor(private http: HttpClient, private account: AccountService) { }

//   // baseUrl: string = `http://localhost:11080/`;
//   baseUrl: string = environment.endpoints.account;

//   // Delete by Id - Coordinator only - DELETE - /api/notifications/{notificationId}
//   deleteNotificationById$(notificationId: string): Observable<any> {
//     return this.http.delete(`${this.baseUrl}api/notifications/${notificationId}`);
//   }

//   // Update at Id - Coordinator only - PUT - /api/notifications/{notificationId}
//   updateNotificationAtId$(notification: Notification): Observable<any> {
//     return this.http.put(`${this.baseUrl}api/notifications/${notification.notificationId}`, notification);
//   }

//   // Get all Notifications by Coordinator Id - Coordinator only - GET - /api/notifications/{coordinatorId}
//   getAllNotificationsByCoordinatorId$(coordinatorId: string): Observable<any> {
//     return this.http.get(`${this.baseUrl}api/notifications/${coordinatorId}`);
//   }

//   // Create a new notification for editing a room
//   sendEditRoomRequest(newCoordinatorId: string, newProviderId: string, room: Room): Observable<any> {
//     let notification: Notification = {
//       notificationId: '',
//       coordinatorId: newCoordinatorId,
//       providerId: newProviderId,
//       updateAction: {
//         updateActionId: '',
//         notificationId: '',
//         updateType: 'EditRoomRequest',
//         serializedTarget: JSON.stringify(room)
//       },
//       status: {
//         statusText: 'Pending'
//       },
//       createdAt: new Date()
//     };

//     return this.http.post(`${this.baseUrl}api/notifications/`, notification);
//   }

//   // Create a new notification for editing a complex
//   sendEditComplexRequest(newCoordinatorId: string, newProviderId: string, complex: Complex): Observable<any> {
//     let notification: Notification = {
//       notificationId: '',
//       coordinatorId: newCoordinatorId,
//       providerId: newProviderId,
//       updateAction: {
//         updateActionId: '',
//         notificationId: '',
//         updateType: "EditComplexRequest",
//         serializedTarget: JSON.stringify(complex)
//       },
//       status: {
//         statusText: 'Pending'
//       },
//       createdAt: new Date()
//     };

//     return this.http.post(`${this.baseUrl}api/notifications/`, notification);
//   }

//   // Create a new notification for getting approved as a provider
//   sendApproveProviderRequest(newCoordinatorId: string, provider: Provider): Observable<any> {
//     let notification: Notification = {
//       notificationId: '',
//       coordinatorId: newCoordinatorId,
//       providerId: provider.providerId,
//       updateAction: {
//         updateActionId: '',
//         notificationId: '',
//         updateType: "ApproveProviderRequest",
//         serializedTarget: JSON.stringify(provider)
//       },
//       status: {
//         statusText: 'Pending'
//       },
//       createdAt: new Date()
//     };

//     return this.http.post(`${this.baseUrl}api/notifications/`, notification);
//   }

//   approveNotificationAction(notification: Notification): Observable<any> {
//     switch (notification.updateAction.updateType) {
//       case "EditRoomRequest": {
//         let room: Room = JSON.parse(notification.updateAction.serializedTarget);
//         // call complex service's updateRoom
//         break;
//       }

//       case "EditComplexRequest": {
//         let complex: Complex = JSON.parse(notification.updateAction.serializedTarget);
//         // call complex service's updateComplex
//         break;
//       }

//       case "ApproveProviderRequest": {
//         let provider: Provider = JSON.parse(notification.updateAction.serializedTarget);
//         return this.account.approveProviderAtId$(provider);
//         break;
//       }
//     }
//   }
// }
