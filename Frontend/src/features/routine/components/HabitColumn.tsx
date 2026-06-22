import { useState } from "react";
import HabitCard from "./HabitCard";
import styles from "./HabitColumn.module.css";
import {
  type DailyTaskStatus,
  type DailyTaskInformation,
} from "../../../types/DailyTasksTypes";

type HabitColumnProps = {
  progress: DailyTaskStatus;
  routines: DailyTaskInformation[];
};

export default function HabitColumn({ progress, routines }: HabitColumnProps) {
  const total = routines.length;
  const [activeIndex, setActiveIndex] = useState(-1);

  const mapping: Record<DailyTaskStatus, string> = {
    Pending: "Pendiente",
    Completed: "Completo",
    Incomplete: "Incompleto",
  };

  const onActive = (key: number) => {
    setActiveIndex(key);
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
          <HabitCard
            key={idx}
            onActive={onActive}
            position={idx}
            {...habit}
            activeIndex={activeIndex}
          />
        ))}
      </div>
    </div>
  );
}
