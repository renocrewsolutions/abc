import { Test } from "@nestjs/testing";
import {
  INestApplication,
  HttpStatus,
  ExecutionContext,
  CallHandler,
} from "@nestjs/common";
import request from "supertest";
import { ACGuard } from "nest-access-control";
import { DefaultAuthGuard } from "../../auth/defaultAuth.guard";
import { ACLModule } from "../../auth/acl.module";
import { AclFilterResponseInterceptor } from "../../interceptors/aclFilterResponse.interceptor";
import { AclValidateRequestInterceptor } from "../../interceptors/aclValidateRequest.interceptor";
import { map } from "rxjs";
import { UserProfileController } from "../userProfile.controller";
import { UserProfileService } from "../userProfile.service";

const nonExistingId = "nonExistingId";
const existingId = "existingId";
const CREATE_INPUT = {
  aud: "exampleAud",
  audience: "exampleAudience",
  confirmationDate: new Date(),
  confirmedAt: new Date(),
  createdAt: new Date(),
  email: "exampleEmail",
  emailAddress: "exampleEmailAddress",
  emailConfirmedAt: new Date(),
  id: "exampleId",
  isAnonymous: "true",
  lastSignInAt: new Date(),
  phone: "examplePhone",
  phoneConfirmedAt: new Date(),
  phoneNumber: "examplePhoneNumber",
  role: "exampleRole",
  supabaseId: "exampleSupabaseId",
  supabaseUserId: "exampleSupabaseUserId",
  updatedAt: new Date(),
  userRole: "exampleUserRole",
};
const CREATE_RESULT = {
  aud: "exampleAud",
  audience: "exampleAudience",
  confirmationDate: new Date(),
  confirmedAt: new Date(),
  createdAt: new Date(),
  email: "exampleEmail",
  emailAddress: "exampleEmailAddress",
  emailConfirmedAt: new Date(),
  id: "exampleId",
  isAnonymous: "true",
  lastSignInAt: new Date(),
  phone: "examplePhone",
  phoneConfirmedAt: new Date(),
  phoneNumber: "examplePhoneNumber",
  role: "exampleRole",
  supabaseId: "exampleSupabaseId",
  supabaseUserId: "exampleSupabaseUserId",
  updatedAt: new Date(),
  userRole: "exampleUserRole",
};
const FIND_MANY_RESULT = [
  {
    aud: "exampleAud",
    audience: "exampleAudience",
    confirmationDate: new Date(),
    confirmedAt: new Date(),
    createdAt: new Date(),
    email: "exampleEmail",
    emailAddress: "exampleEmailAddress",
    emailConfirmedAt: new Date(),
    id: "exampleId",
    isAnonymous: "true",
    lastSignInAt: new Date(),
    phone: "examplePhone",
    phoneConfirmedAt: new Date(),
    phoneNumber: "examplePhoneNumber",
    role: "exampleRole",
    supabaseId: "exampleSupabaseId",
    supabaseUserId: "exampleSupabaseUserId",
    updatedAt: new Date(),
    userRole: "exampleUserRole",
  },
];
const FIND_ONE_RESULT = {
  aud: "exampleAud",
  audience: "exampleAudience",
  confirmationDate: new Date(),
  confirmedAt: new Date(),
  createdAt: new Date(),
  email: "exampleEmail",
  emailAddress: "exampleEmailAddress",
  emailConfirmedAt: new Date(),
  id: "exampleId",
  isAnonymous: "true",
  lastSignInAt: new Date(),
  phone: "examplePhone",
  phoneConfirmedAt: new Date(),
  phoneNumber: "examplePhoneNumber",
  role: "exampleRole",
  supabaseId: "exampleSupabaseId",
  supabaseUserId: "exampleSupabaseUserId",
  updatedAt: new Date(),
  userRole: "exampleUserRole",
};

const service = {
  createUserProfile() {
    return CREATE_RESULT;
  },
  userProfiles: () => FIND_MANY_RESULT,
  userProfile: ({ where }: { where: { id: string } }) => {
    switch (where.id) {
      case existingId:
        return FIND_ONE_RESULT;
      case nonExistingId:
        return null;
    }
  },
};

const basicAuthGuard = {
  canActivate: (context: ExecutionContext) => {
    const argumentHost = context.switchToHttp();
    const request = argumentHost.getRequest();
    request.user = {
      roles: ["user"],
    };
    return true;
  },
};

const acGuard = {
  canActivate: () => {
    return true;
  },
};

const aclFilterResponseInterceptor = {
  intercept: (context: ExecutionContext, next: CallHandler) => {
    return next.handle().pipe(
      map((data) => {
        return data;
      })
    );
  },
};
const aclValidateRequestInterceptor = {
  intercept: (context: ExecutionContext, next: CallHandler) => {
    return next.handle();
  },
};

describe("UserProfile", () => {
  let app: INestApplication;

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      providers: [
        {
          provide: UserProfileService,
          useValue: service,
        },
      ],
      controllers: [UserProfileController],
      imports: [ACLModule],
    })
      .overrideGuard(DefaultAuthGuard)
      .useValue(basicAuthGuard)
      .overrideGuard(ACGuard)
      .useValue(acGuard)
      .overrideInterceptor(AclFilterResponseInterceptor)
      .useValue(aclFilterResponseInterceptor)
      .overrideInterceptor(AclValidateRequestInterceptor)
      .useValue(aclValidateRequestInterceptor)
      .compile();

    app = moduleRef.createNestApplication();
    await app.init();
  });

  test("POST /userProfiles", async () => {
    await request(app.getHttpServer())
      .post("/userProfiles")
      .send(CREATE_INPUT)
      .expect(HttpStatus.CREATED)
      .expect({
        ...CREATE_RESULT,
        confirmationDate: CREATE_RESULT.confirmationDate.toISOString(),
        confirmedAt: CREATE_RESULT.confirmedAt.toISOString(),
        createdAt: CREATE_RESULT.createdAt.toISOString(),
        emailConfirmedAt: CREATE_RESULT.emailConfirmedAt.toISOString(),
        lastSignInAt: CREATE_RESULT.lastSignInAt.toISOString(),
        phoneConfirmedAt: CREATE_RESULT.phoneConfirmedAt.toISOString(),
        updatedAt: CREATE_RESULT.updatedAt.toISOString(),
      });
  });

  test("GET /userProfiles", async () => {
    await request(app.getHttpServer())
      .get("/userProfiles")
      .expect(HttpStatus.OK)
      .expect([
        {
          ...FIND_MANY_RESULT[0],
          confirmationDate: FIND_MANY_RESULT[0].confirmationDate.toISOString(),
          confirmedAt: FIND_MANY_RESULT[0].confirmedAt.toISOString(),
          createdAt: FIND_MANY_RESULT[0].createdAt.toISOString(),
          emailConfirmedAt: FIND_MANY_RESULT[0].emailConfirmedAt.toISOString(),
          lastSignInAt: FIND_MANY_RESULT[0].lastSignInAt.toISOString(),
          phoneConfirmedAt: FIND_MANY_RESULT[0].phoneConfirmedAt.toISOString(),
          updatedAt: FIND_MANY_RESULT[0].updatedAt.toISOString(),
        },
      ]);
  });

  test("GET /userProfiles/:id non existing", async () => {
    await request(app.getHttpServer())
      .get(`${"/userProfiles"}/${nonExistingId}`)
      .expect(HttpStatus.NOT_FOUND)
      .expect({
        statusCode: HttpStatus.NOT_FOUND,
        message: `No resource was found for {"${"id"}":"${nonExistingId}"}`,
        error: "Not Found",
      });
  });

  test("GET /userProfiles/:id existing", async () => {
    await request(app.getHttpServer())
      .get(`${"/userProfiles"}/${existingId}`)
      .expect(HttpStatus.OK)
      .expect({
        ...FIND_ONE_RESULT,
        confirmationDate: FIND_ONE_RESULT.confirmationDate.toISOString(),
        confirmedAt: FIND_ONE_RESULT.confirmedAt.toISOString(),
        createdAt: FIND_ONE_RESULT.createdAt.toISOString(),
        emailConfirmedAt: FIND_ONE_RESULT.emailConfirmedAt.toISOString(),
        lastSignInAt: FIND_ONE_RESULT.lastSignInAt.toISOString(),
        phoneConfirmedAt: FIND_ONE_RESULT.phoneConfirmedAt.toISOString(),
        updatedAt: FIND_ONE_RESULT.updatedAt.toISOString(),
      });
  });

  test("POST /userProfiles existing resource", async () => {
    const agent = request(app.getHttpServer());
    await agent
      .post("/userProfiles")
      .send(CREATE_INPUT)
      .expect(HttpStatus.CREATED)
      .expect({
        ...CREATE_RESULT,
        confirmationDate: CREATE_RESULT.confirmationDate.toISOString(),
        confirmedAt: CREATE_RESULT.confirmedAt.toISOString(),
        createdAt: CREATE_RESULT.createdAt.toISOString(),
        emailConfirmedAt: CREATE_RESULT.emailConfirmedAt.toISOString(),
        lastSignInAt: CREATE_RESULT.lastSignInAt.toISOString(),
        phoneConfirmedAt: CREATE_RESULT.phoneConfirmedAt.toISOString(),
        updatedAt: CREATE_RESULT.updatedAt.toISOString(),
      })
      .then(function () {
        agent
          .post("/userProfiles")
          .send(CREATE_INPUT)
          .expect(HttpStatus.CONFLICT)
          .expect({
            statusCode: HttpStatus.CONFLICT,
          });
      });
  });

  afterAll(async () => {
    await app.close();
  });
});
