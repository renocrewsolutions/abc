import { UserProfile } from "../userProfile/UserProfile";

export type Role = {
  createdAt: Date;
  id: string;
  updatedAt: Date;
  userProfile?: UserProfile | null;
};
