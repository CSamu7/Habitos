export type Errors = {
  type: string;
  list: string[];
};

export type ErrorAPI = {
  detail: string;
  status: number;
  errors?: Errors;
};

export type Success<T> = {
  data: T;
  error?: null;
};

export type Failure = {
  data?: null;
  error: ErrorAPI;
};

export type Result<T> = Success<T> | Failure;

export const formatErrors = (response: Failure): Array<string> => {
  if (response.error.errors) {
    return Object.entries(response.error.errors).reduce((acc, [, value]) => {
      return [...acc, ...value];
    }, [] as string[]);
  }

  return [];
};
