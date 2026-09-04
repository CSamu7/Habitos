import { useState } from "react";
import useCategories from "../../../hooks/useCategories";
import { useForm, type SubmitHandler } from "react-hook-form";
import type {
  CreateRoutineModalInputs,
  CreateRoutineRequest,
  GetAllRoutinesResponse,
} from "../types/routineTypes";
import useRoutine from "../../../hooks/useRoutine";
import FormInput from "../../../components/FormInput";
import styles from "./CreateRoutineModal.module.css";
import AddCategoriesInput from "./AddCategoriesInput";
import type { CategoryData } from "../../../types/CategoriesTypes";

export default function CreateRoutineModal({
  username,
  ref,
}: {
  username: string;
  ref: React.RefObject<HTMLDialogElement | null>;
  routines: GetAllRoutinesResponse;
}) {
  const { categories } = useCategories(username);
  const { routines, postRoutine } = useRoutine(username);

  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<CreateRoutineModalInputs>();

  //TODO: Lo podríamos enviar a través del backend.
  const totalMinutes = routines.list.reduce(
    (acc, cur) => (acc += cur.minutes),
    0,
  );

  const [selectedCategories, setSelectedCategories] = useState<
    Array<CategoryData>
  >([]);

  //TODO: Las horas maximas que permite la aplicación. Yo lo movería a otro archivo...
  const MAX_HOURS = 480;
  const remainingHours = Math.floor((MAX_HOURS - totalMinutes) / 60);

  const formHours = Number(watch("hours"));
  const remainingMinutes = Math.round((480 - totalMinutes) % 60);

  const onAddCategory = (category: CategoryData) => {
    setSelectedCategories([...selectedCategories, category]);
  };

  const onDeleteCategory = (category: CategoryData) => {
    setSelectedCategories(
      selectedCategories.filter((c) => {
        return c.idCategory != category.idCategory;
      }),
    );
  };

  const onSubmit: SubmitHandler<CreateRoutineModalInputs> = async (data) => {
    try {
      const request = await postRoutine(data, selectedCategories);

      //wao se creo tu habito
      //cerrar model
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <dialog id="create-routine-modal" closedby="any" ref={ref}>
      <form action="" onSubmit={handleSubmit(onSubmit)}>
        <h2>Nuevo habito</h2>
        <FormInput
          label="Nombre"
          type="text"
          {...register("name", {
            required: {
              value: true,
              message: "Tu tarea debe tener un nombre",
            },
          })}
        ></FormInput>
        <div className={styles.formTimeSection}>
          <label htmlFor="">Tiempo</label>
          <div className={styles.formTimeInputs}>
            <input
              type="number"
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
            />
            <p>hr</p>

            <input
              type="number"
              defaultValue={0}
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
                  message: `Tus minutos deben estar entre 0 y ${formHours === remainingHours ? remainingMinutes : 60} minutos`,
                },
              })}
            />
            <p>min</p>
          </div>
        </div>
        <div>
          <AddCategoriesInput
            categories={categories.list}
            selectedCategories={selectedCategories}
            onAddCategories={onAddCategory}
            onDeleteCategory={onDeleteCategory}
          ></AddCategoriesInput>
        </div>

        <div>
          {errors.name?.message && (
            <p style={{ color: "red" }}>{errors.name.message}</p>
          )}
          {errors.hours?.message && (
            <p style={{ color: "red" }}>{errors.hours.message}</p>
          )}
          {errors.minutes && (
            <p style={{ color: "red" }}>{errors.minutes.message}</p>
          )}
        </div>
        <input type="submit" value="Añadir habito" />
      </form>
    </dialog>
  );
}
