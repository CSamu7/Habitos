export type GetCategoriesResponse = {
  list: Array<CategoryData>;
  count: number;
};

export type CategoryData = {
  id_routine_category: number;
  name: string;
  color: string;
};
