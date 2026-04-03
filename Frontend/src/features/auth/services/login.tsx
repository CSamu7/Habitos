import { LOGIN_URL } from "../api";

export type LoginUserData = {
  username: string;
  password: string;
};

export type ResponseMessage = {
  message: string;
  code: number;
};

export async function authUser(body: LoginUserData): Promise<ResponseMessage> {
  const result = await fetch(LOGIN_URL, {
    method: "POST",
    body: JSON.stringify(body),
    headers: {
      "Content-type": "application/json",
    },
  });

  if (!result.ok) {
    return {
      message: "Nombre de usuario o contraseña incorrectos",
      code: result.status,
    };
  }

  return {
    message: "Login exitoso",
    code: result.status,
  };
}
