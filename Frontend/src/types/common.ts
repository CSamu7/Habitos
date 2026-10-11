export type Success<T> = {
  data: T;
  error?: null;
};

export type Failure = {
  data?: null;
  error: ManyErrorsAPI | OneErrorAPI;
};

export type ResponseError = ManyErrorsAPI | OneErrorAPI;

export type ManyErrorsAPI = {
  title: string;
  status: number;
  errors: { string: string[] };
  kind: "ManyErrors";
};

export type OneErrorAPI = {
  status: number;
  detail: string;
  error: string;
  kind: "OneError";
};

export type Result<T> = Success<T> | Failure;

export const getErrorObject = (body: any, status: number): ResponseError => {
  if (body?.errors) {
    return {
      errors: body.errors,
      title: body?.title ?? "",
      status,
      kind: "ManyErrors",
    } as ManyErrorsAPI;
  }

  return {
    error: body?.error,
    detail: body?.detail ?? "",
    status,
    kind: "OneError",
  } as OneErrorAPI;
};

export const formatErrors = (response: Failure): Array<string> => {
  if (response.error.kind === "OneError") return [];

  if (response.error.kind === "ManyErrors" && response.error.errors) {
    return Object.entries(response.error.errors).reduce((acc, [, value]) => {
      return [...acc, ...value];
    }, [] as string[]);
  }

  return [];
};
