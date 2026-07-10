import { useEffect, useState } from "react";
import type {
  UserConfigurationForm,
  UserRegisterForm,
  UserRegisterRequest,
} from "../features/register/types/RegisterTypes";
import type { LoginUserData } from "../types/LoginTypes";
import {
  getUserService,
  registerService,
  loginService,
  modifyUserService,
} from "../services/userService";
import { formatErrors, type Result } from "../types/common";
import type { GetUserResponse, UserInformation } from "../types/UserTypes";

export default function useUser() {
  const [user, setUser] = useState<UserInformation>();
  const [userErrors, setUserErrors] = useState<Array<string>>([]);

  useEffect(() => {
    const getUser = async () => {
      const response: Result<GetUserResponse> = await getUserService();

      if (response.error) {
        setUserErrors([...response.error.detail]);
      } else {
        setUser(response.data);
      }
    };

    getUser();
  }, []);

  const loginUser = async (body: LoginUserData) => {
    const response = await loginService(body);

    return response;
  };

  const registerUser = async (
    data: UserRegisterForm,
  ): Promise<Result<void>> => {
    const body: UserRegisterRequest = {
      email: data.email,
      username: data.username,
      password: data.password,
    };

    const response = await registerService(body);

    return response;
  };

  const modifyUser = async (body: UserConfigurationForm) => {
    const response = await modifyUserService(body);

    if (response.errors) {
      const serverErrors = formatErrors(response);

      for (const error of serverErrors) {
        setUserErrors([...userErrors, error]);
      }

      return;
    }

    if (response.detail && response.status >= 400) {
      setUserErrors([response.detail]);
      return;
    }

    return response;
  };

  return { user, userErrors, registerUser, loginUser, modifyUser };
}
