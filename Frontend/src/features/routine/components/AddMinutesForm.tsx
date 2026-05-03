import { useEffect, useState } from "react";
import styles from "./AddMinutesForm.module.css";

type AddMinutesFormProps = {
  idDailyTask: number;
};

export default function AddMinutesForm({ idDailyTask }: AddMinutesFormProps) {
  const [s] = useState(() => new Date(Date.now()));
  const [minutes, setMinutes] = useState(0);

  const modifyTask = async () => {
    const request = modi;
  };

  return (
    <div className={styles.addMinutesForm}>
      <div>
        <label id="minutes">Minutos</label>
        <input
          type="text"
          name="minutes"
          id="minutes"
          value={minutes}
          onChange={(e) => setMinutes(Number(e.target.textContent))}
        />
      </div>
      <div>
        <label>Terminado el</label>
        <input
          type="date"
          name="date"
          id="date"
          onChange={(e) => new Date(e.target.textContent)}
          value={s.toLocaleDateString("sv-SE")}
        />
      </div>
      <input type="submit" value="Añadir" onSubmit={modifyTask} />
    </div>
  );
}
