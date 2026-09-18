import { useState } from "react";
import type { CategoryData } from "../../../types/CategoriesTypes";
import styles from "./AddCategoriesInput.module.css";
import CategoryLabel from "./CategoryLabel";

export default function AddCategoriesInput({
  categories,
  selectedCategories,
  onAddCategories,
  onDeleteCategory,
}: {
  categories: CategoryData[];
  selectedCategories: CategoryData[];
  onAddCategories: (category: CategoryData) => void;
  onDeleteCategory: (category: CategoryData) => void;
}) {
  const [isDropdownActive, setIsDropdownActive] = useState(false);
  const [searchValue, setSearchValue] = useState("");

  const onFocusInput = () => setIsDropdownActive(true);
  const onBlurInput = () => setIsDropdownActive(false);

  const leftCategories = categories.filter((category) => {
    return !selectedCategories.includes(category);
  });

  const selectedCategoriesJSX = selectedCategories.map((category) => {
    return (
      <CategoryLabel category={category} key={category.idCategory}>
        <button
          onClick={() => {
            onDeleteCategory(category);
          }}
        >
          X
        </button>
      </CategoryLabel>
    );
  });

  const dropdownCategories = leftCategories
    .filter((category: CategoryData) =>
      searchValue != ""
        ? category.name.toLowerCase().includes(searchValue.toLowerCase())
        : true,
    )
    .slice(0, 5)
    .map((category) => (
      <CategoryLabel
        category={category}
        onMouseDown={(e) => {
          e.preventDefault();
          onAddCategories(category);
        }}
        className={styles.dropdownItem}
      ></CategoryLabel>
    ));

  return (
    <div className={styles.categoriesGroup}>
      <label htmlFor="input-categories">Categorias</label>
      <div className={styles.categoriesBox}>
        <input
          onFocus={onFocusInput}
          onBlur={onBlurInput}
          id="input-categories"
          type="search"
          autoComplete="on"
          value={searchValue}
          onChange={(e) => setSearchValue(e.target.value)}
          list="categories-options"
          placeholder="Busca la categoria..."
          className={styles.categoriesSearchBar}
        />
        {isDropdownActive && (
          <div className={styles.dropdown}>
            {categories.length == 0 ? (
              <p>No hay mas categorias disponibles!</p>
            ) : (
              <ul className={styles.dropdownList}>{dropdownCategories}</ul>
            )}
          </div>
        )}
        <ul className={styles.categoriesSelected}>{selectedCategoriesJSX}</ul>
      </div>
    </div>
  );
}
