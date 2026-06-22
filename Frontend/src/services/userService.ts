import { LOGIN_URL, USER_URL } from "../features/auth/api";
import type { Result } from "../types/common";
import type { LoginUserData } from "../types/LoginTypes";
import type {
  UserConfigurationForm,
  UserRegisterRequest,
} from "../types/RegisterTypes";
import type { GetUserResponse } from "../types/UserTypes";

export async function loginService(body: LoginUserData): Promise<Result<void>> {
  try {
    const request = await fetch(LOGIN_URL, {
      method: "POST",
      body: JSON.stringify(body),
      credentials: "include",
      headers: {
        "Content-type": "application/json",
      },
    });

    if (request.status === 401) {
      return {
        error: {
          detail: "Correo o contraseña incorrectos",
          status: request.status,
        },
      };
    }

    return { error: { status: 200, detail: "Ok" } };
  } catch (_) {
    return { error: { detail: "Ha habido un error", status: 500 } };
  }
}

export async function registerService(
  body: UserRegisterRequest,
): Promise<ProblemResponse> {
  try {
    const request = await fetch(`${USER_URL}/register`, {
      method: "POST",
      body: JSON.stringify(body),
      headers: {
        "Content-type": "application/json",
      },
    });

    if (request.status >= 400) {
      const response: ProblemResponse = await request.json();

      return response;
    }

    return { status: 201, detail: "Cuenta creada con exito" };
  } catch (error) {
    console.log(error);
    return { status: 500, detail: "Ha habido un error" };
  }
}

export async function getUserService(): Promise<Result<GetUserResponse>> {
  try {
    const request = await fetch(`${USER_URL}`, {
      method: "GET",
      credentials: "include",
    });

    const response: Result<GetUserResponse> = await request.json();

    if (response.error) return response;

    return { data: response };
  } catch (error) {
    return { error: { status: 500, detail: "Ha habido un error" } };
  }
}

export async function modifyUserService(
  body: UserConfigurationForm,
): Promise<ProblemResponse> {
  try {
    const request = await fetch(`${USER_URL}`, {
      method: "PATCH",
      credentials: "include",
      headers: {
        "Content-type": "application/json",
      },
      body: JSON.stringify(body),
    });

    const response: ProblemResponse = await request.json();

    if (request.status > 300) {
      return { status: response.status, detail: response.detail };
    }

    return response;
  } catch (error) {
    return { status: 500, detail: "Ha habido un error" } as GetUserResponse;
  }
}
