import { Status } from './status';
import { UpdateAction } from './update-action';

export interface Notification {
  notificationId: string;
  providerId: string;
  coordinatorId: string;
  updateAction: UpdateAction;
  status: Status;
  createdAt: Date;
}
