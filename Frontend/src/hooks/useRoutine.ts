import { useEffect, useState } from "react";
import { routineService } from "../services/routineService";
import {
  type CreateRoutineModalInputs,
  type CreateRoutineRequest,
  type GetAllRoutinesResponse,
} from "../features/routine/types/routineTypes";
import type { CategoryData } from "../types/CategoriesTypes";

export default function useRoutine(username: string) {
  const [routines, setRoutines] = useState<GetAllRoutinesResponse>({
    list: [],
  });

  useEffect(() => {
    const getRoutines = async () => {
      const routines = await routineService.getAll(username);

      if (!routines.error) {
        setRoutines({ list: routines.data });
      }
    };

    if (username) getRoutines();
  }, [username]);

  const postRoutine = async (
    form: CreateRoutineModalInputs,
    categoriesSelected: CategoryData[],
  ) => {
    const body: CreateRoutineRequest = {
      name: form.name,
      minutes: Number(form.minutes) + Number(form.hours) * 60,
      categories: categoriesSelected.map((category) => category.idCategory),
    };

    const response = await routineService.post(username, body);

    return response;
  };

  const deleteRoutine = async (idRoutine: number) => {
    const response = await routineService.delete(username, idRoutine);

    return response;
  };

  return { routines, postRoutine, deleteRoutine };
}
