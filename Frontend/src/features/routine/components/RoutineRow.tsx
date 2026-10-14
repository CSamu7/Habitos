import { useRef } from "react";
import type { RoutineData } from "../types/routineTypes";
import CategoryLabel from "./CategoryLabel";
import styles from "./RoutineRow.module.css";
import DeleteRoutineModal from "./DeleteRoutineModal";

export default function RoutineRow({
  username,
  routine,
  index,
}: {
  routine: RoutineData;
  index: number;
  username: string;
}) {
  const categoriesJSX = routine.categories.map((category) => (
    <CategoryLabel
      category={category}
      className={styles.categoryLabel}
    ></CategoryLabel>
  ));

  const deleteModalRef = useRef<HTMLDialogElement>(null);

  return (
    <>
      <tr key={routine.id} className={styles.routineItem}>
        <td>{index + 1}</td>
        <th scope="row" className={styles.routineName}>
          {routine.name}
        </th>
        <td className={styles.routineTime}>{routine.minutes} min</td>
        <td className={styles.routineCategory}>
          {routine.categories.length >= 1 ? categoriesJSX : "-"}
        </td>
        <td>
          <div
            className={styles.weekDots}
            aria-label="Sin actividad registrada"
          >
            {Array.from({ length: 7 }, (_, day) => (
              <span key={day} className={styles.dot} aria-hidden="true" />
            ))}
          </div>
        </td>
        <td>
          <div className={styles.actions}>
            <button className={styles.actionBtn}>Editar</button>
            <button
              className={styles.actionBtn}
              onClick={() => deleteModalRef.current?.showModal()}
            >
              Eliminar
            </button>
            <DeleteRoutineModal
              username={username}
              routine={routine}
              ref={deleteModalRef}
            />
          </div>
        </td>
      </tr>
    </>
  );
}
