import { Status } from './status';

export interface Provider {
  providerId;
  coordinatorId: string;
  name: string;
  email: string;
  status: Status;
  accountCreatedAt: Date;
  accountExpiresAt: Date;
}
