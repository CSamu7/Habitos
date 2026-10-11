import type {
  CreateRoutineRequest,
  RoutineData,
} from "../features/routine/types/routineTypes";
import { getErrorObject, type Result } from "../types/common";
import { betterFetch } from "../utils/api";

export const routineService = {
  async getAll(username: string): Promise<Result<Array<RoutineData>>> {
    const url = `${import.meta.env.VITE_USER_URL}/${username}/routines`;

    const request = await fetch(url, {
      credentials: "include",
    });

    const response: Result<Array<RoutineData>> = await request.json();

    return { data: response };
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
      const response = await request.json();
      const errorObject = getErrorObject(response, request.status);

      return { error: errorObject };
    }

    return { error: null, data: null };
  },

  async delete(username: string, idRoutine: number) {
    const url = `${import.meta.env.VITE_ROUTINE_URL}/${idRoutine}`;

    const response = await betterFetch<void>(url, {
      method: "DELETE",
      credentials: "include",
    });

    return response;
  },
};
