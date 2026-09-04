import type { ReactNode } from "react";
import Header from "./Header";
import styles from "./PublicLayout.module.css";
import NavMenu from "./NavMenu";

export default function PublicLayout({
  children,
}: {
  children: ReactNode | ReactNode[];
}) {
  return (
    <div className={styles.page}>
      <Header>
        <NavMenu>
          <li>
            <a href="/login">Inicio</a>
          </li>
        </NavMenu>
      </Header>
      {children}
    </div>
  );
}
