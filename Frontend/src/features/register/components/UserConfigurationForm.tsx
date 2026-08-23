import { useForm, type SubmitHandler } from "react-hook-form";
import { useLocation } from "wouter";
import type { UserConfigurationForm } from "../types/RegisterTypes";
import useUser from "../../../hooks/useUser";
import PublicLayout from "../../../components/PublicLayout";
import styles from "./UserConfigurationForm.module.css";
import FormInput from "../../../components/FormInput";
import SubmitButton from "../../../components/SubmitButton";

export default function UserConfigurationForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<UserConfigurationForm>();

  const [, setLocation] = useLocation();
  const { modifyUser, userErrors } = useUser();
  const timezones = Intl.supportedValuesOf("timeZone");

  const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;

  const timezonesHtml = timezones.map((timezone) => (
    <option value={timezone}>{timezone}</option>
  ));

  const submitForm: SubmitHandler<UserConfigurationForm> = async (data) => {
    await modifyUser(data);
    if (userErrors.length > 0) return;

    setLocation("/dashboard");
  };

  return (
    <PublicLayout>
      <main className={styles.main}>
        <form onSubmit={handleSubmit(submitForm)} className={styles.form}>
          <h2 className={styles.titleForm}>Configura tu cuenta</h2>
          <div className={styles.inputsContainer}>
            <FormInput
              label="Tu meta minima"
              pattern="[0-9]*"
              inputMode="numeric"
              type="text"
              textinformation="El porcentaje de minutos que debes cumplir"
              {...register("minGoal", {
                valueAsNumber: true,
                required: {
                  value: true,
                  message: "Debes ingresar una meta diaria",
                },
                max: { value: 100, message: "El valor maximo es 100%" },
                min: { value: 75, message: "El valor minimo es 75%" },
              })}
              error={errors.minGoal?.message}
            ></FormInput>
            <FormInput
              label="Hora de corte"
              textinformation="La hora hh:00 en que se generaran las nuevas tareas"
              pattern="[0-9]*"
              inputMode="numeric"
              type="text"
              {...register("cutOffTime", {
                valueAsNumber: true,
                required: {
                  value: true,
                  message: "Debes ingresar una hora de corte",
                },
                max: { value: 23, message: "El valor maximo es 23:00" },
                min: { value: 0, message: "El valor minimo es 00:00" },
              })}
              error={errors.cutOffTime?.message}
            ></FormInput>
            <div className={styles.selectContainer}>
              <label htmlFor="">Zona horaria</label>
              <select
                className={styles.select}
                {...register("timeZone", {
                  required: {
                    value: true,
                    message: "Debes ingresar tu zona horaria",
                  },
                })}
                value={timezone}
              >
                {timezonesHtml}
              </select>
            </div>
          </div>
          <SubmitButton
            text="Continuar"
            className={styles.inputSubmit}
          ></SubmitButton>
        </form>
      </main>
    </PublicLayout>
  );
}
