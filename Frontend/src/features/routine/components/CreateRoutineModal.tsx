import { useState } from "react";
import useCategories from "../../../hooks/useCategories";
import { useForm, type SubmitHandler } from "react-hook-form";
import type {
  CreateRoutineModalInputs,
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
    <dialog
      id="create-routine-modal"
      closedby="any"
      ref={ref}
      className={styles.createRoutineModal}
    >
      <div>
        <h2 className={styles.modalTitle}>Crea tu nuevo habito</h2>
      </div>
      <form
        action=""
        onSubmit={handleSubmit(onSubmit)}
        className={styles.createRoutineForm}
      >
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
                  message: `Solo te quedan ${formHours === remainingHours ? remainingMinutes : 60} minutos. Elimina una tarea o reduce la cantidad de tiempo de tu tarea`,
                },
              })}
              defaultValue={0}
            ></FormInput>
          </div>
        </div>
        <div className={styles.categoriesSection}>
          <AddCategoriesInput
            categories={categories.list}
            selectedCategories={selectedCategories}
            onAddCategories={onAddCategory}
            onDeleteCategory={onDeleteCategory}
          ></AddCategoriesInput>
        </div>

        <div className={styles.errorMessages}>
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
        <input className={styles.submitButton} type="submit" value="Crear" />
      </form>
    </dialog>
  );
}
