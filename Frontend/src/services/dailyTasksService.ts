import type { DailyTasksData } from "../types/DailyTasksTypes";
import type { Result } from "../types/common";

export const dailyTasksService = {
  async getAll(username: string): Promise<Result<DailyTasksData>> {
    try {
      const url = `${import.meta.env.VITE_USER_URL}/${username}/dailyRoutines/today`;

      const request = await fetch(url, {
        credentials: "include",
      });

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
}: ModifyTodayTaskBody) {}
