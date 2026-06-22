import { useEffect, useState } from "react";
import type { DailyTasksData } from "../types/DailyTasksTypes";
import { dailyTasksService } from "../services/dailyTasksService";
import type { Result } from "../types/common";

export default function useDailyTasks({ username }: { username: string }) {
  const [dailyTasks, setDailyTasks] = useState<DailyTasksData>({
    list: [],
    count: 0,
    totalMinutes: 0,
    minutesCompleted: 0,
    minutesLeft: 0,
    percentageCompleted: "0.00%",
  });

  const [dailyTaskErrors, setDailyTaskErrors] = useState<Array<string>>([]);

  useEffect(() => {
    const getDailyTasks = async () => {
      const response: Result<DailyTasksData> =
        await dailyTasksService.getAll(username);

      if (response.error) {
        setDailyTaskErrors([response.error.detail]);
      } else {
        setDailyTasks(response.data);
      }
    };

    if (username) {
      getDailyTasks();
    }
  }, [username]);

  return { dailyTasks };
}
