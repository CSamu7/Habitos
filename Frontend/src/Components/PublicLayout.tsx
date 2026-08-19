import type { ReactNode } from "react";
import Header from "./Header";
import styles from "./PublicLayout.module.css";

export default function PublicLayout({
  header,
  children,
}: {
  header?: ReactNode | ReactNode[] | null;
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <Header>{header}</Header>
      <main>{children}</main>
    </div>
  );
}
