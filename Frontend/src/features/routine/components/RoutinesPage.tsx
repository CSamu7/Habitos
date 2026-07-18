import { useRef } from "react";
import PublicLayout from "../../../components/PublicLayout";
import useRoutine from "../../../hooks/useRoutine";
import useUser from "../../../hooks/useUser";

import styles from "./RoutinesPage.module.css";
import CreateRoutineModal from "./CreateRoutineModal";
import AuthPageLayout from "../../../components/AuthPageLayout";

export default function RoutinesPage() {
  const { user } = useUser();
  const { routines } = useRoutine(user?.username ?? "");
  const addNewHabitModalRef = useRef<HTMLDialogElement>(null);

  const routinesJSX = routines.list.map((routine, index) => (
    <li key={routine.id} className={styles.routineItem}>
      <p>{index + 1}</p>
      <strong className={styles.routineName}>{routine.name}</strong>
      <p className={styles.routineTime}>{routine.minutes} min</p>
      <p className={styles.routineCategory}>
        {routine.category ?? "Sin categoría"}
      </p>
      {/* <div className={styles.weekDots}>
        {routine.lastSevenDays.map((status, i) => (
          <span
            key={i}
            className={`${styles.dot} ${styles[`dot--${status}`]}`}
          />
        ))}
      </div> */}
      <div className={styles.actions}>
        <button className={styles.actionBtn}>Editar</button>
        <button className={styles.actionBtn}>Eliminar</button>
      </div>
    </li>
  ));

  return (
    <AuthPageLayout>
      <div className={styles.layout}>
        <div className={styles.header}>
          <h1 className={styles.title}>Tus tareas</h1>
          <button
            className={styles.addButton}
            onClick={() => addNewHabitModalRef.current?.showModal()}
          >
            +
          </button>
        </div>

        <div className={styles.tableHeader}>
          <span className={styles.colLabel}>Pos</span>
          <span className={styles.colLabel}>Nombre</span>
          <span className={styles.colLabel}>Tiempo</span>
          <span className={styles.colLabel}>Categoría</span>
          <span className={styles.colLabel}>Últimos 7 días</span>
          <span />
        </div>

        <CreateRoutineModal
          username={user?.username ?? ""}
          ref={addNewHabitModalRef}
          routines={routines}
        ></CreateRoutineModal>

        <ul className={styles.routineList}>{routinesJSX}</ul>
      </div>
    </AuthPageLayout>
  );
}
