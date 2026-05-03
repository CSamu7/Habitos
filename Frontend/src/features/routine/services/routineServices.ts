import { DAILY_TASK_URL, USER_URL } from "../../auth/api";
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

export type ModifyTodayTaskBody = {
  idDailyTask: number;
  minutes: number;
  date: string;
};

export async function ModifyTodayTask({
  idDailyTask,
  minutes,
  date,
}: ModifyTodayTaskBody) {
  const request = await fetch(`${DAILY_TASK_URL}/${idDailyTask}`);
}
