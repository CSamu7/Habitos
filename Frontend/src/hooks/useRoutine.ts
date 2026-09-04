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
  const [errorRoutines, setErrorRoutines] = useState<Array<string>>([]);

  useEffect(() => {
    const getRoutines = async () => {
      const routines = await routineService.getAll(username);

      if (routines.error) {
        setErrorRoutines([routines.error.detail]);
      } else {
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

    if (response.error) {
      setErrorRoutines([response.error.detail]);
    }
  };

  return { routines, errorRoutines, postRoutine };
}
