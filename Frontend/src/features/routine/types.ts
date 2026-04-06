export type HabitProgress = "Pending" | "Incomplete" | "Completed";

export type DailyTaskResponse = {
  idDailyRoutine: number;
  routine: MinRoutineResponse;
  minutesCompleted: number;
  totalMinutes: number;
  percentageCompleted: string;
  completedAt: Date;
};

export type MinRoutineResponse = {
  id: number;
  name: string;
};

export type GetDailyTasksResponse = {
  results: DailyTaskResponse[];
  count: number;
  totalMinutes: number;
  minutesCompleted: number;
  minutesLeft: number;
  percentageCompleted: string;
};
