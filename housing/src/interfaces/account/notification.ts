import { Provider } from './provider';
import { NotificationKind } from 'rxjs/internal/Notification';
import { Status } from './status';

export interface Notification {
  notificationId: string;
  providerId: string;
  coordinatorId: string;
  updateActionId: string;
  status: Status;
}

// This check can see if account is usable by provider:
// trialCheck = (trial && date > createdDate + 7)
// extendedTrialCheck = (extendedTrial && date > expireDate + 30)
// if (!active && trialCheck || extendedTrialCheck)
//
// This means it will be up to the coordinator to delete inactive accounts
// We should use https://momentjs.com/ to make time handling easier
