import type { ReactNode } from "react";
import styles from "./MenuNav.module.css";

export default function NavMenu({
  children,
}: {
  children?: ReactNode | ReactNode[] | null;
}) {
  return <ul className={styles.menu}>{children}</ul>;
}
