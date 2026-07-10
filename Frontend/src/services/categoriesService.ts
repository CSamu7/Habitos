import type { GetCategoriesResponse } from "../types/CategoriesTypes";
import type { ErrorAPI, Result } from "../types/common";

export const categoriesService = {
  async getAll(username: string): Promise<Result<GetCategoriesResponse>> {
    const baseUrl = import.meta.env["VITE_USER_URL"];

    const request = await fetch(`${baseUrl}/${username}/categories`, {
      credentials: "include",
    });

    if (!request.ok) {
      const response: ErrorAPI = await request.json();

      return { error: response };
    }

    const response: GetCategoriesResponse = await request.json();

    return { data: response };
  },
};
