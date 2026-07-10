import { useEffect, useState } from "react";
import { categoriesService } from "../services/categoriesService";
import type { GetCategoriesResponse } from "../types/CategoriesTypes";

export default function useCategories(username: string) {
  const [categories, setCategories] = useState<GetCategoriesResponse>({
    list: [],
    count: 0,
  });
  const [errorCategories, setErrorCategories] = useState<string>();

  useEffect(() => {
    const getCategories = async () => {
      const categories = await categoriesService.getAll(username);

      if (categories.error) {
        setErrorCategories(categories.error.detail);
      } else {
        setCategories(categories.data);
      }
    };

    if (username) getCategories();
  }, [username]);

  return { categories, errorCategories };
}
