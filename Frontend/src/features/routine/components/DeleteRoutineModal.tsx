import useRoutine from "../../../hooks/useRoutine";
import type { RoutineData } from "../types/routineTypes";
import styles from "./DeleteRoutineModal.module.css";

interface DeleteRoutineModalProps {
  username: string;
  routine: RoutineData;
  ref: React.RefObject<HTMLDialogElement | null>;
}

export default function DeleteRoutineModal(props: DeleteRoutineModalProps) {
  const { routine, ref, username } = props;
  const { deleteRoutine } = useRoutine(username);

  const onDeleteRoutine = async () => {
    try {
      const request = await deleteRoutine(routine.id);

      if (request.error) {
        console.log(request.error);
      }

      //tmp solution
      window.location.reload();
    } catch (error) {}
  };

  return (
    <dialog ref={ref} className={styles.deleteRoutineModal} closedby="any">
      <header className={styles.header}>
        <h2 className={styles.title}>¿Eliminar hábito?</h2>
      </header>
      <div className={styles.content}>
        <p className={styles.message}>El hábito {routine.name} se eliminará</p>
        <div className={styles.actions}>
          <button
            className={styles.confirmButton}
            onClick={() => onDeleteRoutine()}
          >
            Confirmar
          </button>
        </div>
      </div>
    </dialog>
  );
}
