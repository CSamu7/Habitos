import type { ErrorAPI, Result } from "../types/common";
import type { LoginUserData } from "../types/LoginTypes";
import type {
  UserConfigurationForm,
  UserRegisterRequest,
} from "../features/register/types/RegisterTypes";
import type { GetUserResponse } from "../types/UserTypes";
//TODO: No me gusta usar object aqui. Debería buscar un tipo que especifique el estatus y mensaje de exito.
export async function loginService(
  body: LoginUserData,
): Promise<Result<object>> {
  const url = `${import.meta.env.VITE_LOGIN_URL}`;

  const request = await fetch(url, {
    method: "POST",
    body: JSON.stringify(body),
    credentials: "include",
    headers: {
      "Content-type": "application/json",
    },
  });

  if (!request.ok) {
    const response: ErrorAPI = await request.json();

    return { error: response };
  }

  return { data: {} };
}

export async function registerService(
  body: UserRegisterRequest,
): Promise<Result<object>> {
  const url = `${import.meta.env.VITE_REGISTER_URL}`;

  const request = await fetch(url, {
    method: "POST",
    body: JSON.stringify(body),
    headers: {
      "Content-type": "application/json",
    },
  });

  if (!request.ok) {
    const response: ErrorAPI = await request.json();

    return { error: response };
  }

  return { data: {} };
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

  const request = await fetch(url, {
    method: "PATCH",
    credentials: "include",
    headers: {
      "Content-type": "application/json",
    },
    body: JSON.stringify(body),
  });

  if (!request.ok) {
    const response: ErrorAPI = await request.json();

    return { error: response };
  }

  return { data: {} };
}
