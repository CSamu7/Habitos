import type { ReactNode } from "react";
import Header from "./Header";
import styles from "./PublicLayout.module.css";
import PublicNav from "./PublicNav";

export default function PublicLayout({
  children,
}: {
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <Header>
        <PublicNav></PublicNav>
      </Header>
      {children}
    </div>
  );
}
