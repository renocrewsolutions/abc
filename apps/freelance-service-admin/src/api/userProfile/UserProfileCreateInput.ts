import { InputJsonValue } from "../../types";
import { RoleCreateNestedManyWithoutUserProfilesInput } from "./RoleCreateNestedManyWithoutUserProfilesInput";

export type UserProfileCreateInput = {
  appMetadata?: InputJsonValue;
  aud?: string | null;
  audience?: string | null;
  confirmationDate?: Date | null;
  confirmedAt?: Date | null;
  email?: string | null;
  emailAddress?: string | null;
  emailConfirmedAt?: Date | null;
  identities?: InputJsonValue;
  isAnonymous?: boolean | null;
  lastSignInAt?: Date | null;
  phone?: string | null;
  phoneConfirmedAt?: Date | null;
  phoneNumber?: string | null;
  role?: string | null;
  roles?: RoleCreateNestedManyWithoutUserProfilesInput;
  supabaseId?: string | null;
  supabaseUserId?: string | null;
  userIdentities?: InputJsonValue;
  userMetadata?: InputJsonValue;
  userRole?: string | null;
};
