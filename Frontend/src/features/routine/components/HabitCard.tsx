import type { DailyTaskResponse } from "../types";
import AddMinutesForm from "./AddMinutesForm";
import styles from "./HabitCard.module.css";

type HabitCardDetails = {
  activeIndex: number;
  onActive: (key: number) => void;
  position: number;
};

type HabitCardProps = DailyTaskResponse & HabitCardDetails;

export default function HabitCard({
  idDailyRoutine,
  routine,
  minutesCompleted,
  totalMinutes,
  percentageCompleted,
  completedAt,
  onActive,
  position,
  activeIndex,
}: HabitCardProps) {
  return (
    <div className={`${styles.card}`} onClick={() => onActive(position)}>
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
      {activeIndex === position && (
        <AddMinutesForm idDailyTask={idDailyRoutine}></AddMinutesForm>
      )}
    </div>
  );
}
