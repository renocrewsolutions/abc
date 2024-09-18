import { JsonValue } from "type-fest";
import { Role } from "../role/Role";

export type UserProfile = {
  appMetadata: JsonValue;
  aud: string | null;
  audience: string | null;
  confirmationDate: Date | null;
  confirmedAt: Date | null;
  createdAt: Date;
  email: string | null;
  emailAddress: string | null;
  emailConfirmedAt: Date | null;
  id: string;
  identities: JsonValue;
  isAnonymous: boolean | null;
  lastSignInAt: Date | null;
  phone: string | null;
  phoneConfirmedAt: Date | null;
  phoneNumber: string | null;
  role: string | null;
  roles?: Array<Role>;
  supabaseId: string | null;
  supabaseUserId: string | null;
  updatedAt: Date;
  userIdentities: JsonValue;
  userMetadata: JsonValue;
  userRole: string | null;
};
