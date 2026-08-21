export type DailyTaskStatus =
  | "NotDone"
  | "Incomplete"
  | "Completed"
  | "InProgress";

export type DailyTasksData = {
  list: DailyTaskInformation[];
  count: number;
  totalMinutes: number;
  minutesCompleted: number;
  minutesLeft: number;
  percentageCompleted: string;
};

export type DailyTaskInformation = {
  idDailyRoutine: number;
  routine: RoutineBasicInformation;
  minutesCompleted: number;
  totalMinutes: number;
  percentageCompleted: string;
  status: DailyTaskStatus;
  completedAt: Date;
};

export type RoutineBasicInformation = {
  id: number;
  name: string;
};
