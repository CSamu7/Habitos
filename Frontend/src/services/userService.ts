import type { ErrorAPI, Result } from "../types/common";
import type { LoginUserData } from "../types/LoginTypes";
import type {
  UserConfigurationForm,
  UserRegisterRequest,
} from "../features/register/types/RegisterTypes";
import type { GetUserResponse } from "../types/UserTypes";
import { betterFetch } from "../utils/api";

export async function loginService(body: LoginUserData): Promise<Result<null>> {
  const url = `${import.meta.env.VITE_LOGIN_URL}`;

  const request = await betterFetch<null>(url, {
    method: "POST",
    body: JSON.stringify(body),
    credentials: "include",
  });

  return request;
}

export async function registerService(
  body: UserRegisterRequest,
): Promise<Result<null>> {
  const url = `${import.meta.env.VITE_REGISTER_URL}`;

  const response = betterFetch<null>(url, {
    method: "POST",
    body: JSON.stringify(body),
  });

  return response;
}

export async function getUserService(): Promise<Result<GetUserResponse>> {
  const url = `${import.meta.env.VITE_USER_URL}`;

  const request = await fetch(url, {
    method: "GET",
    credentials: "include",
  });

  const response: Result<GetUserResponse> = await request.json();

  if (response.error) return response;

  return { data: response };
}

export async function modifyUserService(
  body: UserConfigurationForm,
): Promise<Result<object>> {
  const url = `${import.meta.env.VITE_USER_URL}`;

  const response = await betterFetch(url, {
    method: "PATCH",
    credentials: "include",
    body: JSON.stringify(body),
  });

  return { data: response };
}
