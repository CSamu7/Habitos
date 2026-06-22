import type { DailyTasksData } from "../types/DailyTasksTypes";
import { DAILY_TASK_URL, USER_URL } from "../features/auth/api";
import type { Result } from "../types/common";

export const dailyTasksService = {
  async getAll(username: string): Promise<Result<DailyTasksData>> {
    try {
      const request = await fetch(
        `${USER_URL}/${username}/dailyRoutines/today`,
        {
          credentials: "include",
        },
      );

      const response: DailyTasksData = await request.json();

      return { data: response, error: null };
    } catch (error) {
      return { error: { detail: "Hubo un error", status: 500 } };
    }
  },
};

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
