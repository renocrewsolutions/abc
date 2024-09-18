import * as React from "react";

import {
  Edit,
  SimpleForm,
  EditProps,
  TextInput,
  DateTimeInput,
  BooleanInput,
  ReferenceArrayInput,
  SelectArrayInput,
} from "react-admin";

import { RoleTitle } from "../role/RoleTitle";

export const UserProfileEdit = (props: EditProps): React.ReactElement => {
  return (
    <Edit {...props}>
      <SimpleForm>
        <div />
        <TextInput label="aud" source="aud" />
        <TextInput label="audience" source="audience" />
        <DateTimeInput label="confirmationDate" source="confirmationDate" />
        <DateTimeInput label="confirmed_at" source="confirmedAt" />
        <TextInput label="email" source="email" type="email" />
        <TextInput label="emailAddress" source="emailAddress" type="email" />
        <DateTimeInput label="email_confirmed_at" source="emailConfirmedAt" />
        <div />
        <BooleanInput label="is_anonymous" source="isAnonymous" />
        <DateTimeInput label="last_sign_in_at" source="lastSignInAt" />
        <TextInput label="phone" source="phone" />
        <DateTimeInput label="phone_confirmed_at" source="phoneConfirmedAt" />
        <TextInput label="phoneNumber" source="phoneNumber" />
        <TextInput label="role" source="role" />
        <ReferenceArrayInput source="roles" reference="Role">
          <SelectArrayInput
            optionText={RoleTitle}
            parse={(value: any) => value && value.map((v: any) => ({ id: v }))}
            format={(value: any) => value && value.map((v: any) => v.id)}
          />
        </ReferenceArrayInput>
        <TextInput label="supabaseId" source="supabaseId" />
        <TextInput label="supabase_user_id" source="supabaseUserId" />
        <div />
        <div />
        <TextInput label="userRole" source="userRole" />
      </SimpleForm>
    </Edit>
  );
};
