import styles from "./ErrorModal.module.css";

export default function ErrorModal({
  title,
  message,
}: {
  title: string;
  message?: string | null;
}) {
  return (
    <dialog className={styles.modal} open={message != null} closedby="any">
      <div className={styles.header}>
        <h2>{title}</h2>
      </div>
      <p className={styles.errorText}>{message}</p>
    </dialog>
  );
}
