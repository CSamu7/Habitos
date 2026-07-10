import { useRef, useState, type MouseEventHandler } from "react";
import useCategories from "../../../hooks/useCategories";
import AddCategoryToHabit from "./AddCategoryModal";
import { useForm, type SubmitHandler } from "react-hook-form";
import type {
  CreateRoutineModalInputs,
  GetAllRoutinesResponse,
} from "../types/routineTypes";
import useRoutine from "../../../hooks/useRoutine";

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
  const addCategoriesModalRef = useRef<HTMLDialogElement>(null);

  const {
    register,
    handleSubmit,
    watch,
    formState: { errors },
  } = useForm<CreateRoutineModalInputs>();

  const totalMinutes = routines.list.reduce(
    (acc, cur) => (acc += cur.minutes),
    0,
  );

  //TODO: Las horas maximas que permite la aplicación. Yo lo movería a otro archivo...
  const MAX_HOURS = 480;
  const remainingHours = Math.floor((MAX_HOURS - totalMinutes) / 60);

  const formHours = Number(watch("hours"));
  const remainingMinutes = Math.round((480 - totalMinutes) % 60);

  const [selectedCategories, setSelectedCategories] = useState<Array<number>>(
    [],
  );

  const selectedCategoriesJSX = categories.list
    .filter((category) =>
      selectedCategories.includes(category.id_routine_category),
    )
    .map((category) => (
      <div>
        <p style={{ backgroundColor: `#${category.color}` }}>{category.name}</p>
      </div>
    ));

  const onSelectCategory: MouseEventHandler<HTMLLIElement> = (event) => {
    //todo: Dejarlo pendiente por el momente. Realizar despues de hacer la conexión con el backend
    event.preventDefault();
    console.log(event);
  };

  const onSubmit: SubmitHandler<CreateRoutineModalInputs> = async (data) => {
    //TODO: Conectar con el backend. Crear el tipo de Request. Verificar que funcione en el backend. ¿Modificar backend posiblemente?

    await postRoutine(data);

    //TODO: If response.errors..., then show the errors.
  };

  return (
    <dialog id="create-routine-modal" closedby="any" ref={ref}>
      <h2>Nuevo habito</h2>
      <form action="" onSubmit={handleSubmit(onSubmit)}>
        <div>
          <label htmlFor="">Nombre</label>
          <input
            type="text"
            {...register("name", {
              required: {
                value: true,
                message: "Debes ingresar un valor en el campo Nombre",
              },
            })}
          />
          {errors.name && <p>{errors.name.message}</p>}
        </div>
        <div>
          <label htmlFor="">Tiempo</label>
          <div>
            <input
              type="number"
              {...register("hours", {
                required: {
                  value: true,
                  message: "Debes ingresar un valor en el campo de Tiempo",
                },
                min: {
                  value: 0,
                  message: `El valor debe estar entre 0 y ${remainingHours}`,
                },
                max: {
                  value: remainingHours,
                  message: `El valor debe estar entre 0 y ${remainingHours}`,
                },
              })}
            />
            <p>hr</p>
            {errors.hours?.message && (
              <p style={{ color: "red" }}>{errors.hours.message}</p>
            )}
            <input
              type="number"
              {...register("minutes", {
                required: {
                  value: true,
                  message: "Debes ingresar un valor en el campo de Tiempo",
                },
                min: {
                  value: 0,
                  message: `El valor debe estar entre 0 y ${formHours === remainingHours ? remainingMinutes : 60}`,
                },
                max: {
                  value: formHours === remainingHours ? remainingMinutes : 60,
                  message: `El valor debe estar entre 0 y ${formHours === remainingHours ? remainingMinutes : 60}`,
                },
              })}
            />
            <p>min</p>
            {errors.minutes && <p>{errors.minutes.message}</p>}
          </div>
        </div>
        <div>
          <label htmlFor="">Categorias</label>
          <button
            type="button"
            onClick={() => addCategoriesModalRef.current?.showModal()}
          >
            Añadir categorias
          </button>
          <div>{selectedCategoriesJSX}</div>
        </div>
        <input type="submit" value="Añadir habito" />
      </form>
      <AddCategoryToHabit
        categories={categories}
        onSelectCategory={onSelectCategory}
        ref={addCategoriesModalRef}
      ></AddCategoryToHabit>
    </dialog>
  );
}
