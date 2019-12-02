import { Provider } from './provider';
import { NotificationKind } from 'rxjs/internal/Notification';
import { Status } from './status';
import { UpdateAction } from './updateAction';

export interface Notification {
  notificationId: string;
  providerId: string;
  coordinatorId: string;
  updateAction: UpdateAction;
  status: Status;
  createdAt: Date;
}
