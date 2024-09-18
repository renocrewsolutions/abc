import * as React from "react";

import {
  Show,
  SimpleShowLayout,
  ShowProps,
  TextField,
  DateField,
  BooleanField,
  ReferenceManyField,
  Datagrid,
  ReferenceField,
} from "react-admin";

import { USERPROFILE_TITLE_FIELD } from "./UserProfileTitle";

export const UserProfileShow = (props: ShowProps): React.ReactElement => {
  return (
    <Show {...props}>
      <SimpleShowLayout>
        <TextField label="app_metadata" source="appMetadata" />
        <TextField label="aud" source="aud" />
        <TextField label="audience" source="audience" />
        <TextField label="confirmationDate" source="confirmationDate" />
        <TextField label="confirmed_at" source="confirmedAt" />
        <DateField source="createdAt" label="Created At" />
        <TextField label="email" source="email" />
        <TextField label="emailAddress" source="emailAddress" />
        <TextField label="email_confirmed_at" source="emailConfirmedAt" />
        <TextField label="ID" source="id" />
        <TextField label="identities" source="identities" />
        <BooleanField label="is_anonymous" source="isAnonymous" />
        <TextField label="last_sign_in_at" source="lastSignInAt" />
        <TextField label="phone" source="phone" />
        <TextField label="phone_confirmed_at" source="phoneConfirmedAt" />
        <TextField label="phoneNumber" source="phoneNumber" />
        <TextField label="role" source="role" />
        <TextField label="supabaseId" source="supabaseId" />
        <TextField label="supabase_user_id" source="supabaseUserId" />
        <DateField source="updatedAt" label="Updated At" />
        <TextField label="userIdentities" source="userIdentities" />
        <TextField label="user_metadata" source="userMetadata" />
        <TextField label="userRole" source="userRole" />
        <ReferenceManyField
          reference="Role"
          target="userProfileId"
          label="Roles"
        >
          <Datagrid rowClick="show" bulkActionButtons={false}>
            <DateField source="createdAt" label="Created At" />
            <TextField label="ID" source="id" />
            <DateField source="updatedAt" label="Updated At" />
            <ReferenceField
              label="UserProfile"
              source="userprofile.id"
              reference="UserProfile"
            >
              <TextField source={USERPROFILE_TITLE_FIELD} />
            </ReferenceField>
          </Datagrid>
        </ReferenceManyField>
      </SimpleShowLayout>
    </Show>
  );
};
