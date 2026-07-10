import type { DailyTaskStatus } from "../../../types/DailyTasksTypes";

export type GetAllRoutinesResponse = {
  list: Array<RoutineData>;
  //TODO: Falta por añadir
  //count: number;
};

export type RoutineData = {
  id: number;
  name: string;
  minutes: number;
  category: string;
  color: string;
  lastDays: Record<string, DailyTaskStatus>;
};

export type CreateRoutineModalInputs = {
  name: string;
  hours: number;
  minutes: number;
  categories: Array<number>;
};

export type CreateRoutineRequest = {
  name: string;
  minutes: number;
  categories: Array<number>;
};
