import { USER_URL } from "../../auth/api";
import type { ResponseMessage } from "../../auth/services/login";
import type { GetDailyTasksResponse } from "../types";

export async function getTodayTasks(
  username: string,
): Promise<GetDailyTasksResponse | ResponseMessage> {
  const request = await fetch(`${USER_URL}/${username}/dailyRoutines/today`, {
    credentials: "include",
  });

  if (!request.ok) return { message: request.statusText, code: request.status };

  const response = await request.json();

  return response;
}
