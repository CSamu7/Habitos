import type { DailyTaskResponse, HabitProgress } from "../types";
import HabitCard from "./HabitCard";
import styles from "./HabitColumn.module.css";

type HabitColumnProps = {
  progress: HabitProgress;
  routines: DailyTaskResponse[];
};

export default function HabitColumn({ progress, routines }: HabitColumnProps) {
  const total = routines.length;
  const label = progress;

  return (
    <div className={styles.column}>
      <div className={styles.columnHeader}>
        <span
          className={`${styles.statusLabel} ${styles[progress.toLowerCase()]}`}
        >
          {label}
        </span>
        <span className={styles.count}>{total} tareas</span>
      </div>

      <div className={styles.cards}>
        {routines.map((habit, idx) => (
          <HabitCard key={idx} {...habit} progress={progress} />
        ))}
      </div>
    </div>
  );
}
