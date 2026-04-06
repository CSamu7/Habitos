import HabitColumn from "./HabitColumn";
import styles from "./HabitBoard.module.css";
import type { GetDailyTasksResponse } from "../types";
import { useEffect, useState } from "react";
import { getAllRoutines } from "../services/routineServices";
import BoardSummary from "./BoardSummary";

export default function HabitBoard({ username }: { username: string }) {
  const [dailyTasks, setDailyTasks] = useState<GetDailyTasksResponse>({
    count: 0,
    minutesCompleted: 0,
    minutesLeft: 0,
    totalMinutes: 0,
    results: [],
    percentageCompleted: "00.00%",
  });

  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const getRoutines = async () => {
      setError(null);
      const routines = await getAllRoutines(username);

      if ("message" in routines) {
        setError(routines.message);
      } else {
        setDailyTasks(routines as GetDailyTasksResponse);
      }
    };

    getRoutines();
  }, [username]);

  const pending =
    dailyTasks?.results.filter((t) => t.minutesCompleted === 0) ?? [];

  const incomplete =
    dailyTasks?.results.filter(
      (t) => t.minutesCompleted > 0 && t.minutesCompleted < t.totalMinutes,
    ) ?? [];

  const completed =
    dailyTasks?.results.filter((t) => t.minutesCompleted >= t.totalMinutes) ??
    [];

  return (
    <div className={styles.board}>
      <BoardSummary
        dailyTasks={dailyTasks as GetDailyTasksResponse}
        completedTasks={completed.length}
      ></BoardSummary>
      <div className={styles.columns}>
        <HabitColumn progress="Pending" routines={pending} />
        <HabitColumn progress="Incomplete" routines={incomplete} />
        <HabitColumn progress="Completed" routines={completed} />
      </div>
    </div>
  );
}
