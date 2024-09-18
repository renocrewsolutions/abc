import { StringFilter } from "../../util/StringFilter";
import { UserProfileWhereUniqueInput } from "../userProfile/UserProfileWhereUniqueInput";

export type RoleWhereInput = {
  id?: StringFilter;
  userProfile?: UserProfileWhereUniqueInput;
};
