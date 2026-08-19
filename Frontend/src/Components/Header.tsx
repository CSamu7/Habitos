import type { ReactNode } from "react";
import styles from "./Header.module.css";

export default function Header({
  children,
}: {
  children: ReactNode | ReactNode[] | null;
}) {
  return <header className={styles.header}>{children}</header>;
}
