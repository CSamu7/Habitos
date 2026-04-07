import type { DailyTaskResponse } from "../types";
import styles from "./HabitCard.module.css";

export default function HabitCard({
  idDailyRoutine,
  routine,
  minutesCompleted,
  totalMinutes,
  percentageCompleted,
  completedAt,
}: DailyTaskResponse) {
  return (
    <div
      className={`${styles.card}`}
      onClick={() => console.log("CLICK CLICK")}
    >
      <div className={styles.header}>
        <span className={styles.name}>{routine.name}</span>
      </div>
      <div className={styles.footer}>
        <div className={styles.progressBar}>
          <div className={styles.progressFill} />
        </div>
        <div className={styles.taskInfo}>
          <span>
            {minutesCompleted} / {totalMinutes} min
          </span>
          <span>{percentageCompleted}</span>
        </div>
      </div>
    </div>
  );
}
