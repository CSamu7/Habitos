import type { UseFormRegister, UseFormWatch } from "react-hook-form";
import FormInput from "../../../components/FormInput";
import { ROUTINE_MAX_MINUTES } from "../../../utils/constants";
import type { CreateRoutineModalInputs } from "../types/routineTypes";
import styles from "./FormTimeInput.module.css";

export default function FormTimeInput({
  totalMinutes,
  register,
  watch,
}: {
  totalMinutes: number;
  register: UseFormRegister<CreateRoutineModalInputs>;
  watch: UseFormWatch<CreateRoutineModalInputs>;
}) {
  const remainingHours = Math.floor((ROUTINE_MAX_MINUTES - totalMinutes) / 60);

  const formHours = Number(watch("hours"));
  const remainingMinutes = Math.round((480 - totalMinutes) % 60);

  return (
    <div className={styles.formTimeSection}>
      <label htmlFor="">Tiempo</label>
      <div className={styles.formTimeInputs}>
        <FormInput
          label=""
          pattern="[0-9]*"
          inputMode="numeric"
          type="text"
          {...register("hours", {
            required: {
              value: true,
              message: "Debes ingresar una hora",
            },
            min: {
              value: 0,
              message: `Tus horas deben estar entre 0 y ${remainingHours} horas`,
            },
            max: {
              value: remainingHours,
              message: `Tus horas deben estar entre 0 y ${remainingHours} horas`,
            },
          })}
          defaultValue={0}
        ></FormInput>
        <p className={styles.timeDivision}>:</p>
        <FormInput
          label=""
          pattern="[0-9]*"
          inputMode="numeric"
          type="text"
          {...register("minutes", {
            required: {
              value: true,
              message: "Debes ingresar los minutos",
            },
            min: {
              value: 0,
              message: `Tus minutos deben estar entre 0 y ${formHours === remainingHours ? remainingMinutes : 60} minutos`,
            },
            max: {
              value: formHours === remainingHours ? remainingMinutes : 60,
              message: `Te quedan ${formHours === remainingHours ? remainingMinutes : 60} minutos. Elimina una tarea o reduce la cantidad de tiempo de tu tarea`,
            },
          })}
          defaultValue={0}
        ></FormInput>
      </div>
    </div>
  );
}
