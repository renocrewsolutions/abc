import { UserProfileWhereUniqueInput } from "../userProfile/UserProfileWhereUniqueInput";

export type RoleCreateInput = {
  userProfile?: UserProfileWhereUniqueInput | null;
};
