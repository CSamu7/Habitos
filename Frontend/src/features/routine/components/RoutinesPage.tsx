import { useRef } from "react";
import useRoutine from "../../../hooks/useRoutine";
import useUser from "../../../hooks/useUser";

import styles from "./RoutinesPage.module.css";
import CreateRoutineModal from "./CreateRoutineModal";
import AuthPageLayout from "../../../components/AuthPageLayout";
import RoutineRow from "./RoutineRow";

export default function RoutinesPage() {
  const { user } = useUser();
  const { routines } = useRoutine(user?.username ?? "");
  const addNewHabitModalRef = useRef<HTMLDialogElement>(null);

  const routinesJSX = routines.list.map((routine, index) => (
    <RoutineRow routine={routine} index={index} key={routine.id}></RoutineRow>
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

        <CreateRoutineModal
          username={user?.username ?? ""}
          ref={addNewHabitModalRef}
          routines={routines}
        />

        <div className={styles.tableWrapper}>
          <table className={styles.routineTable}>
            <thead>
              <tr className={styles.tableHeader}>
                <th scope="col" className={styles.colLabel}>
                  #
                </th>
                <th scope="col" className={styles.colLabel}>
                  Nombre
                </th>
                <th scope="col" className={styles.colLabel}>
                  Tiempo
                </th>
                <th scope="col" className={styles.colLabel}>
                  Categoría
                </th>
                <th scope="col" className={styles.colLabel}>
                  Últimos 7 días
                </th>
                <th scope="col" className={styles.colLabel}>
                  <span className={styles.visuallyHidden}>Acciones</span>
                </th>
              </tr>
            </thead>
            <tbody>{routinesJSX}</tbody>
          </table>
        </div>
      </div>
    </AuthPageLayout>
  );
}
