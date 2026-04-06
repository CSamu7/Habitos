import type { HabitInfo } from "../types";
import styles from "./HabitCard.module.css";

export default function HabitCard({
  name,
  description,
  minutesDone,
  minutesGoal,
  progress,
}: HabitInfo) {
  const percentage = minutesGoal > 0 ? (minutesDone / minutesGoal) * 100 : 0;

  return (
    <div className={`${styles.card} ${styles[progress]}`}>
      <div className={styles.header}>
        <span className={styles.name}>{name}</span>
      </div>
      {description && <p className={styles.description}>{description}</p>}
      <div className={styles.footer}>
        <div className={styles.progressBar}>
          <div
            className={styles.progressFill}
            style={{ width: `${Math.min(percentage, 100)}%` }}
          />
        </div>
        <div className={styles.time}>
          <span>
            {minutesDone} / {minutesGoal} min
          </span>
        </div>
      </div>
    </div>
  );
}
