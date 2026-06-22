import type { ProblemResponse } from "./common";

export type GetUserResponse = ProblemResponse & {
  data: UserInformation;
};

export type UserInformation = {
  username: string;
  email: string;
  minGoal: number;
  cutOffTime: number;
  streak: number;
};
