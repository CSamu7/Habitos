import type { DailyTaskResponse, HabitProgress } from "../types";
import HabitCard from "./HabitCard";
import styles from "./HabitColumn.module.css";

type HabitColumnProps = {
  progress: HabitProgress;
  routines: DailyTaskResponse[];
};

export default function HabitColumn({ progress, routines }: HabitColumnProps) {
  const total = routines.length;

  const mapping: Record<HabitProgress, string> = {
    Pending: "Pendiente",
    Completed: "Completo",
    Incomplete: "Incompleto",
  };

  return (
    <div className={styles.column}>
      <div className={styles.columnHeader}>
        <span
          className={`${styles.statusLabel} ${styles[progress.toString().toLowerCase()]}`}
        >
          {mapping[progress]}
        </span>
        <span className={styles.count}>{total} tareas</span>
      </div>

      <div className={styles.cards}>
        {routines.map((habit, idx) => (
          <HabitCard key={idx} {...habit} />
        ))}
      </div>
    </div>
  );
}
