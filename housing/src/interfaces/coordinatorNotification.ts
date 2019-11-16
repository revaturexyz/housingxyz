import { Provider } from './provider';
import { TrainingCenter } from './trainingcenter';

export interface CoordinatorNotification {
  notificationId: string;
  providerId: number;
  centerId: number;
  centerDetails: TrainingCenter;
  createdDate: Date;
  active: boolean;
  trial: boolean;
  extendedTrial: boolean;
  providerDetails: Provider;
}

// This check can see if account is usable by provider:
// trialCheck = (trial && date > createdDate + 7)
// extendedTrialCheck = (extendedTrial && date > expireDate + 30)
// if (!active && trialCheck || extendedTrialCheck)
//
// This means it will be up to the coordinator to delete inactive accounts
// We should use https://momentjs.com/ to make time handling easier
