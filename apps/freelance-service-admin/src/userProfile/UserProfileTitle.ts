import { UserProfile as TUserProfile } from "../api/userProfile/UserProfile";

export const USERPROFILE_TITLE_FIELD = "aud";

export const UserProfileTitle = (record: TUserProfile): string => {
  return record.aud?.toString() || String(record.id);
};
