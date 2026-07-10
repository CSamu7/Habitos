export type UserRegisterRequest = {
  email: string;
  password: string;
  username: string;
};

export type UserRegisterForm = {
  email: string;
  password: string;
  username: string;
  repeatedPassword: string;
};

export type UserConfigurationForm = {
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
};

export type UserConfigurationRequest = {
  timeZone: string;
  minGoal: number;
  cutOffTime: number;
};
