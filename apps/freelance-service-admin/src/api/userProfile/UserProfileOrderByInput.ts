import { SortOrder } from "../../util/SortOrder";

export type UserProfileOrderByInput = {
  appMetadata?: SortOrder;
  aud?: SortOrder;
  audience?: SortOrder;
  confirmationDate?: SortOrder;
  confirmedAt?: SortOrder;
  createdAt?: SortOrder;
  email?: SortOrder;
  emailAddress?: SortOrder;
  emailConfirmedAt?: SortOrder;
  id?: SortOrder;
  identities?: SortOrder;
  isAnonymous?: SortOrder;
  lastSignInAt?: SortOrder;
  phone?: SortOrder;
  phoneConfirmedAt?: SortOrder;
  phoneNumber?: SortOrder;
  role?: SortOrder;
  supabaseId?: SortOrder;
  supabaseUserId?: SortOrder;
  updatedAt?: SortOrder;
  userIdentities?: SortOrder;
  userMetadata?: SortOrder;
  userRole?: SortOrder;
};
