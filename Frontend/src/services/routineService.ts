import type {
  CreateRoutineRequest,
  RoutineData,
} from "../features/routine/types/routineTypes";
import type { ErrorAPI, Result } from "../types/common";

export const routineService = {
  async getAll(username: string): Promise<Result<Array<RoutineData>>> {
    const url = `${import.meta.env.VITE_USER_URL}/${username}/routines`;

    try {
      const request = await fetch(url, {
        credentials: "include",
      });

      const response: Result<Array<RoutineData>> = await request.json();

      return { data: response };
    } catch (error) {
      return { error: { detail: "Hubo un error en el servidor", status: 500 } };
    }
  },

  async post(
    username: string,
    form: CreateRoutineRequest,
  ): Promise<Result<null>> {
    const url = `${import.meta.env.VITE_USER_URL}/${username}/routines`;

    const request = await fetch(url, {
      method: "POST",
      credentials: "include",
      body: JSON.stringify(form),
      headers: {
        "Content-type": "application/json",
      },
    });

    if (!request.ok) {
      const response: ErrorAPI = await request.json();

      return { error: response };
    }

    const response: Result<null> = await request.json();

    return response;
  },
};
