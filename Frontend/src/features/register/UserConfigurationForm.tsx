import { useForm, type SubmitHandler } from "react-hook-form";
import type { UserConfigurationForm } from "../../types/RegisterTypes";
import PublicLayout from "../../components/PublicLayout";
import useUser from "../../hooks/useUser";
import { useLocation } from "wouter";

export default function UserConfigurationForm() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<UserConfigurationForm>();

  const [, setLocation] = useLocation();
  const { modifyUser, userErrors } = useUser();
  const timezones = Intl.supportedValuesOf("timeZone");

  const timezonesHtml = timezones.map((timezone) => (
    <option value={timezone}>{timezone}</option>
  ));

  const submitForm: SubmitHandler<UserConfigurationForm> = async (data) => {
    await modifyUser(data);
    if (userErrors.length > 0) return;

    setLocation("/dashboard");
  };

  return (
    <PublicLayout header="Configuración de cuenta">
      <form onSubmit={handleSubmit(submitForm)}>
        <div>
          <label htmlFor="">Tu meta minima</label>
          <p>El porcentaje de minutos que debes cumplir</p>
          <input
            max={100}
            min={75}
            type="number"
            {...register("minGoal", {
              valueAsNumber: true,
              required: {
                value: true,
                message: "Debes ingresar una meta diaria",
              },
              max: { value: 100, message: "El valor maximo es 100%" },
              min: { value: 75, message: "El valor minimo es 75%" },
            })}
          />
        </div>
        <div>
          <label htmlFor="">Hora de corte</label>
          <p>La hora local en que se generaran las nuevas tareas</p>
          <input
            max={23}
            min={0}
            type="number"
            {...register("cutOffTime", {
              valueAsNumber: true,
              required: {
                value: true,
                message: "Debes ingresar una hora de corte",
              },
              max: { value: 23, message: "El valor maximo es 23:00" },
              min: { value: 0, message: "El valor minimo es 00:00" },
            })}
          />
        </div>
        <div>
          <label htmlFor="">Zona horaria</label>
          <p>Tu zona horaria</p>
          <select
            {...register("timeZone", {
              required: {
                value: true,
                message: "Debes ingresar tu zona horaria",
              },
            })}
          >
            {timezonesHtml}
          </select>
        </div>
        <ul>
          {errors.minGoal && <li>{errors.minGoal.message}</li>}
          {errors.cutOffTime && <li>{errors.cutOffTime.message}</li>}
          {errors.timeZone && <li>{errors.timeZone.message}</li>}
        </ul>
        <input type="submit" value="Continuar" />
      </form>
    </PublicLayout>
  );
}
