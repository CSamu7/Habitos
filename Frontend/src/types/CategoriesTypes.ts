export type GetCategoriesResponse = {
  list: Array<CategoryData>;
  count: number;
};

export type CategoryData = {
  idCategory: number;
  name: string;
  color: string;
};
