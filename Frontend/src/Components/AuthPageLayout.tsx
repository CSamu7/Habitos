import type { ReactNode } from "react";
import styles from "./AuthPageLayout.module.css";
import DashboardNav from "./DashboardNav";

export default function AuthPageLayout({
  children,
}: {
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <DashboardNav></DashboardNav>
      <main className={styles.main}>{children}</main>
    </div>
  );
}
