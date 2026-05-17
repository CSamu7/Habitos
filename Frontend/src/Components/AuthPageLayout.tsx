import type { ReactNode } from "react";
import MenuNav from "./MenuNav";
import styles from "./AuthPageLayout.module.css";

export default function AuthPageLayout({
  children,
}: {
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <MenuNav></MenuNav>
      <main className={styles.main}>{children}</main>
    </div>
  );
}
