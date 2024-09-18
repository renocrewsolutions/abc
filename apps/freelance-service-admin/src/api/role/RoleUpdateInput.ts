import { UserProfileWhereUniqueInput } from "../userProfile/UserProfileWhereUniqueInput";

export type RoleUpdateInput = {
  userProfile?: UserProfileWhereUniqueInput | null;
};
