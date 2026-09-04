import type { HTMLAttributes } from "react";
import type { CategoryData } from "../../../types/CategoriesTypes";
import styles from "./CategoryLabel.module.css";

interface CategoryLabelProps extends HTMLAttributes<HTMLDivElement> {
  category: CategoryData;
}

export default function CategoryLabel({
  category,
  className,
  ...rest
}: CategoryLabelProps) {
  return (
    <div
      className={`${styles.dropdownItem} ${className}`}
      key={category.idCategory}
      {...rest}
    >
      <div
        style={{ backgroundColor: category.color }}
        className={styles.dropdownItemColor}
      ></div>
      <p className={styles.dropdownItemName}>{category.name}</p>
      {rest.children}
    </div>
  );
}
