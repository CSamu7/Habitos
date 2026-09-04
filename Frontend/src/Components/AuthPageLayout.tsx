import type { ReactNode } from "react";
import styles from "./AuthPageLayout.module.css";
import NavMenu from "./NavMenu";
import Header from "./Header";

export default function AuthPageLayout({
  children,
}: {
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <Header>
        <NavMenu>
          <li>
            <a href="/dashboard">Inicio</a>
          </li>
          <li>
            <a href="/my-routines">Mis rutinas</a>
          </li>
          <li>
            <a href="/stats">Historial</a>
          </li>
        </NavMenu>
      </Header>
      <main className={styles.main}>{children}</main>
    </div>
  );
}
