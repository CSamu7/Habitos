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
import ErrorMessage from "../../../components/ErrorMessage";
import FormTimeInput from "./FormTimeInput";
import { formatErrors } from "../../../types/common";

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
    setError,
  } = useForm<CreateRoutineModalInputs>();

  //enviar esta suma desde el backend...
  const totalMinutes = routines.list.reduce(
    (acc, cur) => (acc += cur.minutes),
    0,
  );

  const [selectedCategories, setSelectedCategories] = useState<
    Array<CategoryData>
  >([]);

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
      const result = await postRoutine(data, selectedCategories);

      if (result.error) {
        if (result.error.kind == "OneError") {
          setError("root.server", { message: result.error.detail });
        } else {
          //formatErrors deberia que pasar en el service...
          const errors = formatErrors(result);
          setError("root.server", { message: errors[0] });
        }
      }
    } catch (error: unknown) {
      setError("root.server", {
        message: "Hubo un error en el servidor. Vuelva mas tarde",
      });
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
        <FormTimeInput
          totalMinutes={totalMinutes}
          register={register}
          watch={watch}
        ></FormTimeInput>
        <div className={styles.categoriesSection}>
          <AddCategoriesInput
            categories={categories.list}
            selectedCategories={selectedCategories}
            onAddCategories={onAddCategory}
            onDeleteCategory={onDeleteCategory}
          ></AddCategoriesInput>
        </div>
        <div className={styles.errorMessages}>
          {errors.name && <ErrorMessage>{errors.name.message}</ErrorMessage>}
          {errors.hours && <ErrorMessage>{errors.hours.message}</ErrorMessage>}
          {errors.minutes && (
            <ErrorMessage>{errors.minutes.message}</ErrorMessage>
          )}
          {errors.root?.server && (
            <ErrorMessage>{errors.root.server.message}</ErrorMessage>
          )}
        </div>
        <input className={styles.submitButton} type="submit" value="Crear" />
      </form>
    </dialog>
  );
}
