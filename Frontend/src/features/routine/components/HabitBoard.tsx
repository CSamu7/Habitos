import HabitColumn from "./HabitColumn";
import styles from "./HabitBoard.module.css";
import BoardSummary from "./BoardSummary";
import useDailyTasks from "../../../hooks/useDailyTasks";
import type { DailyTaskInformation } from "../../../types/DailyTasksTypes";

export default function HabitBoard({ username }: { username: string }) {
  const { dailyTasks } = useDailyTasks({ username });

  const pending: Array<DailyTaskInformation> =
    dailyTasks?.list.filter(
      (dailyTask) =>
        dailyTask.status == "Pending" || dailyTask.status == "InProgress",
    ) ?? [];

  const incomplete: Array<DailyTaskInformation> =
    dailyTasks?.list.filter((dailyTask) => dailyTask.status == "Incomplete") ??
    [];

  const completed: Array<DailyTaskInformation> =
    dailyTasks?.list.filter((dailyTask) => dailyTask.status == "Completed") ??
    [];

  return (
    <div className={styles.board}>
      <BoardSummary
        dailyTasks={dailyTasks}
        completedTasks={completed.length ?? 0}
      ></BoardSummary>
      <div className={styles.columns}>
        <HabitColumn progress="Pending" routines={pending} />
        <HabitColumn progress="Incomplete" routines={incomplete} />
        <HabitColumn progress="Completed" routines={completed} />
      </div>
    </div>
  );
}
