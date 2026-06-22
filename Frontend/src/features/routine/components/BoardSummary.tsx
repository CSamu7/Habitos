import type { DailyTasksData } from "../../../types/DailyTasksTypes";
import styles from "./BoardSummary.module.css";

export default function BoardSummary({
  dailyTasks,
  completedTasks,
}: {
  dailyTasks: DailyTasksData;
  completedTasks: number;
}) {
  return (
    <div className={styles.summary}>
      <span className={styles.summaryText}>
        <strong>{completedTasks}</strong> de{" "}
        <strong>{dailyTasks.count} </strong> tareas completadas
      </span>
      <div className={styles.summaryTime}>
        <span>
          {dailyTasks.minutesCompleted} / {dailyTasks.totalMinutes}
          min
        </span>
      </div>
    </div>
  );
}
