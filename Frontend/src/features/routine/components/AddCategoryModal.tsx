import type { MouseEventHandler } from "react";
import type { GetCategoriesResponse } from "../../../types/CategoriesTypes";

export default function AddCategoryModal({
  categories,
  onSelectCategory,
  ref,
}: {
  categories: GetCategoriesResponse;
  onSelectCategory: MouseEventHandler<HTMLLIElement>;
  ref: React.RefObject<HTMLDialogElement | null>;
}) {
  //TODO: Crear este dialog y añadir mas información a GETALL Categories.
  const selectedCategoriesJSX = categories.list.map((category) => (
    <li key={category.id_routine_category} onClick={onSelectCategory}>
      <p>{category.name}</p>
      <div style={{ backgroundColor: `#${category.color}` }}></div>
    </li>
  ));

  return (
    <dialog closedby="any" ref={ref}>
      <h2>Tus rutinas</h2>
      <ul>
        {selectedCategoriesJSX.length > 0 ? (
          selectedCategoriesJSX
        ) : (
          <p>No tienes categorias</p>
        )}
      </ul>
    </dialog>
  );
}
