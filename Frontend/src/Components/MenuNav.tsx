import Header from "./Header";
import styles from "./MenuNav.module.css";

export default function MenuNav() {
  return (
    <Header>
      <ul className={styles.menu}>
        <li>
          <a href="/dashboard" className={styles.menuLink}>
            Inicio
          </a>
        </li>
        <li>
          <a href="/my-routines" className={styles.menuLink}>
            Mis rutinas
          </a>
        </li>
        <li>
          <a href="/stats" className={styles.menuLink}>
            Historial
          </a>
        </li>
      </ul>
    </Header>
  );
}
